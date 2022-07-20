using Holmes_Services.Models.DomainModels;
using Holmes_Services.Models.Grids;

namespace Holmes_Services.Models.QueryOptions
{
    public class DesignQueryOptions : QueryOptions<Design>
    {
        public enum PriceGroups { A, B, C }
        public void SortFilter(DesignGridBuilder builder)
        {
            if (builder.IsFilteredByPattern)
                Where = p => p.Pattern.Name == builder.CurrentRoute.DesignPatternFilter;

        }
    }
}
