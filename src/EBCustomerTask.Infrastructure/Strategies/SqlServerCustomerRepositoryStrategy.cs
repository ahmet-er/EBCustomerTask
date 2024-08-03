using EBCustomerTask.Core.Entities;
using EBCustomerTask.Core.Interfaces;
using EBCustomerTask.Infrastructure.Repositories;

namespace EBCustomerTask.Infrastructure.Strategies
{
    public class SqlServerCustomerRepositoryStrategy : ICustomerRepositoryStrategy
    {
        private readonly ICustomerRepository _repository;

        public SqlServerCustomerRepositoryStrategy(CustomerRepositoryFromSqlServer repository)
        {
            _repository = repository;
        }

        public Task Delete(Customer customer) => _repository.DeleteAsync(customer);

        public Task<List<Customer>> GetAllAsync() => _repository.GetAllAsync();

        public Task<Customer> GetByIdAsync(string id) => _repository.GetByIdAsync(id);

        public Task<Customer> Save(Customer customer) => _repository.SaveAsync(customer);

        public Task Update(Customer customer) => _repository.UpdateAsync(customer);
    }
}
