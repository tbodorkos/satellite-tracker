namespace SatelliteTracker.Backend.Entites
{
    /// <summary>
    /// SatelliteEntity entity
    /// </summary>
    public class SatelliteEntity
    {
        /// <summary>
        /// PRN number
        /// </summary>
        public string PRN { get; set; }

        /// <summary>
        /// Elevation degree
        /// </summary>
        public int Elevation { get; set; }

        /// <summary>
        /// Azimuth degree
        /// </summary>
        public int Azimuth { get; set; }

        /// <summary>
        /// SNR number
        /// </summary>
        public int SNR { get; set; }
    }
}
