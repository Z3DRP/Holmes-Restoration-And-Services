using Holmes_Services.Models.DomainModels;

namespace Holmes_Services.Models.DTOs
{
    public class PortfollioDTO
    {
        public int Id { get; set; }
        public Decking Deck { get; set; }
        public Railing Rail { get; set; }
        public Pattern Pattern { get; set; }
        public double Estimate { get; set; }
        public string Image { get; set; }
    }
}
