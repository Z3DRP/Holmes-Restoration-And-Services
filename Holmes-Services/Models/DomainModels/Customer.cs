using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Holmes_Services.Models.DomainModels
{
    public class Customer
    {
        [Required(ErrorMessage = "Id required")]
        [Range(0, double.MaxValue, ErrorMessage = "Id must be a positive number")]
        public int Id { get; set; }


        [Required(ErrorMessage = "First name is required")]
        [MaxLength(100, ErrorMessage = "First name must be 100 characters or less")]
        [RegularExpression(@"[a-zA-Z]*?", ErrorMessage = "First name must contain lettersonly")]
        public string First_Name { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [MaxLength(100, ErrorMessage = "Last name must be 100 characters or less")]
        [RegularExpression(@"[a-zA-Z]*?", ErrorMessage = "Last name must contain lettersonly")]
        public string Last_Name { get; set; }


        [Required(ErrorMessage = "Email address is required")]
        [MaxLength(100, ErrorMessage = "Email address must be 100 characters or less")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid email format")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Phone number is required")]
        [MaxLength(12, ErrorMessage = "Phone number must be 12 characters")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid phone number format")]
        public string Phone_Number { get; set; }


        [Required(ErrorMessage = "Street address is required")]
        [MaxLength(75, ErrorMessage = "Street address must be 75 characers or less")]
        [RegularExpression(@"\w+?\s*?\w*?", ErrorMessage = "Street must contain lettersonly")]
        public string Street_Address { get; set; }


        [Required(ErrorMessage = "City is required")]
        [MaxLength(75, ErrorMessage = "City must be 75 characters or less")]
        [RegularExpression(@"\w+?\s*?\w*?", ErrorMessage = "City must contain lettersonly")]
        public string City { get; set; }


        [Required(ErrorMessage = "State is required")]
        [MaxLength(50, ErrorMessage = "State must be 50 characters or less")]
        [RegularExpression(@"\w+?\s*?\w*?", ErrorMessage = "State must contain lettersonly")]
        public string State { get; set; }


        [Required(ErrorMessage = "Zipcode is required")]
        [MaxLength(5, ErrorMessage = "Zipcode cannot be more than 5 characters")]
        public string Zipcode { get; set; }
        // nav prop
        public ICollection<Design> Designs { get; set; }
        // nav prop
        public ICollection<Job> Jobs { get; set; }

        public string GetCustomerFullname() => First_Name + Last_Name;
        public string Slug() => Last_Name + "-" + First_Name;
    }
}
