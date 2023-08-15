using System.Net.Http.Json;
using ConsoleTestApplication.Models;

namespace ConsoleTestApplication.Services
{
    public static partial class Service
    {
        public static async Task<IEnumerable<Contact>?> GetContactsByNameAsync(HttpClient client, string name)
        {
            string requestUri = $"{UriServer}/api/Contacts/name={Uri.EscapeDataString(name)}";

            Console.WriteLine($"HTTP GET {requestUri}\n");

            IEnumerable<Contact>? contacts = null;

            try
            {
                contacts = await client.GetFromJsonAsync<List<Contact>>(requestUri);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP Error: {ex.Message}");
            }

            return contacts;
        }
    }
}
