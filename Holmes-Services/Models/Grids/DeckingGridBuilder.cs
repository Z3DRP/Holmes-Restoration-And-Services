using Holmes_Services.Models.DomainModels;
using Holmes_Services.Models.DTOs;
using Holmes_Services.Models.Extensions;
using Holmes_Services.Models.RouteDictionaries;

namespace Holmes_Services.Models.Grids
{
    public class DeckingGridBuilder : GridBuilder
    {
        // constructor get current route data from session
        public DeckingGridBuilder(ISession sesh) : base(sesh) { }
        //constructor stores filetering route segments and
        // paging and sortin route segments stored by the base constructor
        public DeckingGridBuilder(ISession sesh, DeckingGridDTO values,
            string defaultSortField) : base(sesh, values, defaultSortField)
        {
            // store filter route segments - add filter prefixes if this is initial load
            // of page with defaul values rather than route values (route values have prefix)
            bool isInitial = values.Type.IndexOf(FilterPrefix.Type) == -1;
            routes.DeckTypeFilter = (isInitial) ? FilterPrefix.Type + values.Type : values.Type;
            routes.DeckPriceFilter = (isInitial) ? FilterPrefix.Price + values.Price : values.Price;
            routes.DeckGroupFilter = (isInitial) ? FilterPrefix.Group + values.Group : values.Group;

            SaveRouteSegments();
        }
        // load new filter route segments contained in a string array - add filter prefix
        // to each one 
        public void LoadFilterSegments(string[] filter, Deck_Type type)
        {
            routes.DeckTypeFilter = FilterPrefix.Type + filter[0];
            routes.DeckPriceFilter = FilterPrefix.Price + filter[1];
            routes.DeckGroupFilter = FilterPrefix.Group + filter[2];
        }
        public void ClearFilterSegments() => routes.ClearFilters();

        // filter flags
        string def = DeckingGridDTO.DefaultFilter; // get default filter value from static DTO property
        public bool IsFilterByType => routes.DeckTypeFilter != def;
        public bool IsFilteredByPrice => routes.DeckPriceFilter != def;
        public bool IsFilteredByGroup => routes.DeckGroupFilter != def;
        // sort flags
        public bool IsSortedByType => routes.SortField.EqualsNoCase(nameof(Decking.Type));
        public bool IsSortedByPrice => routes.SortField.EqualsNoCase(nameof(Decking.Price_Per_SqFt));
        public bool IsSortedByGroup => routes.SortField.EqualsNoCase(nameof(Decking.Group));
    }
}
