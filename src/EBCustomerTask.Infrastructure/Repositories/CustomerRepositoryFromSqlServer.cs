using EBCustomerTask.Core.Entities;
using EBCustomerTask.Core.Interfaces;
using EBCustomerTask.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EBCustomerTask.Infrastructure.Repositories
{
    public class CustomerRepositoryFromSqlServer : ICustomerRepository
    {
        private readonly AppIdentityDbContext _context;

        public CustomerRepositoryFromSqlServer(AppIdentityDbContext context)
        {
            _context = context;
        }

        public async Task DeleteAsync(Customer customer)
        {
            _context.Customers.Remove(customer);

            await _context.SaveChangesAsync();
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> GetByIdAsync(string id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task<Customer> SaveAsync(Customer customer)
        {
            if (string.IsNullOrEmpty(customer.Id))
            {
                customer.Id = Guid.NewGuid().ToString();
            }

            await _context.Customers.AddAsync(customer);

            await _context.SaveChangesAsync();

            return customer;
        }

        public async Task UpdateAsync(Customer customer)
        {
            var existingCustomer = await _context.Customers.FindAsync(customer.Id);

            if (existingCustomer is not null)
            {
                _context.Entry(existingCustomer).CurrentValues.SetValues(customer);
                await _context.SaveChangesAsync();
            }

        }
    }
}
