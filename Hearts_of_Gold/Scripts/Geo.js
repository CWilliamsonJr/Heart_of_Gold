
public  GeoLocation()

{
    var foos = $(".foo");
    var markers = new Array();
    var mapOptions = {
       
    }};
var mapCanvas = document.getElementById('map-canvas');
if (mapCanvas != null) {
    map = new google.maps.Map(mapCanvas, mapOptions);

    $.each(foos, function (key, value)
    {
        markers[key] = new google.maps.Marker({
            map: map,
            draggable: false,
            animation: google.maps.Animation.DROP,
            position: new google.maps.LatLng(
                Number($(value).attr("data-latitude")),
                Number($(value).attr("data-longitude")
             ))
        });

        google.maps.event.addListener(markers [key], 'click',  GeoLocation ()  );

        // If you need this...
    })}
