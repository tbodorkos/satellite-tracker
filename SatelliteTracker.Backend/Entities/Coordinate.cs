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
        /// Coordinate's latitude
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Coordinate's longitude
        /// </summary>
        public double Longitude { get; set; }
    }
}
