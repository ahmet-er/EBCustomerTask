using AutoMapper;
using EBCustomerTask.Application.DTOs;
using EBCustomerTask.Application.Interfaces;
using EBCustomerTask.Core.Entities;
using EBCustomerTask.Core.Interfaces;

namespace EBCustomerTask.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task DeleteCustomerAsync(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer is not null)
            {
                await _customerRepository.DeleteAsync(customer);
            }
        }

        public async Task<List<CustomerGetAllViewModel>> GetAllAsync()
        {
            var customers = await _customerRepository.GetAllAsync();
            return _mapper.Map<List<CustomerGetAllViewModel>>(customers);
        }

        public async Task<CustomerDetailViewModel> GetCustomerByIdAsync(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            return _mapper.Map<CustomerDetailViewModel>(customer);
        }

        public async Task SaveCustomerAsync(CustomerCreateViewModel model)
        {
            var customer = _mapper.Map<Customer>(model);
            await _customerRepository.SaveAsync(customer);
        }

        public async Task UpdateCustomerAsync(CustomerUpdateViewModel model)
        {
            var customer = _mapper.Map<Customer>(model);
            await _customerRepository.UpdateAsync(customer);
        }
    }
}
