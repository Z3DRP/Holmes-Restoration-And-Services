using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Holmes_Services.Models.DomainModels
{
    public class Job
    {
        // Note the ErrorDict does not work with DataAnnotations
        [Required(ErrorMessage = "You must enter an Id")]
        [Range(0, int.MaxValue, ErrorMessage = "Id must be a positive number")]
        public int Id { get; set; }


        [Required(ErrorMessage = "Customer id required")]
        [Range(0, int.MaxValue, ErrorMessage = "Id must be a positive number")]
        //fk
        public int Customer_Id { get; set; }
        // nav property
        [ForeignKey(nameof(Customer_Id))]
        public Customer Customer { get; set; }


        [Required(ErrorMessage = "Design Id required")]
        [Range(0, int.MaxValue, ErrorMessage = "Id must be a positive number")]
        //fk
        public int Design_Id { get; set; }
        // nav property
        [ForeignKey(nameof(Design_Id))]
        public Design Design { get; set; }

        // nav property
        public CompletedJob Completed_Job { get; set; }
        public string Slug() => Customer_Id.ToString() + "-" + Design_Id.ToString();
    }
}
