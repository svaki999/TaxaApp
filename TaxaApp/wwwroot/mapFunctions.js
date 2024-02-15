window.initMap = function () {
    const directionsService = new google.maps.DirectionsService();
    const directionsRenderer = new google.maps.DirectionsRenderer();
    const map = new google.maps.Map(document.getElementById("map"), {
        zoom: 7,
        center: { lat: 55.6761, lng: 12.5683 } // Example center (Copenhagen)
    });
    directionsRenderer.setMap(map);

    window.calculateAndDisplayRoute = function (directionsService, directionsRenderer) {
        directionsService.route({
            origin: document.getElementById("start").value, // Assuming you have an input with id="start"
            destination: document.getElementById("end").value, // Assuming you have an input with id="end"
            travelMode: google.maps.TravelMode.DRIVING,
        }, (response, status) => {
            if (status === "OK") {
                directionsRenderer.setDirections(response);
            } else {
                window.alert("Directions request failed due to " + status);
            }
        });
    };
};

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
    // Decode polyline and create path segments
    const route = decodePolyline(routeData.routes[0].overview_polyline.points);
    const pathSegments = [];
    for (let i = 0; i < route.length; i += 2) {
        pathSegments.push({ lat: route[i], lng: route[i + 1] });
    }

    // Draw the route on the map
    const map = new google.maps.Map(document.getElementById("map"));
    new google.maps.Polyline({
        path: pathSegments,
        strokeColor: '#FF0000',
        strokeOpacity: 0.5,
        strokeWeight: 2
    }).setMap(map);

    // Calculate and display distance (optional)
    const distance = routeData.routes[0].legs[0].distance.value;
    document.getElementById("distance").textContent = "Distance: ${distance} meters";
    DotNet.invokeMethodAsync("TaxaApp", "ShowRouteAndDistance", routeData);
};