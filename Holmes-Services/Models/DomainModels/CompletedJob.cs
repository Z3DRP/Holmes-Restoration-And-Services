using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Holmes_Services.Models.DomainModels
{
    public class CompletedJob
    {
        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Job id is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Job is is out of range")]
        public int Job_Id { get; set; }
        [ForeignKey(nameof(Job_Id))]
        public Job Job { get; set; }

        [Required(ErrorMessage = "Completion date required")]
        [DataType(DataType.Date, ErrorMessage = "Completion date must be a valid date")]
        public DateTime Completioin_Date { get; set; }

        [Required(ErrorMessage = "Final image is required")]
        public string Image { get; set; }
        

    }
}
