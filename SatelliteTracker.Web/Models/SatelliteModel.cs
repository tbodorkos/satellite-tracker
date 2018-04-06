using System.Collections.Generic;

namespace SatelliteTracker.Web.Models
{
    /// <summary>
    /// Satellite data model
    /// </summary>
    public class SatelliteModel
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Elevations
        /// </summary>
        public IList<int> ElevationList { get; set; }

        /// <summary>
        /// Azimuths
        /// </summary>
        public IList<int> AzimuthList { get; set; }

        /// <summary>
        /// Custom informations
        /// </summary>
        public string Information { get; set; }
    }
}
