using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
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
        public string TimeOption { get; set; } = "now";
        public string VehicleType { get; set; } = "normal";
        public string PriceType { get; set; } = "set";

        public string? MapRoute { get; set; } = $"https://www.google.com/maps/embed/v1/view?zoom=11&center=55.6761%2C12.5683&key={apiKey}"; // Default view

        public string? DistanceResult { get; set; }

        private bool _showPageTwoOptions { get; set; } = false;

        public double DistanceKm { get; set; }
        public double DurationMinutes { get; set; }

        [Inject]
        public IModalService ModalService { get; set; }

        // Update map view and distance result
        public async Task ShowMapDistance()
        {
            if (string.IsNullOrWhiteSpace(AddressStart) || string.IsNullOrWhiteSpace(AddressEnd))
            {
                return; // Handle empty addresses
            }

            _showPageTwoOptions = true;

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
                    DistanceResult = $"Distance: {distance}, Tid: {duration}";
                }
            }
        }

        private async Task ShowPageTwo()
        {
            if (AddressStart == null || AddressEnd == null)
            {
                // Handle errors or missing input
                return;
            }

            // Calculate price
            double distanceKm = DistanceKm; // Assuming you have this value
            double durationInMinutes = DurationMinutes; // Assuming you have this value
            DateTime now = DateTime.Now;
            double price = PricingService.CalculatePrice(VehicleType, now, distanceKm, durationInMinutes, false, false, false, 0, false);

            var parameters = new ModalParameters();
            parameters.Add("AddressStart", AddressStart);
            parameters.Add("AddressEnd", AddressEnd);
            parameters.Add("Distance", distanceKm.ToString());
            parameters.Add("Duration", durationInMinutes.ToString());
            parameters.Add("Price", price.ToString("N2")); // Format price as needed

            var options = new ModalOptions
            {
                Size = ModalSize.Large,
                HideCloseButton = false
            };

            var modal = ModalService.Show<PageTwo>("Trip Details", parameters, options);
            var result = await modal.Result;

            if (!result.Cancelled)
            {
                // Handle the result if needed
            }
        }
    }
}
