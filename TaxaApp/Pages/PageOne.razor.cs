using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Globalization;
using System.Text.Json;
using TaxaApp.Codes;

namespace TaxaApp.Pages
{
    public partial class PageOne
    {
        //API KEY                   AIzaSyArd3NK5stVf6nSeBSEcrsH-9FKCRuT_U0
        private const string apiKey = "AIzaSyArd3NK5stVf6nSeBSEcrsH-9FKCRuT_U0";
        public string? AddressStart { get; set; }
        public string? AddressEnd { get; set; }
        public string? SelectedTime { get; set; }
        public string? SelectedCar { get; set; }
        public int Checkbox1 { get; set; }
        public bool Checkbox2 { get; set; }
        public bool Checkbox3 { get; set; }
        public bool Checkbox4 { get; set; }
        public bool Checkbox5 { get; set; }
        public bool Checkbox6 { get; set; }
        public bool Checkbox7 { get; set; }
        public bool Button { get; set; }
        public string? MapRoute { get; set; } = $"https://www.google.com/maps/embed/v1/view?zoom=11&center=55.6761%2C12.5683&key={apiKey}"; // Default view

        public string? DistanceResult { get; set; }

        //[Parameter]
        //public string Title { get; set; }


        private double CalculatePrice(string selectedTime, string selectedCar, string distance)
        {
            double startPrice = 0; double pricePerKm = 0; double pricePerMin = 0;



            // Implement your pricing logic here based on your actual pricing structure

            // Example pricing logic (replace with your own):
            if (selectedCar == "Almindelige Vogne")
            {
                if (selectedTime == "dag")
                {
                    startPrice = 37;
                    pricePerKm = 12.75;
                    pricePerMin = 5.75;
                }
                else
                {
                    startPrice = 47;
                    pricePerKm = 16;
                    pricePerMin = 7;
                }
            }
            else if (selectedCar == "Store Vogne")
            {
                if (selectedTime == "dag")
                {
                    startPrice = 77;
                    pricePerKm = 17;
                    pricePerMin = 5.75;
                }
                else
                {
                    startPrice = 87;
                    pricePerKm = 19;
                    pricePerMin = 7;
                }
            }
            // Assuming distance is provided in kilometers and you want to calculate total price
            double totalDistance = double.Parse(distance, CultureInfo.InvariantCulture);
            double totalPrice = startPrice + (pricePerKm * totalDistance); // Simplified calculation

            return totalPrice; // Ensure to return the calculated total price

        }


        // Calculate the price based on distance and time (replace with your specific
        public async Task Calculate()
        {

            if (string.IsNullOrWhiteSpace(AddressStart) || string.IsNullOrWhiteSpace(AddressEnd))
            {
                return; // Handle empty addresses
            }


            var response = await ApiService.GetDistance(AddressStart, AddressEnd);
                if (response != null)
                {
                    var result = JsonSerializer.Deserialize<GoogleMapsDistanceMatrixResponse>(response);

                    if (result != null)
                    {
                        // Extract relevant data (assuming specific properties in your response)
                        string distance = result.rows?.FirstOrDefault()?.elements?.FirstOrDefault()?.distance?.text ?? "";
                        string duration = result.rows?.FirstOrDefault()?.elements?.FirstOrDefault()?.duration?.text ?? "";

                        // Construct the directions URL using your API key and addresses
                        string newMapRoute = $"https://www.google.com/maps/embed/v1/directions?key={apiKey}&origin={AddressStart}&destination={AddressEnd}";
                        await JSRuntime.InvokeVoidAsync("updateIframeSource", newMapRoute);

                        // Update displayed information
                        DistanceResult = $"Distance: {distance}, Tid: {duration}, Kr: {CalculatePrice(SelectedTime, SelectedCar, distance)}"; // Calculate and display price
                    }
                }
            }
        }
    }
