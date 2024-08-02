using EBCustomerTask.Application.DTOs;

namespace EBCustomerTask.Application.Interfaces
{
    public interface ICustomerService
    { 
        Task<CustomerDetailViewModel> GetCustomerByIdAsync(int id);
        Task<List<CustomerGetAllViewModel>> GetAllAsync();
        Task SaveCustomerAsync(CustomerCreateViewModel model);
        Task UpdateCustomerAsync(CustomerUpdateViewModel model);
        Task DeleteCustomerAsync(int id);
    }
}
