using Holmes_Services.Models.DomainModels;
using Holmes_Services.Models.DTOs;
using Holmes_Services.Models.Extensions;
using Holmes_Services.Models.RouteDictionaries;

namespace Holmes_Services.Models.Grids
{
    public class RailingGridBuilder : GridBuilder
    {
        // this gets current route data from session
        public RailingGridBuilder(ISession sesh) : base(sesh) { }

        // this stores filtering route segments and the
        // paging and sorting route segments stored by the base constructor
        public RailingGridBuilder(ISession sesh, RailingGridDTO values,
            string defaultSortField) : base(sesh, values, defaultSortField)
        {
            // store filter route segments - add fileter prefixed if this is initial load
            // of page with default values ratheer than route values (route values have prefix)
            bool isInitial = values.Type.IndexOf(FilterPrefix.Type) == 1;
            routes.RailTypeFilter = (isInitial) ? FilterPrefix.Type + values.Type : values.Type;
            routes.RailPriceFilter = (isInitial) ? FilterPrefix.Price + values.Price : values.Price;
            routes.RailGroupFilter = (isInitial) ? FilterPrefix.Group + values.Group : values.Group;

            SaveRouteSegments();
        }
        public void LoadFilterSegments(string[] filter, Rail_Type type)
        {
            if (type == null)
                routes.DeckTypeFilter = FilterPrefix.Type + filter[0];
            else
                routes.DeckTypeFilter = FilterPrefix.Type + filter[0]
                    + "-" + type.Type.Slug();
            routes.RailPriceFilter = FilterPrefix.Price + filter[1];
            routes.RailGroupFilter = FilterPrefix.Group + filter[2];
        }
        public void ClearFilterSegments() => routes.ClearFilters();

        // filter flags
        string def = RailingGridDTO.DefaultFilter;
        public bool IsFilteredByType => routes.RailTypeFilter != default;
        public bool IsFilteredByPrice => routes.RailPriceFilter != default;
        public bool IsFilteredByGroup => routes.RailGroupFilter != default;
        // sort flags
        public bool IsSortedByType => routes.SortField.EqualsNoCase(nameof(Railing.Type));
        public bool IsSortedByPrice => routes.SortField.EqualsNoCase(nameof(Railing.Price_Per_SqFt));
        public bool IsSortedByGroup => routes.SortField.EqualsNoCase(nameof(Railing.Group));
    }
}
