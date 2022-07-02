using Holmes_Services.Models.DomainModels;
using Holmes_Services.Models.RouteDictionaries;

namespace Holmes_Services.Models.ViewModels
{
    public class DeckListViewModel
    {
        public IEnumerable<Decking> Decks { get; set; }
        public RouteDictionary CurrentRoute { get; set; }
        public IEnumerable<Deck_Type> Types { get; set; }
        public IEnumerable<Price_Groups> Groups { get; set; }
        public int TotalPages { get; set; }

        // eventually take out the hardcode and make it where values pulled from db
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
