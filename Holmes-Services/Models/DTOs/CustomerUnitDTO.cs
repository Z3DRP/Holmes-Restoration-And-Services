using Holmes_Services.Models.DomainModels;

namespace Holmes_Services.Models.DTOs
{
    public class CustomerUnitDTO : CustomerDTO
    {
        public ICollection<DesignDTO> Designs { get; set; }
        public ICollection<JobDTO> Jobs { get; set; }

        public void Load(Customer customer, ICollection<Design> designs, ICollection<Job> jobs)
        {
            Firstname = customer.First_Name;
            Lastname = customer.Last_Name;
            Email = customer.Email;
            Phone = customer.Phone_Number;
            City = customer.City;
            State = customer.State;
            Zipcode = customer.Zipcode;
            LoadDesigns(designs);
            LoadJobs(jobs);
        }
        public void LoadDesigns(ICollection<Design> customerDesigns)
        {
            foreach(Design design in customerDesigns)
            {
                DesignDTO dto = new DesignDTO
                {
                    DesignId = design.Id,
                    DeckId = design.Decking_Id,
                    RailId = design.Railing_Id,
                    Length = design.Length,
                    Width = design.Width,
                    Estimate = design.Estimate
                };
                Designs.Append(dto);
            }
        }
        public void LoadJobs(ICollection<Job> customerJobs)
        {
            foreach(Job job in customerJobs)
            {
                JobDTO dto = new JobDTO
                {
                    JobId = job.Id,
                    DesignId = job.Design_Id,
                    CustomerId = job.Customer_Id
                };
                Jobs.Append(dto);
            }
        }
    }
}
