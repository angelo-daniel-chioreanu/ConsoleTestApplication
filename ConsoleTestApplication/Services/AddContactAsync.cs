using System.Net.Http.Json;
using ConsoleTestApplication.Models;

namespace ConsoleTestApplication.Services
{
    public static partial class Service
    {
        public static async Task AddContactAsync(HttpClient client, Contact contact)
        {
            string requestUri = $"{UriServer}/api/Contacts";

            Console.WriteLine($"HTTP POST {requestUri}\n");

            try
            {
                using HttpResponseMessage response = await client.PostAsJsonAsync(requestUri, contact);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Failed to Add Contact.");
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP Error: {ex.Message}");
            }
        }
    }
}
