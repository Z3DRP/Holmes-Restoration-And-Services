using Holmes_Services.Models.DomainModels;
using Holmes_Services.Models.DTOs;
using Holmes_Services.Models.Extensions;
using Holmes_Services.Models.ModelListExtensions;
using Holmes_Services.Models.QueryOptions;
using Holmes_Services.Models.Repositories;

namespace Holmes_Services.Models.Sessions
{
    public class DesignSession
    {
        private const string DesignKey = "design";
        private const string CountKey = "Count";

        private List<DesignItem> designs { get; set; }
        private List<DesignItemDTO> storedDesigns { get; set; }
        private Repo<Design> data { get; set; }
        private ISession session { get; set; }
        private IRequestCookieCollection requestCookies { get; set; }
        private IResponseCookies responseCookies { get; set; }

        public DesignSession(HttpContext ctx)
        {
            session = ctx.Session;
            requestCookies = ctx.Request.Cookies;
            responseCookies = ctx.Response.Cookies;
        }
        public void Load(Repo<Design> designRepo)
        {
            designs = session.GetObject<List<DesignItem>>(DesignKey);
            if (designs == null)
            {
                designs = new List<DesignItem>();
                storedDesigns = requestCookies.GetObject < List<DesignItemDTO>>(DesignKey);
            }
            foreach(DesignItemDTO storedDto in storedDesigns)
            {
                Design design = data.Get(new QueryOptions<Design>
                {
                    Includes = "Type.Type, Group.Group_Name",
                    Where = d => d.Id == storedDto.Id
                });
                if (design != null)
                {
                    DesignDTO ddto = new DesignDTO();
                    ddto.Load(design);

                    DesignItem ditem = new DesignItem
                    {
                        Design = ddto,
                        Price = ddto.Estimate
                    };
                    designs.Add(ditem);
                }
            }
            Save();
        }
        public void Add(Design design)
        {
            DesignDTO ddto = new DesignDTO();
            ddto.Load(design);

            DesignItem ditem = new DesignItem
            {
                Design = ddto,
                Price = ddto.Estimate
            };

            if (Count > 0)
                Edit(ditem, true);
            else
                designs.Add(ditem);
        }
        public void Add(DesignItem ditem)
        {
            var designItem = GetById(ditem.Design.DesignId);

            if (designItem == null)
                designs.Add(ditem);
            else
            {
                if (Count > 0)
                    Edit(ditem, true);
            }
        }
        public int? Count => session.GetInt32(CountKey) ?? requestCookies.GetInt32(CountKey);
        public IEnumerable<DesignItem> List => designs;
        public DesignItem? GetById(int id) => designs.FirstOrDefault(di => di.Design.DesignId == id);
        public void Edit(DesignItem design, bool countTested = false)
        {
            var d = GetById(design.Design.DesignId);
            if (!countTested)
            {
                if (d != null)
                {
                    designs.Clear();
                    designs.Add(design);
                }
            }    
            else
            {
                designs.Clear();
                designs.Add(design);
            }
        }
        public void Remove(DesignItem designItem) => designs.Remove(designItem);
        public void Clear() => designs.Clear();
        public void Save()
        {
            if (designs.Count == 0)
            {
                session.Remove(DesignKey);
                session.Remove(CountKey);
                responseCookies.Delete(DesignKey);
                responseCookies.Delete(CountKey);
            }
            else
            {
                session.SetObject<List<DesignItem>>(DesignKey, designs);
                session.SetInt32(DesignKey, designs.Count);
                responseCookies.SetObject<List<DesignItemDTO>>(DesignKey, designs.ToDTO());
                responseCookies.SetInt32(CountKey, designs.Count);
            }
        }

    }
}
