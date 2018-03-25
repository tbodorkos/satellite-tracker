using System;
using System.Linq;
using System.Collections.Generic;

using SatelliteTracker.Backend.Entities;

namespace SatelliteTracker.Backend
{
    public static class DataProvider
    {
        private static IList<SatelliteData> Data = new List<SatelliteData>()
        {
            new SatelliteData() { PRN = "01", Name = "USA-232", System = SystemType.GPS, Launch = DateTime.Parse("2011-07-16 06:41") },
            new SatelliteData() { PRN = "02", Name = "USA-180", System = SystemType.GPS, Launch = DateTime.Parse("2004-11-06 05:39") },
            new SatelliteData() { PRN = "03", Name = "USA-258", System = SystemType.GPS, Launch = DateTime.Parse("2014-10-29 17:21") },
            new SatelliteData() { PRN = "05", Name = "USA-206", System = SystemType.GPS, Launch = DateTime.Parse("2011-07-16 06:41") },
            new SatelliteData() { PRN = "06", Name = "USA-251", System = SystemType.GPS, Launch = DateTime.Parse("2014-05-17 00:03") },
            new SatelliteData() { PRN = "07", Name = "USA-201", System = SystemType.GPS, Launch = DateTime.Parse("2008-03-15 06:10") },
            new SatelliteData() { PRN = "08", Name = "USA-262", System = SystemType.GPS, Launch = DateTime.Parse("2015-07-15 15:36") },
            new SatelliteData() { PRN = "09", Name = "USA-256", System = SystemType.GPS, Launch = DateTime.Parse("2014-09-02 03:23") },
            new SatelliteData() { PRN = "10", Name = "USA-265", System = SystemType.GPS, Launch = DateTime.Parse("2015-10-31 16:13") },
            new SatelliteData() { PRN = "11", Name = "USA-145", System = SystemType.GPS, Launch = DateTime.Parse("1999-10-07 12:51") },
            new SatelliteData() { PRN = "12", Name = "USA-192", System = SystemType.GPS, Launch = DateTime.Parse("2006-11-17 19:12") },
            new SatelliteData() { PRN = "13", Name = "USA-132", System = SystemType.GPS, Launch = DateTime.Parse("1994-07-23 03:43") },
            new SatelliteData() { PRN = "14", Name = "USA-154", System = SystemType.GPS, Launch = DateTime.Parse("2000-11-10 17:14") },
            new SatelliteData() { PRN = "15", Name = "USA-196", System = SystemType.GPS, Launch = DateTime.Parse("2007-10-17 12:23") },
            new SatelliteData() { PRN = "16", Name = "USA-166", System = SystemType.GPS, Launch = DateTime.Parse("2003-01-29 18:06") },
            new SatelliteData() { PRN = "17", Name = "USA-183", System = SystemType.GPS, Launch = DateTime.Parse("2005-09-26 03:37") },
            new SatelliteData() { PRN = "18", Name = "USA-96", System = SystemType.GPS, Launch = DateTime.Parse("1993-10-26 17:04") },
            new SatelliteData() { PRN = "19", Name = "USA-177", System = SystemType.GPS, Launch = DateTime.Parse("2004-03-20 17:53") },
            new SatelliteData() { PRN = "20", Name = "USA-150", System = SystemType.GPS, Launch = DateTime.Parse("2000-05-11 01:48") },
            new SatelliteData() { PRN = "21", Name = "USA-168", System = SystemType.GPS, Launch = DateTime.Parse("2003-03-31 22:09") },
            new SatelliteData() { PRN = "22", Name = "USA-175", System = SystemType.GPS, Launch = DateTime.Parse("2003-12-21 08:05") },
            new SatelliteData() { PRN = "23", Name = "USA-178", System = SystemType.GPS, Launch = DateTime.Parse("2004-06-23 22:54") },
            new SatelliteData() { PRN = "24", Name = "USA-239", System = SystemType.GPS, Launch = DateTime.Parse("2012-10-04 12:10") },
            new SatelliteData() { PRN = "25", Name = "USA-213", System = SystemType.GPS, Launch = DateTime.Parse("2010-05-28 03:00") },
            new SatelliteData() { PRN = "26", Name = "USA-260", System = SystemType.GPS, Launch = DateTime.Parse("2015-03-25 18:36") },
            new SatelliteData() { PRN = "27", Name = "USA-242", System = SystemType.GPS, Launch = DateTime.Parse("2013-05-15 21:38") },
            new SatelliteData() { PRN = "28", Name = "USA-151", System = SystemType.GPS, Launch = DateTime.Parse("2000-07-16 09:17") },
            new SatelliteData() { PRN = "29", Name = "USA-199", System = SystemType.GPS, Launch = DateTime.Parse("2007-12-20 20:04") },
            new SatelliteData() { PRN = "30", Name = "USA-248", System = SystemType.GPS, Launch = DateTime.Parse("2014-02-21 01:59") },
            new SatelliteData() { PRN = "31", Name = "USA-190", System = SystemType.GPS, Launch = DateTime.Parse("2006-09-25 18:50") },
            new SatelliteData() { PRN = "32", Name = "USA-266", System = SystemType.GPS, Launch = DateTime.Parse("2016-02-05 13:38") },
            
            //new SatelliteData() { PRN = "", Name = "", System = SystemType.GPS, Launch = DateTime.Parse("") },
            //new SatelliteData() { PRN = "", Name = "", System = SystemType.GPS, Launch = DateTime.Parse("") },
            //new SatelliteData() { PRN = "", Name = "", System = SystemType.GPS, Launch = DateTime.Parse("") },
            //new SatelliteData() { PRN = "", Name = "", System = SystemType.GPS, Launch = DateTime.Parse("") },
            //new SatelliteData() { PRN = "", Name = "", System = SystemType.GPS, Launch = DateTime.Parse("") },
            //new SatelliteData() { PRN = "", Name = "", System = SystemType.GPS, Launch = DateTime.Parse("") },
        };

        public static String GetDataByPRN(String prn)
        {
            return GetData(prn).ToString();
        }

        public static String GetNameByPRN(String prn)
        {
            return GetData(prn).Name;
        }

        private static SatelliteData GetData(String prn)
        {
            SatelliteData satellite = Data.FirstOrDefault(d => d.PRN.Equals(prn));

            if (satellite == null)
            {
                return new SatelliteData() { PRN = prn, Name = "Undefined", System = SystemType.Unknown, Launch = null };
            }

            return satellite;
        }
    }
}
