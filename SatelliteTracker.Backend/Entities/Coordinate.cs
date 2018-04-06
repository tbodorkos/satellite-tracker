using System;
using System.Globalization;

namespace SatelliteTracker.Backend.Entites
{
    /// <summary>
    /// Coordinates entity
    /// </summary>
    public class Coordinates
    {
        /// <summary>
        /// Constructor of Coordinates entity
        /// </summary>
        /// <param name="latitude">Coordinate's latitude</param>
        /// <param name="longitude">Coordinate's longitude</param>
        public Coordinates(string latitude, string longitude)
        {
            Latitude = Double.TryParse(latitude, style: NumberStyles.Number,
                provider: CultureInfo.InvariantCulture, result: out double lat) ? lat : -1;
            Longitude = Double.TryParse(longitude, style: NumberStyles.Number,
                provider: CultureInfo.InvariantCulture, result: out double lon) ? lon : -1;
        }

        /// <summary>
        /// Coordinate's latitude
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Coordinate's longitude
        /// </summary>
        public double Longitude { get; set; }
    }
}
