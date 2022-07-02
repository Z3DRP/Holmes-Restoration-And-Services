using Holmes_Services.Models.DomainModels;
using Holmes_Services.Models.RouteDictionaries;

namespace Holmes_Services.Models.ViewModels
{
    public class RailListViewModel
    {
        public IEnumerable<Railing> Rails { get; set; }
        public RouteDictionary CurrentRoute { get; set; }
        // data for filter drop-downs
        public IEnumerable<Rail_Type> Types { get; set; }
        public IEnumerable<Price_Groups> Groups { get; set; }
        public int TotalPages { get; set; }
        public Dictionary<string, string> Prices =>
            new Dictionary<string, string>
            {
                {"A",  "Group A"},
                {"B", "Group B"},
                {"C", "Group C" },
            };
        // pagesizes will be changed to do default size of 5 or show all
        public int[] PageSizes => new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
    }
}
