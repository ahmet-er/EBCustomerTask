using EBCustomerTask.Core.Entities;

namespace EBCustomerTask.Core.Interfaces
{
    public interface ICustomerRepositoryStrategy
    {
        Task<List<Customer>> GetAllAsync();
        Task<Customer> GetByIdAsync(string id);
        Task<Customer> Save(Customer customer);
        Task Update(Customer customer);
        Task Delete(Customer customer);
    }
}
