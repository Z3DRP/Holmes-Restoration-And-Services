using Holmes_Services.Models.DomainModels;

namespace Holmes_Services.Models.ViewModels
{
    public class CustomerListViewModel
    {
        public IEnumerable<Customer> Customers { get; set; }
        //public IEnumerable<Design> Designs { get; set; }

    }
}
