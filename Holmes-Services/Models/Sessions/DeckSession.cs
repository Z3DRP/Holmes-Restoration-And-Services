using Holmes_Services.Models.DomainModels;
using Holmes_Services.Models.DTOs;
using Holmes_Services.Models.Extensions;
using Holmes_Services.Models.QueryOptions;
using Holmes_Services.Models.Repositories;
using Holmes_Services.Models.ModelListExtensions;

namespace Holmes_Services.Models.Sessions
{
    public class DeckSession
    {
        private const string DeckKey = "deck";
        private const string CountKey = "count";

        private List<DeckItem> decks { get; set; }
        private List<DeckItemDTO> storedDecks { get; set; }
        private Repo<Decking> data { get; set;}
        private ISession session { get; set; }
        private IRequestCookieCollection requestCookies { get; set;}
        private IResponseCookies responseCookies { get; set; }
        
        public DeckSession(HttpContext ctx)
        {
            session = ctx.Session;
            requestCookies = ctx.Request.Cookies;
            responseCookies = ctx.Response.Cookies;
        }

        public void Load(Repo<Decking> data)
        {
            decks = session.GetObject<List<DeckItem>>(DeckKey);
            if (decks == null)
            {
                decks = new List<DeckItem>();
                storedDecks = requestCookies.GetObject<List<DeckItemDTO>>(DeckKey);
            }
            foreach(DeckItemDTO storedDeck in storedDecks)
            {
                Decking deck = data.Get(new QueryOptions<Decking>
                {
                    Includes = "Types.Type, Group.Group_Name",
                    Where = d => d.Id == storedDeck.DeckId
                });
                if (deck != null)
                {
                    DeckDTO ddto = new DeckDTO();
                    ddto.Load(deck);

                    DeckItem dItem = new DeckItem
                    {
                        Deck = ddto,
                        Price = storedDeck.Price
                    };
                    decks.Add(dItem);
                }
            }
            Save();
        }

        public void AddToDesign(Decking deck)
        {
            DeckDTO ddto = new DeckDTO();
            ddto.Load(deck);

            DeckItem ditem = new DeckItem
            {
                Deck = ddto,
                Price = ddto.Price
            };

            if (Count > 0)
                Edit(ditem, true);
            else
                decks.Add(ditem);

        }
        public void Add(DeckItem item)
        {
            var deckItem = GetById(item.Deck.DeckId);

            if (deckItem == null)
                decks.Add(item);
            else
            {
                if (Count > 0)
                    Edit(item, true);
            }
        }
        public int? Count => session.GetInt32(CountKey) ?? requestCookies.GetInt32(CountKey);
        public IEnumerable<DeckItem> List => decks;
        public DeckItem? GetById(int id) => decks.FirstOrDefault(di => di.Deck.DeckId == id);
        public void Edit(DeckItem deck, bool countTested=false)
        {
            var d = GetById(deck.Deck.DeckId);
            if (!countTested)
            {
                if (d != null)
                {
                    // if there is an existing item delete item then replace
                    decks.Clear();
                    decks.Add(deck);
                }
            }
            else
            {
                decks.Clear();
                decks.Add(deck);
            }
        }
        public void Remove(DeckItem deckItem) => decks.Remove(deckItem);
        public void Clear() => decks.Clear();
        public void Save()
        {
            if (decks.Count == 0)
            {
                session.Remove(DeckKey);
                session.Remove(CountKey);
                responseCookies.Delete(DeckKey);
                responseCookies.Delete(CountKey);
            }
            else
            {
                session.SetObject<List<DeckItem>>(DeckKey, decks);
                session.SetInt32(CountKey, decks.Count);
                responseCookies.SetObject<List<DeckItemDTO>>(DeckKey, decks.ToDTO());
                responseCookies.SetInt32(CountKey, decks.Count);
            }
        }
    }
}
