using Holmes_Services.Models.DTOs;
using Holmes_Services.Models.Extensions;
using Holmes_Services.Models.RouteDictionaries;

namespace Holmes_Services.Models.Grids
{
    public class GridBuilder
    {
        protected const string RouteKey = "currentroute";

        protected RouteDictionary routes { get; set; }
        protected ISession session { get; set; }

        // constructor used when just need to get current route data from sesh
        public GridBuilder(ISession sesh)
        {
            session = sesh;
            routes = session.GetObject<RouteDictionary>(RouteKey) ?? new RouteDictionary();
        }
        // constructor used when need to store new paging and sorting route segments
        public GridBuilder(ISession sesh, GridDTO values, string defaultSortField)
        {
            session = sesh;
            routes = new RouteDictionary(); // clear previous route segment
            routes.PageNumber = values.PageNumber;
            routes.SortField = values.SortField ?? defaultSortField;
            routes.SortDirection = values.SortDirection;

            SaveRouteSegments();
        }

        public void SaveRouteSegments() =>
            session.SetObject<RouteDictionary>(RouteKey, routes);
        public int GetTotalPages(int count)
        {
            int size = routes.PageSize;
            return (count + size - 1) / size;
        }
        public RouteDictionary CurrentRoute => routes;
    }
}
