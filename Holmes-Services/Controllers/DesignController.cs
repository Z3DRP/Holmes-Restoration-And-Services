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
        public ViewResult List()
        {
            IEnumerable<Design> designs = DesignRepo.GetAllDesigns();
            return designs == null ? View(Enumerable.Empty<Design>()) : View(designs);
        }
        public ViewResult Details(int id)
        {
            Design design = DesignRepo.GetDesignById(id);
            return design == null ? View(new Design()) : View(design);
        }
        [HttpPost]
        public RedirectToActionResult Add(int id)
        {
            Design design = DesignRepo.GetDesignById(id);

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

                DesignSession dsesh = new DesignSession(HttpContext);

                if (dsesh.Count > 0)
                    return RedirectToAction("Edit", ditem);
                else
                {
                    dsesh.Add(ditem);
                    dsesh.Save();
                    TempData["message"] = "Design has been added";
                }
            }
            // might need to redirect some where else
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Remove(int id)
        {
            bool wasDeleted, doesExist = DesignRepo.VerifyDesignById(id);
            
            if (doesExist)
            {
                wasDeleted = DesignRepo.DeleteDesign(id);

                if (wasDeleted)
                    TempData["message"] = "Your design was deleted";
                else
                    TempData["message"] = "An error occured while deleting design";
                // need to redirect to " My Designs " or something
                return RedirectToAction("List");
            }
            else
                TempData["message"] = "Design does not exist";

            // need to redirect to " My Designs " or something
            return RedirectToAction("List");

        }
        [HttpGet]
        public RedirectToActionResult Remove(Design design)
        {
            DesignSession dsesh = GetDesign();
            DesignItem? ditem = dsesh.GetById(design.Id);
            if (ditem != null)
            {
                dsesh.Remove(ditem);
                dsesh.Save();
                TempData["message"] = "Design has been removed";
            }
            else
                TempData["message"] = "Unable to remove design";

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
            var design = DesignRepo.GetDesignById(id);

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
                DesignItem? ditem = dsesh.GetById(design.Id);

                if (ditem != null)
                {
                    dsesh.Edit(ditem);
                    dsesh.Save();
                    bool added = DesignRepo.UpdateDesign(design);
                    TempData["message"] = "Design updated";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["message"] = "Design Id Not Found";
                    return View(design);
                }
            }
            else
                return View(design);
        }
    }
}
