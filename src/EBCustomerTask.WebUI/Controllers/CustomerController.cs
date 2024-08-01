using Microsoft.AspNetCore.Mvc;

namespace EBCustomerTask.WebUI.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
