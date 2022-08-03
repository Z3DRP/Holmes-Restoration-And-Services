using Microsoft.AspNetCore.Mvc;

namespace Holmes_Services.Controllers
{
    public class IdeaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
