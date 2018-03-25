using System;

namespace SatelliteTracker.Backend.Entities
{
    public enum SystemType
    {
        Unknown,
        GPS,
        GLONASS,
        Galileo,
        BeiDou
    }

    public class SatelliteData
    {
        public String PRN { get; set; }

        public String Name { get; set; }

        public SystemType System { get; set; }

        public DateTime? Launch { get; set; }

        public override String ToString()
        {
            return "System: " + (System == SystemType.Unknown ? "Unknown" : System.ToString())
                + "<br />"
                + "PRN number: " + PRN
                + "<br />"
                + "Launch date: " + (Launch.HasValue ? Launch.Value.ToString("yyyy.MM.dd HH:mm") : "Unknown");
        }
    }
}
