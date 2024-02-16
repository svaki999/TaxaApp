using Microsoft.AspNetCore.Components;
using System.Text.Json;
using TaxaApp.Codes;

namespace TaxaApp.Pages
{
    public partial class PageOne
    {
        //API KEY                   AIzaSyArd3NK5stVf6nSeBSEcrsH-9FKCRuT_U0
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
        public string? MapUrl { get; set; }

        public string? DistanceResult { get; set; }

        //[Parameter]
        //public string Title { get; set; }

        public async Task Calculate()
        {

            if (!string.IsNullOrWhiteSpace(AddressStart) && !string.IsNullOrWhiteSpace(AddressEnd))
            {

                double startPrice = 0;
                double pricePerKm = 0;
                double pricePerMin = 0;

                if (SelectedTime == "dag" && SelectedCar == "Almindelige Vogne")
                {
                    startPrice = 37;
                    pricePerKm = 12.75;
                    pricePerMin = 5.75;
                }
                else if (SelectedTime == "nat" && SelectedCar == "Almindelige Vogne")
                {
                    startPrice = 47;
                    pricePerKm = 16;
                    pricePerMin = 7;
                }
                else if (SelectedTime == "dag" && SelectedCar == "Store Vogne")
                {
                    startPrice = 77;
                    pricePerKm = 17;
                    pricePerMin = 5.75;
                }
                else if (SelectedTime == "nat" && SelectedCar == "Store Vogne")
                {
                    startPrice = 87;
                    pricePerKm = 19;
                    pricePerMin = 7;
                }



                var response = await ApiService.GetDistance(AddressStart, AddressEnd);
                if (response != null)
                {
                    var result = JsonSerializer.Deserialize<GoogleMapsDistanceMatrixResponse>(response);
                    string distance = result?.rows?.FirstOrDefault()?.elements?.FirstOrDefault()?.distance?.text;
                    string duration = result?.rows?.FirstOrDefault()?.elements?.FirstOrDefault()?.duration?.text;
                    DistanceResult = distance != null ? $"Distance: {distance}, tid: {duration}, kr:{startPrice}" : "";




                    MapUrl = $"https://www.google.com/maps/embed/v1/directions?key=AIzaSyBH1LLJchXHqhquPfqwe8KUCcc2yu7HWG0&origin={AddressStart}&destination={AddressEnd}";

                    Button = true;
                }
            }
        }
    }
}