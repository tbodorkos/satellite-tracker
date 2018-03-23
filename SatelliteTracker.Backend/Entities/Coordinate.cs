using System;
using System.Globalization;

namespace SatelliteTracker.Backend.Entites
{
    public class Coordinates
    {
        public Coordinates(String latitude, String longitude)
        {
            Latitude = Double.TryParse(latitude, style: NumberStyles.Number,
                provider: CultureInfo.InvariantCulture, result: out double lat) ? lat : -1;
            Longitude = Double.TryParse(longitude, style: NumberStyles.Number,
                provider: CultureInfo.InvariantCulture, result: out double lon) ? lon : -1;
        }

        public Double Latitude { get; set; }

        public Double Longitude { get; set; }
    }
}
