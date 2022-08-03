using Microsoft.AspNetCore.Mvc;

namespace Holmes_Services.Controllers
{
    public class PortfollioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
