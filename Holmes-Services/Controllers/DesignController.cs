using Microsoft.AspNetCore.Mvc;

namespace Holmes_Services.Controllers
{
    public class DesignController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
