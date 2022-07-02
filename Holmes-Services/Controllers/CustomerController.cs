using Microsoft.AspNetCore.Mvc;

namespace Holmes_Services.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
