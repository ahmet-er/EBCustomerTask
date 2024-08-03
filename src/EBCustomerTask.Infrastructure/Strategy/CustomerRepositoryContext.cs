using EBCustomerTask.Application.Interfaces;
using EBCustomerTask.Core.Enums;
using EBCustomerTask.Core.Interfaces;

namespace EBCustomerTask.Infrastructure.Strategy
{
    public class CustomerRepositoryContext : ICustomerRepositoryContext
    {
        private readonly ICustomerRepositoryFactory _repositoryFactory;
        private readonly IConfigurationService _configurationService;

        public CustomerRepositoryContext(ICustomerRepositoryFactory repositoryFactory, IConfigurationService configurationService)
        {
            _repositoryFactory = repositoryFactory;
            _configurationService = configurationService;
        }

        public async Task<ICustomerRepository> GetRepositoryAsync()
        {
            var config = await _configurationService.GetConfigurationByKeyAsync(Configuration.DatabaseType.ToString());
            var databaseType = config is not null ? (DatabaseType)int.Parse(config.Value) : DatabaseType.SQLServer;

            return databaseType switch
            {
                DatabaseType.SQLServer => _repositoryFactory.CreateSqlServerRepository(),
                DatabaseType.MongoDb => _repositoryFactory.CreateMongoDbRepository(),
                _ => throw new InvalidOperationException("Unknow database type")
            };
        }
    }
}
