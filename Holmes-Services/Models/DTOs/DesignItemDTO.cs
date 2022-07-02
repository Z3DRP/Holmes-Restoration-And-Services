namespace Holmes_Services.Models.DTOs
{
    public class DesignItemDTO
    {
        public int DesignID { get; set; }
        public int DeckId { get; set; }
        public bool DeckAdded { get; set; }
        public int RailId { get; set; }
        public bool RailAdded { get; set; }
        public double Estimate { get; set; }
    }
}
