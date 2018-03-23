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
            new SatelliteData() { PRN = "05", Name = "USA-206", Launch = DateTime.Parse("2011-07-16 06:41") },
            new SatelliteData() { PRN = "30", Name = "USA-248", Launch = DateTime.Parse("2014-02-21 01:59") }
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
                return new SatelliteData() { PRN = "", Name = "", Launch = null };
            }

            return satellite;
        }
    }
}
