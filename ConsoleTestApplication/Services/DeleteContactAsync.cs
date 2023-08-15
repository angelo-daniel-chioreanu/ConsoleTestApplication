namespace ConsoleTestApplication.Services
{
    public static partial class Service
    {
        public static async Task DeleteContactAsync(HttpClient client, long contactId)
        {
            string requestUri = $"{UriServer}/api/Contacts/{contactId}";

            Console.WriteLine($"HTTP DELETE {requestUri}\n");

            try
            {
                using HttpResponseMessage response = await client.DeleteAsync(requestUri);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Failed to Delete Contact.");
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP Error: {ex.Message}");
            }
        }
    }
}
