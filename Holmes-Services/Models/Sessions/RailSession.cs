using Holmes_Services.Models.DomainModels;
using Holmes_Services.Models.DTOs;
using Holmes_Services.Models.Extensions;
using Holmes_Services.Models.ModelListExtensions;
using Holmes_Services.Models.QueryOptions;
using Holmes_Services.Models.Repositories;

namespace Holmes_Services.Models.Sessions
{
    public class RailSession
    {
        public const string RailKey = "rail";
        public const string CountKey = "count";

        private List<RailItem> rails { get; set; }
        private List<RailItemDTO> storedRails { get; set; }
        private ISession session { get; set; }
        private IRequestCookieCollection requestCookies { get; set; }
        private IResponseCookies responseCookies { get; set; }

        public RailSession(HttpContext ctx)
        {
            session = ctx.Session;
            requestCookies = ctx.Request.Cookies;
            responseCookies = ctx.Response.Cookies;
        }
        public void Load(Repo<Railing> data)
        {
            rails = session.GetObject<List<RailItem>>(RailKey);
            if (rails == null)
            {
                rails = new List<RailItem>();
                storedRails = requestCookies.GetObject<List<RailItemDTO>>(RailKey);
            }
            foreach(RailItemDTO storedRail in storedRails)
            {
                Railing rail = data.Get(new QueryOptions<Railing>
                {
                    Includes = "Type.Type, Group.Group_Name",
                    Where = r => r.Id == storedRail.RailId
                });
                if (rail != null)
                {
                    RailDTO railDto = new RailDTO();
                    railDto.Load(rail);

                    RailItem rItem = new RailItem
                    {
                        Rail = railDto,
                        Price = railDto.Price
                    };
                    rails.Add(rItem);
                }
            }
            Save();
        }
        public void AddToDesign(Railing rail)
        {
            RailDTO rdto = new RailDTO();
            rdto.Load(rail);

            RailItem ritem = new RailItem
            {
                Rail = rdto,
                Price = rdto.Price
            };

            if (Count > 0)
                Edit(ritem, true);
            else
                rails.Add(ritem);

        }
        public void Add(RailItem railItem)
        {
            var rail = GetById(railItem.Rail.RailId);

            if (rail == null)
                rails.Add(railItem);
            else
            {
                if (Count > 0)
                    Edit(railItem, true);
            }
        }
        public int? Count => session.GetInt32(CountKey) ?? requestCookies.GetInt32(CountKey);
        public IEnumerable<RailItem> List => rails;
        public RailItem? GetById(int id) => rails.FirstOrDefault(ri => ri.Rail.RailId == id);
        public void Edit(RailItem rail, bool countTested=false)
        {
            var r = GetById(rail.Rail.RailId);
           
            if (!countTested)
            {
                if (rail != null)
                {
                    rails.Clear();
                    rails.Add(rail);
                }
            }
            else
            {
                rails.Clear();
                rails.Add(rail);
            }
        }
        public void Remove(RailItem rail) => rails.Remove(rail);
        public void Clear() => rails.Clear();
        public void Save()
        {
            if (rails.Count == 0)
            {
                session.Remove(RailKey);
                session.Remove(CountKey);
                responseCookies.Delete(RailKey);
                responseCookies.Delete(CountKey);
            }
            else
            {
                session.SetObject<List<RailItem>>(RailKey, rails);
                session.SetInt32(CountKey, rails.Count);
                responseCookies.SetObject<List<RailItemDTO>>(RailKey, rails.ToDTO());
                responseCookies.SetInt32(CountKey, rails.Count);
            }
        }
    }
}
