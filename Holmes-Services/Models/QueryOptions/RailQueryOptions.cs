using Holmes_Services.Models.DomainModels;
using Holmes_Services.Models.Extensions;
using Holmes_Services.Models.Grids;

namespace Holmes_Services.Models.QueryOptions
{
    public class RailQueryOptions : QueryOptions<Railing>
    {
        public enum PriceGroups { A, B, C }
        public void SortFilter(RailingGridBuilder builder)
        {
            if (builder.IsFilteredByType)
            {
                Where = t => t.Type.Type == builder.CurrentRoute.RailTypeFilter;
            }
            if (builder.IsFilteredByPrice)
            {
                if (builder.CurrentRoute.DeckPriceFilter == PriceGroups.A.ToString())
                    Where = p => p.Price_Per_SqFt == 75;
                else if (builder.CurrentRoute.DeckPriceFilter == PriceGroups.B.ToString())
                    Where = p => p.Price_Per_SqFt == 100;
                else
                    Where = p => p.Price_Per_SqFt > 100;
            }
            if (builder.IsFilteredByGroup)
            {
                int id = builder.CurrentRoute.RailGroupFilter.ToInt();
                if (id > 0)
                    Where = r => r.Group.GroupId == id;
            }
            // sort
            if (builder.IsSortedByType)
                OrderBy = t => t.Type.Type;
            else if (builder.IsSortedByPrice)
                OrderBy = p => p.Price_Per_SqFt;
            else
                OrderBy = g => g.Group;
           

        }
    }
}
