using System.Text.Json.Serialization;
using WebTestApplication.Utilities;

namespace ConsoleTestApplication.Models
{
    public class Contact
    {
        [property: JsonPropertyName("id")]
        public long Id { get; set; }

        [property: JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [property: JsonPropertyName("birthDate"), JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly? BirthDate { get; set; } = null!;

        [property: JsonPropertyName("emails")]
        public ICollection<Email> Emails { get; set; } = null!;
    }
}
