using Holmes_Services.Models.DomainModels;

namespace Holmes_Services.Models.DTOs
{
    public class DesignDTO
    {
        public int DesignId { get; set; }
        public int DeckId { get; set; }
        public int RailId { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public int PatternId { get; set; }
        public double Estimate { get; set; }

        public void Load(Design design)
        {
            DesignId = design.Id;
            DeckId = design.Decking_Id;
            RailId = design.Railing_Id;
            Length = design.Length;
            Width = design.Width;
            PatternId = design.PatternId;
            Estimate = design.Estimate;
        }
    }
}
