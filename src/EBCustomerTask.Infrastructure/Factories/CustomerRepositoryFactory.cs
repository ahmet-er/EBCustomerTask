using EBCustomerTask.Core.Interfaces;
using EBCustomerTask.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace EBCustomerTask.Infrastructure.Factories
{
    public class CustomerRepositoryFactory : ICustomerRepositoryFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public CustomerRepositoryFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ICustomerRepository CreateSqlServerRepository()
        {
            return _serviceProvider.GetRequiredService<CustomerRepositoryFromSqlServer>();
        }

        public ICustomerRepository CreateMongoDbRepository()
        {
            return _serviceProvider.GetRequiredService<CustomerRepositoryFromMongoDb>();
        }
    }
}
