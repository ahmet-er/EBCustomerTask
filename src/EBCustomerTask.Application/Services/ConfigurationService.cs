using EBCustomerTask.Application.Interfaces;
using EBCustomerTask.Core.Entities;
using EBCustomerTask.Core.Interfaces;

namespace EBCustomerTask.Application.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IConfigurationRepository _configurationRepository;

        public ConfigurationService(IConfigurationRepository configurationRepository)
        {
            _configurationRepository = configurationRepository;
        }

        public async Task<Configuration> GetConfigurationByKeyAsync(string key)
        {
            return await _configurationRepository.GetConfigurationByKeyAsync(key);
        }

        public async Task SetConfigurationAsync(string key, string value)
        {
            await _configurationRepository.SetConfigurationAsync(key, value);
        }
    }
}
