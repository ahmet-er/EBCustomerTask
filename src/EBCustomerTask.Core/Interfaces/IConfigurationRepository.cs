using EBCustomerTask.Core.Entities;

namespace EBCustomerTask.Core.Interfaces
{
    public interface IConfigurationRepository
    {
        Task<Configuration> GetConfigurationByKeyAsync(string key);
        Task SetConfigurationAsync(string key, string value);
    }
}
