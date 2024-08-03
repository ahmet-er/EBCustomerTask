using EBCustomerTask.Core.Entities;

namespace EBCustomerTask.Application.Interfaces
{
    public interface IConfigurationService
    {
        Task<Configuration> GetConfigurationByKeyAsync(string key);
        Task SetConfigurationAsync(string key, string value);
    }
}
