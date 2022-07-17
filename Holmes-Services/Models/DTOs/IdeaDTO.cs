using Holmes_Services.Models.DomainModels;

namespace Holmes_Services.Models.DTOs
{
    public class IdeaDTO
    {
        public int Id { get; set; }
        public Decking Deck { get; set; }
        public Railing Rail { get; set; }
        public Pattern Pattern { get; set; }
        public double Estimate { get; set; }
    }
}
