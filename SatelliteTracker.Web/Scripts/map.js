var colors = ['blue', 'fuchsia', 'gray', 'lime',
    'maroon', 'navy', 'olive', 'orange', 'purple', 'red',
    'silver', 'teal', 'white', 'yellow'];

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
        satellites = model.SatelliteList;

    if (userCoordinates.length === 0 || satellites.length === 0) {
        return;
    }

    document.getElementById("satelliteList").innerHTML = "";
    $("#zeroSatellite").hide();

    drawUserCoordinates(userCoordinates, colors[0]);

    for (i; i < satellites.length; ++i) {
        var satellite = satellites[i],
            color = colors[i + 1],
            name_id = satellite.Name.replace(/_/g, " ");

        drawSatellite(userCoordinates, satellite, color);
        document.getElementById("satelliteList").innerHTML +=
            "<li style='color: " + color + "'><span onmouseover='showSatelliteData(\"" + name_id + "\")'" +
            "onmouseout='hideSatelliteData(\"" + name_id + "\")' class='pointer c-black underline'>" + satellite.Name + "</span>" +
            "<div id='" + name_id + "' class='satelliteContent c-black d-none mt-5 ml-10 mb-10 size-13'>" + satellite.Information + "</div></li>";
    }

    setMapCenter(userCoordinates[0]);
}

function drawUserCoordinates(coordinates, color) {
    var points = [],
        index = 0;

    for (index; index < coordinates.length; ++index) {
        points[index] = [coordinates[index].Longitude, coordinates[index].Latitude];
    }

    addLayerToMap(points, "User route", color);
}

function drawSatellite(coordinates, satellite, color) {
    var elevations = satellite.ElevationList,
        azimuths = satellite.AzimuthList,
        points = [],
        index = 0,
        distance,
        dx,
        dy,
        delta_longitude,
        delta_latitude,
        final_longitude,
        final_latitude;

    for (index; index < elevations.length; ++index) {
        distance = getDistance(elevations[index], 22000);
        azimuth = azimuths[index];
        dx = distance * Math.sin(toRadian(azimuth));
        dy = distance * Math.cos(toRadian(azimuth));
        console.log(index);
        delta_longitude = dx / (111320 * Math.cos(coordinates[index].Latitude));
        delta_latitude = dy / 110540;
        final_longitude = coordinates[index].Longitude + delta_longitude;
        final_latitude = coordinates[index].Latitude + delta_latitude;

        points[index] = [final_longitude, final_latitude];
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
    curved.properties = { stroke: color };

    var geojsonObject = {
        "type": "FeatureCollection",
        "features": [line, curved]
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
                width: 3
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
