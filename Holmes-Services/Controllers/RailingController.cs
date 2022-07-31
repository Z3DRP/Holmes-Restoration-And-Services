using Holmes_Services.Data_Access.Repos;
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
    public class RailingController : Controller
    {
        private HolmesServiceUnit data { get; set; }
        private Repo<Railing> Ctx { get; set; }
        public RailingController(HolmesContext ctx)
        {
            data = new HolmesServiceUnit(ctx);
            Ctx = new Repo<Railing>(ctx);
        }

        public IActionResult Index()
        {
            return View();
        }
        private RailSession GetRailing()
        {
            var rail = new RailSession(HttpContext);
            rail.Load(Ctx);
            return rail;
        }
        public ViewResult List()
        {
            IEnumerable<Railing> rails = RailRepo.GetAllRails();
            return rails == null ? View(Enumerable.Empty<Railing>()) : View(rails);
        }
        public ViewResult Details(int id)
        {
            Railing rail = RailRepo.GetRailById(id);
            return rail == null ? View(new Railing()) : View(rail);
        }
        [HttpPost]
        public RedirectToActionResult Filter(string[] filter, bool clear = false)
        {
            var builder = new RailingGridBuilder(HttpContext.Session);

            if (clear)
                builder.ClearFilterSegments();
            else
            {
                var type = data.RailTypes.Get(filter[0].ToInt());
                builder.CurrentRoute.PageNumber = 1;
                builder.LoadFilterSegments(filter, type);
            }

            builder.SaveRouteSegments();
            return RedirectToAction("List", builder.CurrentRoute);
        }
        [HttpPost]
        public RedirectToActionResult PageSize(int pagesize)
        {
            var builder = new RailingGridBuilder(HttpContext.Session);

            builder.CurrentRoute.PageSize = pagesize;
            builder.SaveRouteSegments();

            return RedirectToAction("List", builder.CurrentRoute);
        }
        [HttpPost]
        public RedirectToActionResult Add(int id)
        {
            Railing rail = RailRepo.GetRailById(id);

            if (rail == null)
                TempData["message"] = "Unable to add railing";
            else
            {
                RailDTO rdto = new RailDTO();
                rdto.Load(rail);
                RailItem ritem = new RailItem
                {
                    Rail = rdto,
                    Price = rail.Price_Per_SqFt
                };

                RailSession rsession = GetRailing();

                if (rsession.Count > 0)
                    return RedirectToAction("Edit", ritem);
                else
                {
                    rsession.Add(ritem);
                    rsession.Save();
                    TempData["message"] = $"{rail.Name} has been added";
                }
            }

            return RedirectToAction("List");
        }

        [HttpPost]
        public RedirectToActionResult Remove(int id)
        {
            RailSession rsesh = GetRailing();
            RailItem? ritem = rsesh.GetById(id);

            if (ritem != null)
            {
                rsesh.Remove(ritem);
                rsesh.Save();
                TempData["message"] = $"{ritem.Rail.Name} removed from design";
            }
            else
                TempData["message"] = $"Unable to remove railing from design";

            return RedirectToAction("List");
        }

        [HttpPost]
        public RedirectToActionResult Clear()
        {
            RailSession rsesh = GetRailing();
            rsesh.Clear();
            rsesh.Save();

            TempData["message"] = "Design cleared";
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            RailSession rsesh = GetRailing();
            RailItem? ritem = rsesh.GetById(id);
            if (ritem == null)
            {
                TempData["message"] = "Unable to locate railing";
                return RedirectToAction("List");
            }
            else
                return View(ritem);
        }
        [HttpPost]
        public RedirectToActionResult Edit(RailItem railItem)
        {
            RailSession rsesh = GetRailing();
            rsesh.Edit(railItem);
            rsesh.Save();

            TempData["message"] = $"{railItem.Rail.Name} updated";
            return RedirectToAction("Index");
        }
        public ViewResult Checkout() => View();
    }
}
