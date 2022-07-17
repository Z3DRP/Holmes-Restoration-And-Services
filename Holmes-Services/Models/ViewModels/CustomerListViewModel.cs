using Holmes_Services.Models.DomainModels;
using Holmes_Services.Models.RouteDictionaries;

namespace Holmes_Services.Models.ViewModels
{
    public class CustomerListViewModel
    {
        public IEnumerable<Customer> Customers { get; set; }
        //public IEnumerable<Design> Designs { get; set; }
        public RouteDictionary CurrentRoute { get; set; }
        public int TotalPages { get; set; }

    }
}
