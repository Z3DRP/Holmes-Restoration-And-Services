using Holmes_Services.Models.DomainModels;

namespace Holmes_Services.Models.DTOs
{
    public class CustomerJobsDTO
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Job> Jobs { get; set; }
    }
}
