namespace Holmes_Services.Models.Extensions
{
    public static class QueryExtension
    {
        public static IQueryable<T> PageBy<T>(this IQueryable<T> items, int pagenumber, int pagesize)
        {
            return items.Skip((pagenumber - 1) * pagesize)
                .Take(pagesize);
        }
    }
}
