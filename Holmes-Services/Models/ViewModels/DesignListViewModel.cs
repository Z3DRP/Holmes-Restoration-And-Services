using Holmes_Services.Models.DomainModels;
using Holmes_Services.Models.RouteDictionaries;

namespace Holmes_Services.Models.ViewModels
{
    public class DesignListViewModel
    {
        public IEnumerable<Design> Designs { get; set; }
        public RouteDictionary CurrentRoute { get; set; }
        public int TotalPages { get; set; }
        // data for filter drop-downs 

    }
}
