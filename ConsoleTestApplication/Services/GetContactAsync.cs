using System.Net.Http.Json;
using ConsoleTestApplication.Models;

namespace ConsoleTestApplication.Services
{
    public static partial class Service
    {
        public static async Task<Contact?> GetContactAsync(HttpClient client, long contactId)
        {
            string requestUri = $"{UriServer}/api/Contacts/{contactId}";

            Console.WriteLine($"HTTP GET {requestUri}\n");

            Contact? contact = null;

            try
            {
                contact = await client.GetFromJsonAsync<Contact>(requestUri);
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine($"Failed to Get Contact.");
                }
                else
                {
                    Console.WriteLine($"HTTP Error: {ex.Message}");
                }
            }

            return contact;
        }
    }
}
