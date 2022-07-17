using System.ComponentModel.DataAnnotations;

namespace Holmes_Services.Models.DomainModels
{
    public class Pattern
    {
        [Required(ErrorMessage = "Pattern id is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is requried")]
        [MaxLength(75, ErrorMessage = "Name must be 75 characters or less")]
        public int Name { get; set; }

        [Required(ErrorMessage = "Shape is required")]
        [MaxLength(50, ErrorMessage = "Shape must be 50 characters or less")]
        public string Shape { get; set; }

        // nav properties
        public ICollection<Design> Designs { get; set; }
        public ICollection<Idea> Ideas { get; set; } 

    }
}
