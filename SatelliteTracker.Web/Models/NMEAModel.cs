using System.Collections.Generic;
using SatelliteTracker.Backend.Entites;

namespace SatelliteTracker.Web.Models
{
    /// <summary>
    /// NMEA data model
    /// </summary>
    public class NMEAModel
    {
        /// <summary>
        /// User coordinates
        /// </summary>
        public IEnumerable<Coordinates> UserCoordinatesList { get; set; }

        /// <summary>
        /// Satellite Model list
        /// </summary>
        public IEnumerable<SatelliteModel> SatelliteList { get; set; }
    }
}
