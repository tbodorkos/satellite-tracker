using System;

namespace SatelliteTracker.Backend.Entites
{
    public class SatelliteEntity
    {
        public String PRN { get; set; }

        public Int32 Elevation { get; set; }

        public Int32 Azimuth { get; set; }

        public Int32 SNR { get; set; }
    }
}
