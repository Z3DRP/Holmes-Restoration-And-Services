using Holmes_Services.Models.DTOs;
using Holmes_Services.Models.Extensions;
using Holmes_Services.Models.ModelListExtensions;
using Holmes_Services.Models.QueryOptions;
using Holmes_Services.Models.Repositories;

namespace Holmes_Services.Models.DomainModels
{
    public class MyDeck
    {
        private const string DeckKey = "mydeck";
        private const string CountKey = "dcount";

        private List<DeckItem> items { get; set; }
        private List<DeckItemDTO> storedItems { get; set; }
        private ISession session { get; set; }
        private IRequestCookieCollection requestCookies { get; set; }
        private IResponseCookies responseCookies { get; set; }

        public MyDeck(HttpContext dtx)
        {
            session = dtx.Session;
            requestCookies = dtx.Request.Cookies;
            responseCookies = dtx.Response.Cookies;
        }
        public void Load(Repo<Decking> deckData)
        {
            items = session.GetObject<List<DeckItem>>(DeckKey);

            if (items == null)
            {
                items = new List<DeckItem>();
                storedItems = requestCookies.GetObject<List<DeckItemDTO>>(DeckKey);
            }

            if (storedItems?.Count > items?.Count)
            {
                foreach (DeckItemDTO storedItem in storedItems)
                {
                    var deck = deckData.Get(new QueryOptions<Decking>
                    {
                        Includes = "Type, Group",
                        Where = d => d.Id == storedItem.DeckId
                    });

                    if (deck != null)
                    {
                        var dto = new DeckDTO();
                        dto.Load(deck);

                        DeckItem ditem = new DeckItem
                        {
                            Deck = dto
                        };
                        items.Add(ditem);
                    }
                }
                Save();
            }
        }
        public int? Count => session.GetInt32(CountKey) ?? requestCookies.GetInt32(CountKey);
        public IEnumerable<DeckItem> List => items;
        public DeckItem GetById(int id) =>
            items.FirstOrDefault(did => did.Deck.DeckId == id);

        // if user clicks add to design and there is already a deck stored in the session
        // delete old deck and replace with the newly selected deck
        public void Add(DeckItem item)
        {
            if (this.Count > 0)
            {
                items.Clear();
                items.Add(item);
            }
            else
                items.Add(item);
        }
        public void Edit(DeckItem item)
        {
            var itemInDesign = GetById(item.Deck.DeckId);

            if (itemInDesign != null)
            {

            }
        }
        public void Remove(DeckItem item) => items.Remove(item);
        public void Clear() => items.Clear();

        public void Save()
        {
            if (items.Count == 0)
            {
                session.Remove(DeckKey);
                session.Remove(CountKey);
                responseCookies.Delete(DeckKey);
                responseCookies.Delete(CountKey);
            }
            else
            {
                session.SetObject<List<DeckItem>>(DeckKey, items);
                session.SetInt32(CountKey, items.Count);
                responseCookies.SetObject<List<DeckItemDTO>>(DeckKey, items.ToDTO());
                responseCookies.SetInt32(CountKey, items.Count);
            }
        }
    }
}
