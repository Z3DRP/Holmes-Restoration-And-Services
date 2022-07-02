using Holmes_Services.Models.DomainModels;

namespace Holmes_Services.Models.ViewModels
{
    public class CustomerDesignViewModel : Customer
    {
        public double Length { get; set; }
        public double Width { get; set; }
        public DateTime StartDate { get; set; }
    }
}
