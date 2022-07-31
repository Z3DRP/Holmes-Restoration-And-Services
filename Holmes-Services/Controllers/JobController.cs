using Microsoft.AspNetCore.Mvc;

namespace Holmes_Services.Controllers
{
    public class JobController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
