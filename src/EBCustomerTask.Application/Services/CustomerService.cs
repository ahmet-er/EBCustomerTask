using AutoMapper;
using EBCustomerTask.Application.DTOs;
using EBCustomerTask.Application.Interfaces;
using EBCustomerTask.Core.Entities;
using EBCustomerTask.Core.Interfaces;

namespace EBCustomerTask.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepositoryContext _customerRepositoryContext;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepositoryContext customerRepositoryContext, IMapper mapper)
        {
            _customerRepositoryContext = customerRepositoryContext;
            _mapper = mapper;
        }
        
        public async Task<List<CustomerGetAllViewModel>> GetAllAsync()
        {
            var repository = await _customerRepositoryContext.GetRepositoryAsync();
            var customers = await repository.GetAllAsync();
            return _mapper.Map<List<CustomerGetAllViewModel>>(customers);
        }

		public async Task<List<CustomerGetAllViewModel>> GetAllAsync(string query)
		{
			var repository = await _customerRepositoryContext.GetRepositoryAsync();
			var customers = await repository.GetAllAsync();

            var filteredCustomers = customers
				.Where(c => c.FirstName.Contains(query, StringComparison.OrdinalIgnoreCase) ||
						c.LastName.Contains(query, StringComparison.OrdinalIgnoreCase) ||
						c.Email.Contains(query, StringComparison.OrdinalIgnoreCase) ||
						c.PhoneNumber.Contains(query, StringComparison.OrdinalIgnoreCase))
			.Select(c => new CustomerGetAllViewModel
			{
				Id = c.Id,
				FirstName = c.FirstName,
				LastName = c.LastName,
				Email = c.Email,
				PhoneNumber = c.PhoneNumber
			});

			return _mapper.Map<List<CustomerGetAllViewModel>>(filteredCustomers);
		}

		public async Task<CustomerDetailViewModel> GetCustomerByIdAsync(string id)
        {
            var repository = await _customerRepositoryContext.GetRepositoryAsync();
            var customer = await repository.GetByIdAsync(id);
            return _mapper.Map<CustomerDetailViewModel>(customer);
        }

        public async Task SaveCustomerAsync(CustomerCreateViewModel model)
        {
            var repository = await _customerRepositoryContext.GetRepositoryAsync();
            var customer = _mapper.Map<Customer>(model);
            await repository.SaveAsync(customer);
        }

        public async Task UpdateCustomerAsync(CustomerUpdateViewModel model)
        {
            var repository = await _customerRepositoryContext.GetRepositoryAsync();
            var customer = _mapper.Map<Customer>(model);
            await repository.UpdateAsync(customer);
        }

        public async Task DeleteCustomerAsync(string id)
        {
            var repository = await _customerRepositoryContext.GetRepositoryAsync();
            var customer = await repository.GetByIdAsync(id);
            if (customer is not null)
            {
                await repository.DeleteAsync(customer);
            }
        }
    }
}
