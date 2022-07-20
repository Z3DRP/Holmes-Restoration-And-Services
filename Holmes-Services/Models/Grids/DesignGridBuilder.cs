using Holmes_Services.Models.DomainModels;
using Holmes_Services.Models.DTOs;
using Holmes_Services.Models.Extensions;
using Holmes_Services.Models.RouteDictionaries;
namespace Holmes_Services.Models.Grids
{
    public class DesignGridBuilder : GridBuilder
    {
        public DesignGridBuilder(ISession sesh) : base(sesh) { }
        public DesignGridBuilder(ISession sesh, DesignGridDTO vals,
            string defaultSortField) : base(sesh, vals, defaultSortField)
        {
            bool isInitial = vals.Pattern.IndexOf(FilterPrefix.Pattern) == -1;
            routes.DesignPatternFilter = (isInitial) ? FilterPrefix.Pattern + vals.Pattern : vals.Pattern;
            routes.DesignPriceFilter = (isInitial) ? FilterPrefix.Price + vals.Price : vals.Price;
            routes.DesignDeckGroupFilter = (isInitial) ? FilterPrefix.DeckGroup + vals.DeckGroup : vals.DeckGroup;
            routes.DesignRailGroupFilter = (isInitial) ? FilterPrefix.RailGroup + vals.RailGroup : vals.RailGroup;
            routes.DesignStartDateFilter = (isInitial) ? FilterPrefix.StartDate + vals.StartDate : vals.StartDate;
            routes.DesignDeckFilter = (isInitial) ? FilterPrefix.Deck + vals.Deck : vals.Deck;
            routes.DesignRailFitler = (isInitial) ? FilterPrefix.Rail + vals.Rail : vals.Rail;

            SaveRouteSegments();
        }
        public void LoadFilterSegments(string[] filter, Pattern pattern)
        {
            routes.DesignPatternFilter = FilterPrefix.Pattern + filter[0];
            routes.DesignPriceFilter = FilterPrefix.Price + filter[1];
            routes.DesignDeckGroupFilter = FilterPrefix.DeckGroup + filter[2];
            routes.DesignRailGroupFilter = FilterPrefix.RailGroup + filter[3];
            routes.DesignStartDateFilter = FilterPrefix.StartDate + filter[4];
            routes.DesignDeckFilter = FilterPrefix.Deck + filter[5];
            routes.DesignRailFitler = FilterPrefix.Rail + filter[6];
        }
        public void ClearFilterSegments() => routes.ClearDesignFilters();

        // filter flags
        string def = DesignGridDTO.DefaultFilter;
        public bool IsFilteredByPattern => routes.DesignPatternFilter != def;
        public bool IsFilteredByPrice => routes.DesignPriceFilter != def;
        public bool IsFilteredByDeckGroup => routes.DesignDeckGroupFilter != def;
        public bool IsFilteredByRailGroup => routes.DesignRailGroupFilter != def;
        public bool IsFilteredByStart => routes.DesignStartDateFilter != def;
        public bool IsFilteredByDeck => routes.DesignDeckFilter != def;
        public bool IsFilteredByRail => routes.DesignRailFitler != def;
        // sort flags
        public bool IsSortedByPattern => routes.SortField.EqualsNoCase(nameof(Design.Pattern));
        public bool IsSortedByPrice => routes.SortField.Equals(nameof(Design.Estimate));
        public bool IsSortedByDeckGroup => routes.SortField.EqualsNoCase(nameof(Price_Groups.Group_Name));
        public bool IsSortedByRailGroup => routes.SortField.EqualsNoCase(nameof(Price_Groups.Group_Name));
        public bool IsSortedByStart => routes.SortField.Equals(nameof(Design.Start_Date));
        public bool IsSortedByDeck => routes.SortField.EqualsNoCase(nameof(Design.Deck));
        public bool IsSortedByRail => routes.SortField.EqualsNoCase(nameof(Design.Rail));
    }
}
