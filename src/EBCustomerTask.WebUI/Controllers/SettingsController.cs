using EBCustomerTask.Application.Interfaces;
using EBCustomerTask.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EBCustomerTask.WebUI.Controllers
{
    [Authorize(Roles =nameof(Role.Admin))]
    public class SettingsController : Controller
    {
        private readonly IConfigurationService _configurationService;

        public SettingsController(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }

        public async Task<IActionResult> Index()
        {
            var config = await _configurationService.GetConfigurationByKeyAsync(Configuration.DatabaseType.ToString());
            var databaseType = config is not null ? (DatabaseType)int.Parse(config.Value) : DatabaseType.SQLServer;
            ViewBag.DatabaseType = databaseType;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangeDatabase(int databaseType)
        {
            await _configurationService.SetConfigurationAsync(Configuration.DatabaseType.ToString(), databaseType.ToString());
            return RedirectToAction(nameof(Index));
        }
    }
}
