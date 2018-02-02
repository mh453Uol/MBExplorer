var map;

google.maps.event.addDomListener(window, 'load', initialize);

function initialize() {
    var mapCanvas = document.getElementById('map');
    var mapOptions = {
        center: new google.maps.LatLng(52.6368778, -1.1397591999999577),
        zoom: 10,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    }
    map = new google.maps.Map(mapCanvas, mapOptions)
}

$('#js-view-locationModal').on('shown.bs.modal', function () {
    google.maps.event.trigger(map, "resize");
});


function addMarker(latitude, longitude) {
    map.setCenter({ lat: latitude, lng: longitude });

    var marker = new google.maps.Marker({
        position: { lat: latitude, lng: longitude },
        map: map,
        title: 'Hello World!'
    });
}




