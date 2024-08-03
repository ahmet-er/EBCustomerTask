using EBCustomerTask.Application.DTOs;
using EBCustomerTask.Application.Interfaces;
using EBCustomerTask.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace EBCustomerTask.WebUI.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IPhotoService _photoService;

		public CustomerController(ICustomerService customerService, IPhotoService photoService)
		{
			_customerService = customerService;
			_photoService = photoService;
		}

		public async Task<IActionResult> Index()
        {
            var customers = await _customerService.GetAllAsync();
            return View(customers);
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id.IsNullOrEmpty())
            {
                return NotFound();
            }
            
            var customer = await _customerService.GetCustomerByIdAsync(id);

            if (customer is null)
            {
                return NotFound();
            }

            return View(customer);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerCreateViewModel model, IFormFile? photo)
        {
            if (ModelState.IsValid)
            {
                const string defaultPhotoUrl = "/photos/default_user_photo.png";

                if (photo is not null)
                {
                    var fileName = $"{model.FirstName}_{model.LastName}_{DateTime.Now:yyyyMMdd_HHmmss}";
                    model.PhotoUrl = await _photoService.UploadPhotoAsync(photo, fileName);
                }
                else
                {
                    model.PhotoUrl = "/photos/default_user_photo.png";
                }

                await _customerService.SaveCustomerAsync(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id.IsNullOrEmpty())
            {
                return NotFound();
            }

            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer is null)
            {
                return NotFound();
            }

            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, CustomerUpdateViewModel model, IFormFile photo)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingCustomer = await _customerService.GetCustomerByIdAsync(id);
                    if (existingCustomer is null)
                    {
                        return NotFound();
                    }

                    if (photo is not null)
                    {
						var fileName = $"{model.FirstName}_{model.LastName}_{DateTime.Now:yyyyMMdd_HHmmss}";
						model.PhotoUrl = await _photoService.UploadPhotoAsync(photo, fileName);

						if (!string.IsNullOrEmpty(existingCustomer.PhotoUrl) && existingCustomer.PhotoUrl != "/photos/default_user_photo.png")
						{
							await _photoService.DeletePhotoAsync(existingCustomer.PhotoUrl);
						}
					}
					else
					{
						model.PhotoUrl = existingCustomer.PhotoUrl;
					}

					await _customerService.UpdateCustomerAsync(model);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if(!CustomerExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id.IsNullOrEmpty())
            {
                return NotFound();
            }

            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer is null)
            {
                return NotFound();
            }

            return View(customer);
        }

        public async Task<IActionResult> DeleteConfirmed(string id)
		{
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer is null)
            {
                return NotFound();  
            }

			if (!string.IsNullOrEmpty(customer.PhotoUrl) && customer.PhotoUrl != "/photos/default_user_photo.png")
			{
				await _photoService.DeletePhotoAsync(customer.PhotoUrl);
			}

			await _customerService.DeleteCustomerAsync(id);

            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(string id)
        {
            return _customerService.GetCustomerByIdAsync(id) != null;
        }
    }
}
