using EBCustomerTask.Core.Entities;
using EBCustomerTask.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace EBCustomerTask.Infrastructure.Repositories
{
    public class CustomerRepositoryFromMongoDb : ICustomerRepository
    {
        private readonly IMongoCollection<Customer> _customerCollection;
        public CustomerRepositoryFromMongoDb(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MongoDb");

            var client = new MongoClient(connectionString);

            var database = client.GetDatabase("EBCustomerTaskDb");

            _customerCollection = database.GetCollection<Customer>("Customers");
        }
        public async Task DeleteAsync(Customer customer)
        {
            await _customerCollection.DeleteOneAsync(x => x.Id == customer.Id);
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            return await _customerCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            return await _customerCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Customer> SaveAsync(Customer customer)
        {
            await _customerCollection.InsertOneAsync(customer);

            return customer;
        }

        public async Task UpdateAsync(Customer customer)
        {
            await _customerCollection.FindOneAndReplaceAsync(x => x.Id == customer.Id, customer);
        }
    }
}
