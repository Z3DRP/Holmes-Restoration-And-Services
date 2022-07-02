using Holmes_Services.Models.DTOs;
using Holmes_Services.Models.Extensions;

namespace Holmes_Services.Models.RouteDictionaries
{
    public static class FilterPrefix
    {
        public const string Type = "type-";
        public const string Price = "price-";
        public const string Group = "group-";
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
        public void ClearFilters() =>
            DeckTypeFilter = RailTypeFilter = DeckPriceFilter = RailPriceFilter = RailingGridDTO.DefaultFilter;

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
