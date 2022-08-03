using Holmes_Services.Data_Access.Repos;
using Holmes_Services.Models;
using Holmes_Services.Models.DomainModels;
using Holmes_Services.Models.DTOs;
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

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Customer()
        {
            return View();
        }
        public IActionResult Ideas()
        {
            IEnumerable<Idea> ideas = IdeaRepo.GetAllIdeas();
            // eventually incorporate idea vm
            return ideas == null ? View(Enumerable.Empty<Idea>()) : View(ideas);
        }
        public IActionResult Portfollio()
        {
            IEnumerable<CompletedJob> portfollio = PortfollioRepo.GetPortfollio();
            return portfollio == null ? View(Enumerable.Empty<CompletedJob>()) : View(portfollio);

        }
        [HttpGet]
        public IActionResult Contact(int id)
        {
            return View();
        }
        [HttpPost]
        public IActionResult Contact(HolmesMessage msg)
        {
            // this method will create a message obj in db
            return View();
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