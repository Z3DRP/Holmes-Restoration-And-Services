using Holmes_Services.Models;
using Holmes_Services.Models.DomainModels;
using Holmes_Services.Models.DTOs;
using Holmes_Services.Models.Extensions;
using Holmes_Services.Models.Grids;
using Holmes_Services.Models.QueryOptions;
using Holmes_Services.Models.Repositories;
using Holmes_Services.Models.Sessions;
using Holmes_Services.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Holmes_Services.Controllers
{
    public class DesignController : Controller
    {
        private HolmesServiceUnit data { get; set; }
        private Repo<Design> Ctx { get; set; }
        public DesignController(HolmesContext ctx)
        {
            data = new HolmesServiceUnit(ctx);
            Ctx = new Repo<Design>(ctx);
        }
        public IActionResult Index()
        {
            return View();
        }
        public DesignSession GetDesign()
        {
            var design = new DesignSession(HttpContext);
            design.Load(Ctx);
            return design;
        }
        public ViewResult List(DesignGridDTO designs)
        {
            var builder = new DesignGridBuilder(HttpContext.Session, designs, defaultSortField: nameof(Design.Id));
            var options = new DesignQueryOptions
        }
    }
}
