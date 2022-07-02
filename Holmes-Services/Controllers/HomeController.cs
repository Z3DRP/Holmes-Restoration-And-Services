using Holmes_Services.Models;
using Holmes_Services.Models.Repositories;
using Holmes_Services.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;

namespace Holmes_Services.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private HolmesServiceUnit data { get; set; }
        private HolmesContext testCtx { get; set; }

        //public HomeController(HolmesContext ctx) => data = new HolmesServiceUnit(ctx);
        public HomeController(HolmesContext ctx)
        {
            data = new HolmesServiceUnit(ctx);
            testCtx = ctx;
        }

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var customers = testCtx.Customers.OrderBy(c => c.Id);
            var customersViewModel = new CustomerListViewModel
            {
                Customers = testCtx.Customers.OrderBy(c => c.Id)

            };    
            return View(customersViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}