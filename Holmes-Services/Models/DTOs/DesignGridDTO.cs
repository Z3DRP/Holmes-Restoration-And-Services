using System.Text.Json.Serialization;

namespace Holmes_Services.Models.DTOs
{
    public class DesignGridDTO : GridDTO
    {
        [JsonIgnore]
        public const string DefaultFilter = "all";
        public string Pattern { get; set; }
        public string Price { get; set; }
        public string DeckGroup { get; set; }
        public string RailGroup { get; set; }
        public string StartDate { get; set; }
        public string Deck { get; set; }
        public string Rail { get; set; }
    }
}
