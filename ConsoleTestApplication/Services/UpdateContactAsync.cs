using System.Net.Http.Json;
using ConsoleTestApplication.Models;

namespace ConsoleTestApplication.Services
{
    public static partial class Service
    {
        public static async Task UpdateContactAsync(HttpClient client, Contact contact)
        {
            string requestUri = $"{UriServer}/api/Contacts/{contact.Id}";

            Console.WriteLine($"HTTP PUT {requestUri}\n");

            try
            {
                using HttpResponseMessage response = await client.PutAsJsonAsync(requestUri, contact);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Failed to Update Contact.");
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP Error: {ex.Message}");
            }
        }
    }
}
