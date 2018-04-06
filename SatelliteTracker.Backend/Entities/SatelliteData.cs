using System;
using SatelliteTracker.Backend.Types;

namespace SatelliteTracker.Backend.Entities
{
    /// <summary>
    /// SatelliteData entity
    /// </summary>
    public class SatelliteData
    {
        /// <summary>
        /// PRN number
        /// </summary>
        public string PRN { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// System type
        /// </summary>
        public SystemType System { get; set; }

        /// <summary>
        /// Launch date
        /// </summary>
        public DateTime? Launch { get; set; }

        /// <summary>
        /// Returns data in string
        /// </summary>
        /// <returns>Satellite data in string</returns>
        public override string ToString()
        {
            return $@"System: {(System == SystemType.Unknown ? "Unknown" : System.ToString())}
                    <br />
                    PRN number: {PRN}
                    <br />
                    Launch date: {(Launch.HasValue ? Launch.Value.ToString("yyyy.MM.dd HH:mm") : "Unknown")}";
        }
    }
}
