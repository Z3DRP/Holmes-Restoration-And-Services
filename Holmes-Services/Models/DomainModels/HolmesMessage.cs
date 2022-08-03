using System.ComponentModel.DataAnnotations;

namespace Holmes_Services.Models.DomainModels
{
    public class HolmesMessage
    {
        
        public int Message_Id { get; set; }

        [Required(ErrorMessage = "Sender id is required")]
        [MaxLength(int.MaxValue, ErrorMessage = "Sender id is out of range")]
        public int Sender_Id { get; set; }

        [Required(ErrorMessage = "Message text is required")]
        [DataType(DataType.Text)]   
        public string Message { get; set; }

        [Required(ErrorMessage = "Send date is required")]
        [DataType(DataType.DateTime, ErrorMessage = "Send date must be date time")]
        public DateTime Send_Date { get; set; }
    }
}
