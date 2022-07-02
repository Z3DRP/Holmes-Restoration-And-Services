using System.Text.Json.Serialization;

namespace Holmes_Services.Models.DTOs
{
    public class RailingGridDTO : GridDTO
    {
        [JsonIgnore]
        public const string DefaultFilter = "all";

        public string Type { get; set; } = DefaultFilter;
        public string Price { get; set; } = DefaultFilter;
        public string Group { get; set; } = DefaultFilter;
    }
}
