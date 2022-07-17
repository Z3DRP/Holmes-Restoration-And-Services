using Holmes_Services.Models.DomainModels;

namespace Holmes_Services.Models.DTOs
{
    public class CustomerDesignDTO
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Design> Designs { get; set; }
    }
}
