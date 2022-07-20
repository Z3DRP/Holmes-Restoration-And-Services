namespace Holmes_Services.Models.DTOs
{
    public class GridDTO
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 7;
        public string SortField { get; set; }
        public string SortDirection { get; set; } = "asc";
        public string DefaultFilter {get;set;} = "all";
    }
}
