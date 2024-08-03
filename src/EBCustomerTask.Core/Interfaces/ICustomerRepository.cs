using EBCustomerTask.Core.Entities;

namespace EBCustomerTask.Core.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer> GetByIdAsync(string id);
        Task<List<Customer>> GetAllAsync();
        Task<Customer> SaveAsync(Customer customer);
        Task UpdateAsync(Customer customer);
        Task DeleteAsync(Customer customer);
    }
}
