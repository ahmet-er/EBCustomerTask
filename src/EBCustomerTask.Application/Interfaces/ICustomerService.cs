using EBCustomerTask.Application.DTOs;

namespace EBCustomerTask.Application.Interfaces
{
    public interface ICustomerService
    { 
        Task<CustomerDetailViewModel> GetCustomerByIdAsync(string id);
        Task<List<CustomerGetAllViewModel>> GetAllAsync();
        Task<List<CustomerGetAllViewModel>> GetAllAsync(string query);
        Task SaveCustomerAsync(CustomerCreateViewModel model);
        Task UpdateCustomerAsync(CustomerUpdateViewModel model);
        Task DeleteCustomerAsync(string id);
    }
}
