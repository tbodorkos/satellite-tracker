var colors = ['blue', 'fuchsia', 'gray', 'lime',
    'maroon', 'navy', 'olive', 'orange', 'purple', 'red',
    'silver', 'teal', 'yellow'];

var map = new ol.Map({
    layers: [new ol.layer.Tile({
        source: new ol.source.OSM()
    })
    ],
    target: document.getElementById('map'),
    view: new ol.View({
        center: ol.proj.fromLonLat([19.03991, 47.49801]),
        zoom: 4
    })
});

var overlay = new ol.Overlay({
    element: document.getElementById('mapTooltip'),
    positioning: 'bottom-left'
});
overlay.setMap(map);

map.on(['pointermove', 'singleclick'], function (evt) {
    var feature = map.forEachFeatureAtPixel(evt.pixel, function (feature) {
        overlay.setPosition(evt.coordinate);
        overlay.getElement().innerHTML = feature.get('name');
        return feature;
    });
    overlay.getElement().style.display = feature ? '' : 'none';
    document.body.style.cursor = feature ? 'pointer' : '';
    if (feature) {
        showSatelliteData(feature.get("name"));
    }
    if (!feature) {
        $(".satelliteContent").hide();
    }
});

function drawOnMap(model) {
    var i = 0,
        userCoordinates = model.UserCoordinatesList,
        satellites = model.SatelliteList,
        frequency = 5,
        jump = getJump(frequency, userCoordinates.length),
        userColor = colors[0];

    if (userCoordinates.length === 0 || satellites.length === 0) {
        return;
    }

    document.getElementById("satelliteList").innerHTML = "";
    $("#zeroSatellite").hide();

    drawUserCoordinates(userCoordinates, userColor, jump);
    document.getElementById("satelliteList").innerHTML +=
        "<li style='color: " + userColor + "'><span class='pointer c-black'>User route</span><span class='c-black opacity-half ml-5'>(" + userColor + ")</span></li><br />";

    while (i < satellites.length) {
        var satellite = satellites[i],
            color = colors[++i],
            name_id = satellite.Name.replace(/_/g, " ");

        drawSatellite(userCoordinates, satellite, color, jump);
        document.getElementById("satelliteList").innerHTML +=
            "<li style='color: " + color + "'><span onmouseover='showSatelliteData(\"" + name_id + "\")'" +
        "onmouseout='hideSatelliteData(\"" + name_id + "\")' class='pointer c-black underline'>" + satellite.Name + "</span> <span class='c-black opacity-half ml-5' style='opacity: 0.5'>(" + color + ")</span>" +
            "<div id='" + name_id + "' class='satelliteContent c-black d-none mt-5 ml-10 mb-10 size-13'>" + satellite.Information + "</div></li>";
    }

    setMapCenter(userCoordinates[0]);
}

function getJump(frequency, count) {
    if (frequency > count) {
        frequency = 1;
    }

    return Math.floor(count / frequency);
}

function drawUserCoordinates(coordinates, color, jump) {
    var points = [],
        index = 0,
        jumpIndex = 0;

    while (jumpIndex < coordinates.length) {
        points[index++] = [coordinates[jumpIndex].Longitude, coordinates[jumpIndex].Latitude];
        jumpIndex += jump;
    }

    addLayerToMap(points, "User route", color);
}

function drawSatellite(coordinates, satellite, color, jump) {
    var elevations = satellite.ElevationList,
        azimuths = satellite.AzimuthList,
        points = [],
        index = 0,
        jumpIndex = 0,
        distance,
        dx,
        dy,
        delta_longitude,
        delta_latitude,
        final_longitude,
        final_latitude;

    while (jumpIndex < elevations.length) {
        distance = getDistance(elevations[jumpIndex], 22000);
        azimuth = azimuths[jumpIndex];
        dx = distance * Math.sin(toRadian(azimuth));
        dy = distance * Math.cos(toRadian(azimuth));
        delta_longitude = dx / (111320 * Math.cos(coordinates[jumpIndex].Latitude));
        delta_latitude = dy / 110540;
        final_longitude = coordinates[jumpIndex].Longitude + delta_longitude;
        final_latitude = coordinates[jumpIndex].Latitude + delta_latitude;

        points[index++] = [final_longitude, final_latitude];

        jumpIndex += jump;
    }

    addLayerToMap(points, satellite.Name, color);
}

function toRadian(angle) {
    return angle * (Math.PI / 180);
}

function toDegree(angle) {
    return angle * (180 / Math.PI);
}

function getDistance(elevation) {
    return Math.cos(toRadian(elevation)) * 22000;
}

function setMapCenter(coordinates) {
    map.getView().setCenter(ol.proj.transform([coordinates.Longitude, coordinates.Latitude], 'EPSG:4326', 'EPSG:3857'));
    map.getView().setZoom(10);
}

function addLayerToMap(points, name, color) {
    var line = {
        "type": "Feature",
        "properties": {
            "name": name,
            "stroke": "black"
        },
        "geometry": {
            "type": "LineString",
            "coordinates": points
        }
    };

    var curved = turf.bezier(line);
    curved.properties = { stroke: color, name: name };

    var geojsonObject = {
        "type": "FeatureCollection",
        "features": [curved]
    };
    var vectorSource = new ol.source.Vector({
        features: (new ol.format.GeoJSON()).readFeatures(geojsonObject,
            { dataProjection: 'EPSG:4326', featureProjection: 'EPSG:3857' })
    });

    var styles = {};
    var styleFunction = function (feature) {
        var featureColor = feature.get('stroke');
        styles[featureColor] = new ol.style.Style({
            stroke: new ol.style.Stroke({
                color: featureColor,
                width: 4
            })
        });
        return styles[featureColor];
    };

    var vectorLineLayer = new ol.layer.Vector({
        source: vectorSource,
        style: styleFunction
    });

    map.addLayer(vectorLineLayer);
}

function showSatelliteData(satellite) {
    $("#" + satellite).show();
}

function hideSatelliteData(satellite) {
    $("#" + satellite).hide();
}
