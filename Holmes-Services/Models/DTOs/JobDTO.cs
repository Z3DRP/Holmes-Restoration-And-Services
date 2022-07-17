using Holmes_Services.Models.DomainModels;

namespace Holmes_Services.Models.DTOs
{
    public class JobDTO
    {
        public int JobId { get; set; }
        public int DesignId { get; set; }
        public int CustomerId { get; set; }
        
        public void Load(Job job)
        {
            JobId = job.Id;
            DesignId = job.Design_Id;
            CustomerId = job.Customer_Id;
        }
    }
}
