using System.ComponentModel.DataAnnotations;

namespace Holmes_Services.Models.DomainModels
{
    public class Rail_Type
    {
        [Required(ErrorMessage = "Id is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Id must be a positive number")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Rail type is required")]
        [MaxLength(100, ErrorMessage = "Rail type must be 100 characters or less")]
        [RegularExpression(@"[0-9]*?[a-zA-Z]*?[0-9]*?[a-zA-Z]*?", ErrorMessage = "Rail type may contain letters and numbers only")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Type code is required")]
        [MaxLength(10, ErrorMessage = "Type code must be 10 characters or less")]
        [RegularExpression(@"[0-9]*?[a-zA-Z]*?[0-9]*?[a-zA-Z]*?", ErrorMessage = "Type code may contain letters and numbers only")]
        public string Type_Code { get; set; }
        // nav property
        public ICollection<Railing> Railings { get; set; }
    }
}
