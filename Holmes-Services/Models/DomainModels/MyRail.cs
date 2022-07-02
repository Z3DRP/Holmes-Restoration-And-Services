using Holmes_Services.Models.DTOs;
using Holmes_Services.Models.Extensions;
using Holmes_Services.Models.ModelListExtensions;
using Holmes_Services.Models.QueryOptions;
using Holmes_Services.Models.Repositories;

namespace Holmes_Services.Models.DomainModels
{
    public class MyRail
    {
        private const string RailKey = "myrail";
        private const string CountKey = "mycount";

        private List<RailItem> items { get; set; }
        private List<RailItemDTO> storedItems { get; set; }

        private ISession session { get; set; }
        private IRequestCookieCollection requestCookies { get; set; }
        private IResponseCookies responseCookies { get; set; }

        public MyRail(HttpContext rtx)
        {
            session = rtx.Session;
            requestCookies = rtx.Request.Cookies;
            responseCookies = rtx.Response.Cookies;
        }
        public void Load(Repo<Railing> railData)
        {
            items = session.GetObject<List<RailItem>>(RailKey);

            if (items == null)
            {
                items = new List<RailItem>();
                storedItems = requestCookies.GetObject<List<RailItemDTO>>(RailKey);
            }

            if (storedItems?.Count > items?.Count)
            {
                foreach (RailItemDTO storedItem in storedItems)
                {
                    var rail = railData.Get(new QueryOptions<Railing>
                    {
                        Includes = "Type, Group",
                        Where = r => r.Id == storedItem.RailId
                    });

                    if (rail != null)
                    {
                        var dto = new RailDTO();
                        dto.Load(rail);

                        RailItem item = new RailItem { Rail = dto };
                        items.Add(item);
                    }
                }
                Save();
            }
        }
        public int? Count => session.GetInt32(CountKey) ?? requestCookies.GetInt32(CountKey);
        public IEnumerable<RailItem> List => items;

        public RailItem GetById(int id) =>
            items.FirstOrDefault(rid => rid.Rail.RailId == id);

        // if user clicks add to design and there is already a railing in session
        // delete old railing and add new railing
        public void Add(RailItem item)
        {
            if (this.Count > 0)
            {
                items.Clear();
                items.Add(item);
            }
            else
                items.Add(item);
        }
        public void Remove(RailItem item) => items.Remove(item);
        public void Clear() => items.Clear();

        public void Save()
        {
            if (items.Count == 0)
            {
                session.Remove(RailKey);
                session.Remove(CountKey);
                responseCookies.Delete(RailKey);
                responseCookies.Delete(CountKey);
            }
            else
            {
                session.SetObject<List<RailItem>>(RailKey, items);
                session.SetInt32(CountKey, items.Count);
                responseCookies.SetObject<List<RailItemDTO>>(RailKey, items.ToDTO());
                responseCookies.SetInt32(CountKey, items.Count);
            }
        }
    }
}
