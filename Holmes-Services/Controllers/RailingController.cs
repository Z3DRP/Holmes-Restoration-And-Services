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
        public ViewResult List(RailingGridDTO rails)
        {
            var builder = new RailingGridBuilder(HttpContext.Session, rails, defaultSortField: nameof(Railing.Name));

            var options = new RailQueryOptions
            {
                Includes = "Type.Type, Groups.Group_Name",
                OrderByDirection = builder.CurrentRoute.SortDirection,
                PageNumber = builder.CurrentRoute.PageNumber,
                PageSize = builder.CurrentRoute.PageSize
            };

            options.SortFilter(builder);

            var vm = new RailListViewModel
            {
                Rails = data.Rails.List(options),
                Types = data.RailTypes.List(new QueryOptions<Rail_Type>
                {
                    OrderBy = r => r.Type
                }),
                Groups = data.Groups.List(new QueryOptions<Price_Groups>
                {
                    OrderBy = p => p.Group_Name
                }),
                CurrentRoute = builder.CurrentRoute,
                TotalPages = builder.GetTotalPages(data.Rails.Count)
            };

            return View(vm);
        }
        public ViewResult Details(int id)
        {
            var rail = data.Rails.Get(new QueryOptions<Railing>
            {
                Includes = "Type.Type, Groups.Group_Name",
                Where = r => r.Id == id
            });

            return View(rail);
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
            var rail = Ctx.Get(new QueryOptions<Railing>
            {
                Includes = "Type.Type, Group.Group_Name",
                Where = r => r.Id == id
            });
            if (rail == null)
                TempData["message"] = "Unable to add rail to design";
            else
            {
                var rdto = new RailDTO();
                rdto.Load(rail);
                RailItem railitem = new RailItem
                {
                    Rail = rdto,
                    Price = rdto.Price
                };

                RailSession rsesh = GetRailing();
                rsesh.Add(railitem);
                rsesh.Save();
                TempData["message"] = $"{rail.Name} added to design";
            }

            var builder = new RailingGridBuilder(HttpContext.Session);
            return RedirectToAction("List", "Rail", builder.CurrentRoute);
        }
        [HttpPost]
        public RedirectToActionResult Remove(int id)
        {
            RailSession rsesh = GetRailing();
            RailItem ritem = rsesh.GetById(id);
            rsesh.Remove(ritem);
            rsesh.Save();

            TempData["message"] = $"{ritem.Rail.Name} removed from design";
            return RedirectToAction("Index");
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
            RailItem ritem = rsesh.GetById(id);
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
