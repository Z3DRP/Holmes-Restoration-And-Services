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
using Holmes_Services.Data_Access.Repos;

namespace Holmes_Services.Controllers
{
    public class DeckingController : Controller
    {
        private HolmesServiceUnit data { get; set; }
        private Repo<Decking> Ctx { get; set; }
        public DeckingController(HolmesContext ctx)
        {
            data = new HolmesServiceUnit(ctx);
            Ctx = new Repo<Decking>(ctx);
        }
        public IActionResult Index()
        {
            return View();
        }
        private DeckSession GetDecking()
        {
            var deck = new DeckSession(HttpContext);
            deck.Load(Ctx);
            return deck;
        }
        public ViewResult List(DeckingGridDTO decks)
        {
            IEnumerable<Decking> deckings = DeckRepo.GetAllDecks();

            return deckings == null ? View(Enumerable.Empty<Decking>()) : View(deckings);
        }
        public ViewResult Details(int id)
        {
            Decking deck = DeckRepo.GetDeckById(id);
            return deck == null ? View(Enumerable.Empty<Decking>()) : View(deck);
        }

        [HttpPost]
        public RedirectToActionResult PageSize(int pagesize)
        {
            var builder = new DeckingGridBuilder(HttpContext.Session);

            builder.CurrentRoute.PageSize = pagesize;
            builder.SaveRouteSegments();

            return RedirectToAction("List", builder.CurrentRoute);
        }
        [HttpPost]
        public RedirectToActionResult Add(int id)
        {
            Decking deck = DeckRepo.GetDeckById(id);

            if (deck == null)
                TempData["message"] = "Unable to add deck";
            else
            {    
                DeckDTO ddto = new DeckDTO();
                ddto.Load(deck);
                DeckItem ditem = new DeckItem
                {
                    Deck = ddto,
                    Price = ddto.Price
                };

                DeckSession dsesh = GetDecking();

                if (dsesh.Count > 0)
                    return RedirectToAction("Edit", ditem);
                else
                {
                    dsesh.Add(ditem);
                    dsesh.Save();
                    TempData["message"] = $"{deck.Name} added to design";
                }
            }

            return RedirectToAction("List");
        }

        [HttpPost]
        public RedirectToActionResult Remove(int id)
        {
            DeckSession dsesh = GetDecking();
            DeckItem? ditem = dsesh.GetById(id);
            if (ditem != null)
            {
                dsesh.Remove(ditem);
                dsesh.Save();
                TempData["message"] = $"{ditem.Deck.Name} removed from design";
            }
            else
                TempData["message"] = "Unable to remove decking from design";

            return RedirectToAction("List");
        }

        [HttpPost]
        public RedirectToActionResult Clear()
        {
            DeckSession dsesh = GetDecking();
            dsesh.Clear();
            dsesh.Save();

            TempData["message"] = "Design cleared";
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            DeckSession dsesh = GetDecking();
            DeckItem? ditem = dsesh.GetById(id);
            if (ditem == null)
            {
                TempData["message"] = "Unable to locate decking";
                return RedirectToAction("List");
            }
            else
            {
                return View(ditem);
            }
        }
        [HttpPost]
        public RedirectToActionResult Edit(DeckItem deckItem)
        {
            DeckSession dsesh = GetDecking();
            dsesh.Edit(deckItem);
            dsesh.Save();

            TempData["message"] = $"{deckItem.Deck.Name} updated";
            return RedirectToAction("Index");
        }
        public ViewResult Checkout() => View();
    }
}
