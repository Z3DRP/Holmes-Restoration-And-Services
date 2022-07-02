using Holmes_Services.Models.DomainModels;

namespace Holmes_Services.Models.ViewModels
{
    public class RailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Rail_Type Type { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
    }
}
