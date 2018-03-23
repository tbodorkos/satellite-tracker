using System;

namespace SatelliteTracker.Backend.Entities
{
    public class SatelliteData
    {
        public String PRN { get; set; }

        public String Name { get; set; }

        public DateTime? Launch { get; set; }

        public override String ToString()
        {
            return "PRN number: " + PRN
                + "<br />"
                + "Launch date: " + (Launch.HasValue ? Launch.Value.ToString("yyyy.MM.dd HH:mm") : "");
        }
    }
}
