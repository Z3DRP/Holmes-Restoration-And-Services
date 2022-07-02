using Holmes_Services.Models.DomainModels;
using Holmes_Services.Models.Grids;

namespace Holmes_Services.Models.QueryOptions
{
    public class DeckQueryOptions : QueryOptions<Decking>
    {
        public enum PriceGroups { A, B, C }

        public void SortFilter(DeckingGridBuilder builder)
        {
            // filter
            if (builder.IsFilterByType)
                Where = t => t.Type.Type == builder.CurrentRoute.DeckTypeFilter;
            if (builder.IsFilteredByPrice)
            {
                if (builder.CurrentRoute.DeckPriceFilter == PriceGroups.A.ToString())
                    Where = p => p.Price_Per_SqFt == 75;
                if (builder.CurrentRoute.DeckPriceFilter == PriceGroups.B.ToString())
                    Where = p => p.Price_Per_SqFt == 100;
                else
                    Where = p => p.Price_Per_SqFt > 100;
            }
            
            // sort
            if (builder.IsSortedByByType)
                OrderBy = t => t.Type;
            else if (builder.IsSortedByGroup)
                OrderBy = g => g.Group;
            else if (builder.IsSortedByPrice)
                OrderBy = p => p.Price_Per_SqFt;
            else
                OrderBy = i => i.Id;
        }
    }
}
