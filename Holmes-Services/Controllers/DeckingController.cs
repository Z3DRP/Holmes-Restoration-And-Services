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
            // get grid builder which loads route segment values and stores them in session
            var builder = new DeckingGridBuilder(HttpContext.Session, decks, defaultSortField: nameof(Decking.Name));

            // create a DeckQueryOption object to build a query expression for a page of data
            var options = new DeckQueryOptions
            {
                Includes = "Type.Type, Group.Group_Name",
                OrderByDirection = builder.CurrentRoute.SortDirection,
                PageNumber = builder.CurrentRoute.PageNumber,
                PageSize = builder.CurrentRoute.PageSize
            };

            // call the SortFilter method of the DeckQueryOpt object and pass it the builder
            // object it uses the route information and the properties of the builder object to
            // add sort and filter options to the query expression
            options.SortFilter(builder);

            // creat view model and add page of book data, data for drop downs
            // the current route and the total number of pages
            var vm = new DeckListViewModel
            {
                Decks = data.Decks.List(options),
                Types = data.DeckTypes.List(new QueryOptions<Deck_Type>
                {
                    OrderBy = t => t.Type
                }),
                Groups = data.Groups.List(new QueryOptions<Price_Groups>
                {
                    OrderBy = g => g.Group_Name
                }),
                CurrentRoute = builder.CurrentRoute,
                TotalPages = builder.GetTotalPages(data.Decks.Count)
            };

            return View(vm);
        }
        public ViewResult Details(int id)
        {
            var deck = data.Decks.Get(new QueryOptions<Decking>
            {
                Includes = "Type.Type, Group.Group_Name",
                Where = d => d.Id == id
            });

            return View(deck);
        }

        [HttpPost]
        public RedirectToActionResult Filter(string[] filter, bool clear = false)
        {
            // get current route segments from session
            var builder = new DeckingGridBuilder(HttpContext.Session);

            // clear or update filter route segment values if update get author data
            // type slug to type filter value
            if (clear)
                builder.ClearFilterSegments();
            else
            {
                var type = data.DeckTypes.Get(filter[0].ToInt());
                builder.CurrentRoute.PageNumber = 1;
                builder.LoadFilterSegments(filter, type);
            }
            // save route data back to session and redirect to tdeck list action method
            // passing dictionary of route segment values to build URL
            builder.SaveRouteSegments();
            return RedirectToAction("List", builder.CurrentRoute);
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
            Decking deck = Ctx.Get(new QueryOptions<Decking>
            {
                Includes = "Type.Type, Group.Group_Name ",
                Where = d => d.Id == id
            });

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
                dsesh.Add(ditem);
                dsesh.Save();

                TempData["message"] = $"{deck.Name} added to design";
            }
            var builder = new DeckingGridBuilder(HttpContext.Session);
            return RedirectToAction("List", "Deck", builder.CurrentRoute);
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
            return RedirectToAction("Index");
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
            DeckItem ditem = dsesh.GetById(id);
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
