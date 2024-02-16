//window.initMap = function () {
//    const directionsService = new google.maps.DirectionsService();
//    const directionsRenderer = new google.maps.DirectionsRenderer();
//    const map = new google.maps.Map(document.getElementById("map"), {
//        zoom: 7,
//        center: { lat: 55.6761, lng: 12.5683 } // Example center (Copenhagen)
//    });
//    directionsRenderer.setMap(map);

//    window.calculateAndDisplayRoute = function (directionsService, directionsRenderer) {
//        directionsService.route({
//            origin: document.getElementById("start").value, // Assuming you have an input with id="start"
//            destination: document.getElementById("end").value, // Assuming you have an input with id="end"
//            travelMode: google.maps.TravelMode.DRIVING,
//        }, (response, status) => {
//            if (status === "OK") {
//                directionsRenderer.setDirections(response);
//            } else {
//                window.alert("Directions request failed due to " + status);
//            }
//        });
//    };
//};

window.displayAddressSuggestions = function (suggestions) {
    // Create an array to store formatted suggestions
    const formattedSuggestions = [];

    // Loop through suggestions and extract relevant data
    suggestions.predictions.forEach(suggestion => {
        formattedSuggestions.push({
            description: suggestion.description,
            placeId: suggestion.place_id
        });
    });

    // Call a Blazor method to handle displaying suggestions (see step 2)
    DotNet.invokeMethodAsync("TaxaApp", "HandleAddressSuggestions", formattedSuggestions);
};

window.updateMapWithRoute = function (routeData) {
    const map = document.getElementById("map"); // Use existing map instance

    // Create directionsService and directionsRenderer
    const directionsService = new google.maps.DirectionsService();
    const directionsRenderer = new google.maps.DirectionsRenderer({ map: map });

    // Decode polyline (if necessary)
    const decodedPolyline = routeData.routes[0].overview_polyline.points; // Assuming you've already decoded it

    // Set the route's polyline (or use alternative route representation)
    directionsRenderer.setDirections({
        routes: [
            {
                overview_polyline: {
                    points: decodedPolyline
                }
            }
        ]
    });

    // Extract and display distance (if desired)
    const distance = routeData.routes[0].legs[0].distance.value;
    document.getElementById("distance").textContent = `Distance: ${distance} meters`;
};