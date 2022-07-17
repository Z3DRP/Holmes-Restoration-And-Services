using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Holmes_Services.Models.DomainModels
{
    public class Idea
    {
        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Decking Id required")]
        [Range(0, int.MaxValue, ErrorMessage = "Id must be a positive number")]
        public int Decking_Id { get; set; }
        //navigational property - fk
        [ForeignKey(nameof(Decking_Id))]
        public Decking Deck { get; set; }

        [Required(ErrorMessage = "Railing Id required")]
        [Range(0, int.MaxValue, ErrorMessage = "Id must be a positive number")]
        public int Railing_Id { get; set; }
        //navigational property - fk
        [ForeignKey(nameof(Railing_Id))]
        public Railing Rail { get; set; }

        [Required(ErrorMessage = "Decking Id required")]
        [Range(0, int.MaxValue, ErrorMessage = "Id must be a positive number")]
        public int Pattern_Id { get; set; }
        //navigational property - fk
        [ForeignKey(nameof(Pattern_Id))]
        public Pattern Pattern { get; set; }

        [Required(ErrorMessage = "Estimate is required")]
        public double Estimate { get; set; }
    }
}
