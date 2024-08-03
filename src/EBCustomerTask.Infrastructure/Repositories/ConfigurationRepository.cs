using EBCustomerTask.Core.Entities;
using EBCustomerTask.Core.Interfaces;
using EBCustomerTask.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EBCustomerTask.Infrastructure.Repositories
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        private readonly AppIdentityDbContext _context;

        public ConfigurationRepository(AppIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<Configuration> GetConfigurationByKeyAsync(string key)
        {
            return await _context.Configurations.FirstOrDefaultAsync(x => x.Key == key);
        }

        public async Task SetConfigurationAsync(string key, string value)
        {
            var config = await _context.Configurations.FirstOrDefaultAsync(x => x.Key == key);

            if (config is not null)
            {
                config.Value = value;
                _context.Configurations.Update(config);
            }
            else
            {
                _context.Configurations.Add(new Configuration { Key = key, Value = value });
            }
            await _context.SaveChangesAsync();
        }
    }
}
