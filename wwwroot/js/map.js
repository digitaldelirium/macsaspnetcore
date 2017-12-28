//(function () {
var getLocation, loc, checked, map, credentials, campLat, campLong;
checked = $("#showMyDirections").unchecked;

campLat = 44.052341;
campLong = -70.440102;

function getMap() {
    map = new Microsoft.Maps.Map(document.getElementById('campMap'), {
        credentials: 'ArDOPLYiClYI38DkQkn07iW736WfoKdJcriQJwJNvYCGybwo9jXXxQRW4hGYg6Gl'
    });

    var pushpin = new Microsoft.Maps.Pushpin(map.getCenter(), null);
    map.entities.push(pushpin);
    pushpin.setLocation(new Microsoft.Maps.Location(campLat, campLong));
    map.setView({
        zoom: 10,
        center: new Microsoft.Maps.Location(campLat, campLong)
    });
};
/* Feature Blocked Pending .NET Core DataContract type resolving
// Request user's location
    getLocation = (navigator.geolocation.getCurrentPosition(function(position) {
    loc = new Microsoft.Maps.Location(
        position.coords.latitude,
        position.coords.longitude);

    //Add a push pin at user location
    var userPin = new Microsoft.Maps.Pushpin(loc);
    map.entities.push(userPin);
}));


    // if  the user doesn't check personalized directions and doesn't want to share location hide the div for directions
    if (!checked && !getLocation || getLocation) {
        $("#getDirections").hide();
    }

if (!getLocation) {
    getDirections(getLocation);
} else {
    getDirections(getLocation);
}

if ($("#showMyDirections").click()) {
    getDirections(checked);
}

function getDirections(show) {
    if(show){
      $("#showMyDirections").unchecked;
      $("#standardUi").show();
      $("#altUi").hide();

        } else {
        $("#standardUi").hide();
        $("#altUi").show();
        checked = true;
        if (!getLocation) {
            $("#enterDirections").show();
        } else {
            $("#enterDirections").hide();
        }
    }
}

function getDrivingDirections(loc) {
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "http://dev.virtualearth.net/REST/V1/Routes/Driving?wp.0=" + loc + "&wp.1=" + campLat +"," + campLong + "&optmz=distance&routeAttributes=routePath&key=" + credentials,
        success: function(data) {
            parseDirections(data);
        },
        error: function() {

        }
    });
}

function parseDirections(data) {
    var directions = JSON.parse(data);
    var maneuvers = directions.resourceSets.routeLegs.itineraryItems;


    // Get Starting Address, Finishing Address


    if (maneuvers.length > 10)
        // if i > 10 write iframe for contents and scrolling capability

    // Get Turns in foreach statement increment i
    for (var i = 0; i < maneuvers.length; ++i) {
        var heading = maneuvers[i].compassDirection.value;
        var instruction = maneuvers[i].instruction.text.value;
        var maneuverType = maneuvers[i].instruction.maneuverType.value;
        var sideOfStreet = maneuvers[i].sideOfStreet.value;
        var maneuverPoint = maneuvers[i].maneuverPoint.Coordinates.value;
        var tollZone = maneuvers[i].tollZone.value;
        var travelDistance = maneuvers[i].travelDistance.value;
    }


    // Else write it out 'raw' into Bootstrapped list-group items
}
*/
  getMap();
//})();