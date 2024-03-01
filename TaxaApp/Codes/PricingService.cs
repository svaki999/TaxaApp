namespace TaxaApp.Codes
{
    public class PricingService
    {
        public double CalculatePrice(string vehicleType, DateTime rideDateTime, double distanceInKm, double durationInMinutes, bool hasBicycle, bool hasLuggage, bool fromAirport, int passengerCount, bool hasLift)
        {
            double price = 0;
            bool isDaytime = rideDateTime.Hour >= 6 && rideDateTime.Hour < 18 && rideDateTime.DayOfWeek != DayOfWeek.Saturday && rideDateTime.DayOfWeek != DayOfWeek.Sunday;
            bool isNormalVehicle = vehicleType == "normal";

            // Start price, kilometer price, and minute price based on time and vehicle type
            double startPrice = isDaytime ? (isNormalVehicle ? 37 : 77) : (isNormalVehicle ? 47 : 87);
            double kmPrice = isDaytime ? (isNormalVehicle ? 12.75 : 17) : (isNormalVehicle ? 16 : 19);
            double minutePrice = isDaytime ? 5.75 : 7;

            price += startPrice + (kmPrice * distanceInKm) + (minutePrice * durationInMinutes);

            // Additional charges
            if (hasBicycle) price += 30;
            if (hasLuggage) price += 30; // Assuming 'hasLuggage' means 'Opbæring'
            if (fromAirport) price += 15;
            if (passengerCount >= 5) price += 40; // Assuming this is for 5-6 passengers
            if (hasLift) price += 350;

            // Assuming you're handling bridge tolls separately or in another method

            return price;
        }
    }

}
