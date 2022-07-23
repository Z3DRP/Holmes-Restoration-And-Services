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
        private HolmesContext db { get; set; }
        public DesignController(HolmesContext ctx)
        {
            data = new HolmesServiceUnit(ctx);
            Ctx = new Repo<Design>(ctx);
            db = ctx;
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
            {
                Includes = "Customer, Jobs",
                OrderByDirection = builder.CurrentRoute.SortDirection,
                PageNumber = builder.CurrentRoute.PageNumber,
                PageSize = builder.CurrentRoute.PageSize
            };
            options.SortFilter(builder);
            var vm = new DesignListViewModel
            {
                Designs = data.Designs.List(options),
                CurrentRoute = builder.CurrentRoute,
                TotalPages = builder.GetTotalPages(data.Designs.Count),
            };

            return View(vm);
        }
        public ViewResult Details(int id)
        {
            var design = data.Designs.Get(new QueryOptions<Design>
            {
                Where = d => d.Id == id
            });

            return View(design);
        }
        [HttpPost]
        public RedirectToActionResult Add(int id)
        {
            var design = Ctx.Get(new QueryOptions<Design>
            {
                Where = d => d.Id == id
            });

            if (design == null)
                TempData["message"] = "Unable to add design";
            else
            {
                DesignDTO ddto = new DesignDTO();
                ddto.Load(design);
                DesignItem ditem = new DesignItem
                {
                    Design = ddto,
                    Price = ddto.Estimate
                };

                db.Designs.Add(design);
                db.SaveChanges();
                DesignSession dsesh = new DesignSession(HttpContext);
                dsesh.Add(ditem);
                dsesh.Save();

                TempData["message"] = "Design has been added";

            }
            var builder = new DesignGridBuilder(HttpContext.Session);
            return RedirectToAction("List", "Design", builder.CurrentRoute);
        }
        [HttpGet]
        public IActionResult Remove(int id)
        {
            var design = db.Designs.FirstOrDefault(d => d.Id == id);
            TempData["message"] = "Delete Design";
            // add a view model
            return View(design);

        }
        [HttpPost]
        public RedirectToActionResult Remove(Design design)
        {
            DesignSession dsesh = GetDesign();
            DesignItem? ditem = dsesh.GetById(design.Id);
            if (ditem != null)
            {
                db.Designs.Remove(design);
                db.SaveChanges();
                dsesh.Remove(ditem);
                dsesh.Save();
                TempData["message"] = "Design has been removed";
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public RedirectToActionResult Clear()
        {
            DesignSession dsesh = GetDesign();
            dsesh.Clear();
            dsesh.Save();

            TempData["message"] = "Design cleared";
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var design = db.Designs.FirstOrDefault(d => d.Id == id);

            if (design == null)
            {
                TempData["message"] = "Unable to locate design";
                return RedirectToAction("List");
            }
            else
            {
                return View(design);
            }
        }
        [HttpPost]
        public IActionResult Edit(Design design)
        {
            if (ModelState.IsValid)
            {
                DesignSession dsesh = GetDesign();
                DesignItem ditem = dsesh.GetById(design.Id);
                dsesh.Edit(ditem);
                dsesh.Save();
                db.Designs.Update(design);
                db.SaveChanges();
                TempData["message"] = "Design updated";
                return RedirectToAction("Index", "Home");
            }
            else
                return View(design);
        }
    }
}
