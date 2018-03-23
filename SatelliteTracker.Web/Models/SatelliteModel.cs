using System;
using System.Collections.Generic;

namespace SatelliteTracker.Web.Models
{
    public class SatelliteModel
    {
        public String Name { get; set; }

        public IList<Int32> ElevationList { get; set; }
        
        public IList<Int32> AzimuthList { get; set; }

        public String Information { get; set; }
    }
}