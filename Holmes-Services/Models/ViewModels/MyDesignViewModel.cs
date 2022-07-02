using Holmes_Services.Models.DomainModels;

namespace Holmes_Services.Models.ViewModels
{
    public class MyDesignViewModel
    {
        public int CustomerId { get; set; }
        public int DesignId { get; set; }
        public Customer Customer { get; set; }
        public IEnumerable<Design> Designs { get; set; }
    }
}
