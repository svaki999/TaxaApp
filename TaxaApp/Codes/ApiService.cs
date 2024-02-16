using System;
using System.Net.Http;
using System.Threading.Tasks;
using TaxaApp;
namespace TaxaApp.Codes
{
    public class ApiService
    {
        public readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetDistance(string addressStart, string addressEnd)
        {
            try
            {
                string apiKey = "AIzaSyArd3NK5stVf6nSeBSEcrsH-9FKCRuT_U0";
                string apiUrl = $"https://maps.googleapis.com/maps/api/distancematrix/json?origins={Uri.EscapeDataString(addressStart)}&destinations={Uri.EscapeDataString(addressEnd)}&key={apiKey}";

                var response = await _httpClient.GetStringAsync(apiUrl);
                Console.WriteLine($"API Response: {response}");
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }
    }
}
