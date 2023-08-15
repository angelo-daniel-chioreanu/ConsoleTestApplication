using System.Text.Json.Serialization;

namespace ConsoleTestApplication.Models
{
    public class Email
    {
        [property: JsonPropertyName("id")]
        public long Id { get; set; }

        [property: JsonPropertyName("isPrimary")]
        public bool IsPrimary { get; set; }

        [property: JsonPropertyName("address")]
        public string Address { get; set; } = default!;
    }
}
