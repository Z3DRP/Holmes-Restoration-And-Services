using Holmes_Services.Models.DTOs;
using Holmes_Services.Models.Extensions;

namespace Holmes_Services.Models.RouteDictionaries
{
    public static class FilterPrefix
    {
        public const string DefaultFilter = "all";
        public const string Type = "type-";
        public const string Price = "price-";
        public const string Group = "group-";
        public const string RailGroup = "rail-group-";
        public const string DeckGroup = "deck-group-";
        public const string Pattern = "pattern-";
        public const string StartDate = "startdate-";
        public const string Deck = "deck-";
        public const string Rail = "rail-";
    }

    public class RouteDictionary : Dictionary<string, string>
    {
        public int PageNumber
        {
            get => Get(nameof(GridDTO.PageNumber)).ToInt();
            set => this[nameof(GridDTO.PageNumber)] = value.ToString();
        }
        public int PageSize
        {
            get => Get(nameof(GridDTO.PageSize)).ToInt();
            set => this[nameof(GridDTO.PageSize)] = value.ToString();
        }
        public string SortField
        {
            get => Get(nameof(GridDTO.SortField));
            set => this[nameof(GridDTO.SortField)] = value;
        }
        public string SortDirection
        {
            get => Get(nameof(GridDTO.SortDirection));
            set => this[nameof(GridDTO.SortDirection)] = value;
        }
        public void SetSortAndDirection(string fieldName, RouteDictionary current)
        {
            if (current.SortField.EqualsNoCase(fieldName) && current.SortDirection == "asc")
                this[nameof(GridDTO.SortDirection)] = "desc";
            else
                this[nameof(GridDTO.SortDirection)] = "asc";
        }
        public string DeckTypeFilter
        {
            get => Get(nameof(DeckingGridDTO.Type))?.Replace(FilterPrefix.Type, "");
            set => this[nameof(DeckingGridDTO.Type)] = value;
        }
        public string DeckGroupFilter
        {
            get => Get(nameof(DeckingGridDTO.Group))?.Replace(FilterPrefix.Group, "");
            set => this[nameof(DeckingGridDTO.Group)] = value;
        }
        public string DeckPriceFilter
        {
            get => Get(nameof(DeckingGridDTO.Price))?.Replace(FilterPrefix.Price, "");
            set => this[nameof(DeckingGridDTO.Price)] = value;
        }
        public string RailTypeFilter
        {
            get => Get(nameof(RailingGridDTO.Type))?.Replace(FilterPrefix.Type, "");
            set => this[nameof(RailingGridDTO.Type)] = value;
        }
        public string RailGroupFilter
        {
            get => Get(nameof(RailingGridDTO.Group))?.Replace(FilterPrefix.Group, "");
            set => this[nameof(RailingGridDTO.Group)] = value;
        }
        public string RailPriceFilter
        {
            get => Get(nameof(RailingGridDTO.Price))?.Replace(FilterPrefix.Price, "");
            set => this[nameof(RailingGridDTO.Price)] = value;
        }
        public string DesignPatternFilter
        {
            get => Get(nameof(DesignGridDTO.Pattern))?.Replace(FilterPrefix.Pattern, "");
            set => this[nameof(DesignGridDTO.Pattern)] = value;
        }
        public string DesignPriceFilter
        {
            get => Get(nameof(DesignGridDTO.Price))?.Replace(FilterPrefix.Price, "");
            set => this[nameof(DesignGridDTO.Price)] = value;
        }
        public string DesignDeckGroupFilter
        {
            get => Get(nameof(DesignGridDTO.DeckGroup))?.Replace(FilterPrefix.DeckGroup, "");
            set => this[nameof(DesignGridDTO.DeckGroup)] = value;
        }
        public string DesignRailGroupFilter
        {
            get => Get(nameof(DesignGridDTO.RailGroup))?.Replace(FilterPrefix.RailGroup, "");
            set => this[nameof(DesignGridDTO.RailGroup)] = value;
        }
        public string DesignStartDateFilter
        {
            get => Get(nameof(DesignGridDTO.StartDate))?.Replace(FilterPrefix.StartDate, "");
            set => this[nameof(DesignGridDTO.StartDate)] = value;
        }
        public string DesignDeckFilter 
        {
            get => Get(nameof(DesignGridDTO.Deck))?.Replace(FilterPrefix.Deck, "");
            set => this[nameof(DesignGridDTO.Deck)] = value;
        }
        public string DesignRailFitler
        {
            get => Get(nameof(DesignGridDTO.Rail))?.Replace(FilterPrefix.Rail, "");
            set => this[nameof(DesignGridDTO.Rail)] = value;
        }
        public void ClearFilters() =>
            DeckTypeFilter = RailTypeFilter = DeckPriceFilter = RailPriceFilter = DeckGroupFilter = RailGroupFilter = FilterPrefix.DefaultFilter;
        public void ClearDesignFilters() => DesignPriceFilter = DesignPatternFilter = DesignDeckGroupFilter = DesignRailGroupFilter = DesignStartDateFilter = DesignDeckFilter = DesignRailFitler = FilterPrefix.DefaultFilter;

        private string Get(string key) => Keys.Contains(key) ? this[key] : null;

        // return a new dictionary that contains the same values as this dictionary.
        // needed so that pages can change the route values when calculating paging, sorting,
        // and filtering links, without changing the values of the current route
        public RouteDictionary Clone()
        {
            var clone = new RouteDictionary();
            foreach (var key in Keys)
            {
                clone.Add(key, this[key]);
            }
            return clone;
        }
    }
}
