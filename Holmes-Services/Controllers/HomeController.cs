using Holmes_Services.Models;
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
        private HolmesServiceUnit data { get; set; }
        private HolmesContext Ctx { get; set; }

        //public HomeController(HolmesContext ctx) => data = new HolmesServiceUnit(ctx);
        public HomeController(HolmesContext ctx)
        {
            data = new HolmesServiceUnit(ctx);
            Ctx = ctx;
        }

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var customers = Ctx.Customers.OrderBy(c => c.Id);
            var customersViewModel = new CustomerListViewModel
            {
                Customers = Ctx.Customers.OrderBy(c => c.Id)

            };    
            return View(customersViewModel);
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Ideas()
        {
            var ideas = Ctx.Ideas.OrderBy(i => i.Id);
            List<IdeaDTO> ideaDTOs = new List<IdeaDTO>();

            foreach (var ideasViewModel in ideas)
            {
                IdeaDTO idto = new IdeaDTO
                {
                    Id = ideasViewModel.Id,
                    Deck = ideasViewModel.Deck,
                    Rail = ideasViewModel.Rail,
                    Estimate = ideasViewModel.Estimate
                };
                ideaDTOs.Add(idto);
            }

            return View(ideaDTOs);
        }
        public IActionResult Portfollio()
        {
            var portfollios = Ctx.CompletedJobs.OrderBy(c => c.Id);
            List<PortfollioDTO> portfollioDTOs = new List<PortfollioDTO>();

            foreach (var portfollio in portfollios)
            {
                PortfollioDTO pdto = new PortfollioDTO
                {
                    Id = portfollio.Id,
                    Deck = portfollio.Job.Design.Deck,
                    Rail = portfollio.Job.Design.Rail,
                    Pattern = portfollio.Job.Design.Pattern,
                    Estimate = portfollio.Job.Design.Estimate,
                    Image = portfollio.Image
                };
                portfollioDTOs.Add(pdto);
            }

            return View(portfollioDTOs);

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