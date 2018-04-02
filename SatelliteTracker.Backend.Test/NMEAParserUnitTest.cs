using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SatelliteTracker.Backend.Entites;

namespace SatelliteTracker.Backend.Test
{
    [TestClass]
    public class NMEAParserUnitTest
    {
        [TestMethod]
        public void Parse_ZeroSatelliteData_ZeroListLength()
        {
            String testData = "$GPGSV,3,1,12,,,,,,,,,,,,,,,,*76";
            Int32 expectedCount = 0;

            List<SatelliteEntity> entityList = NMEAParser.Parse(new List<String>(new String[] { testData }));

            Assert.AreEqual(expectedCount, entityList.Count);
        }

        [TestMethod]
        public void Parse_OneSatelliteData_OneListLength()
        {
            String testData = "$GPGSV,3,1,12,30,72,253,36,,,,,,,,,,,,*76";
            Int32 expectedCount = 1;

            List<SatelliteEntity> entityList = NMEAParser.Parse(new List<String>(new String[] { testData }));

            Assert.AreEqual(expectedCount, entityList.Count);
        }

        [TestMethod]
        public void Parse_TwoSatelliteData_TwoListLength()
        {
            String testData = "$GPGSV,3,1,12,30,72,253,36,05,70,124,42,,,,,,,,*76";
            Int32 expectedCount = 2;

            List<SatelliteEntity> entityList = NMEAParser.Parse(new List<String>(new String[] { testData }));

            Assert.AreEqual(expectedCount, entityList.Count);
        }

        [TestMethod]
        public void Parse_ThreeSatelliteData_ThreeListLength()
        {
            String testData = "$GPGSV,3,1,12,30,72,253,36,05,70,124,42,24,37,082,43,,,,*76";
            Int32 expectedCount = 3;

            List<SatelliteEntity> entityList = NMEAParser.Parse(new List<String>(new String[] { testData }));

            Assert.AreEqual(expectedCount, entityList.Count);
        }

        [TestMethod]
        public void Parse_FourSatelliteData_FourListLength()
        {
            String testData = "$GPGSV,3,1,12,30,72,253,36,05,70,124,42,24,37,082,43,02,37,112,45*76";
            Int32 expectedCount = 4;

            List<SatelliteEntity> entityList = NMEAParser.Parse(new List<String>(new String[] { testData }));

            Assert.AreEqual(expectedCount, entityList.Count);
        }

        [TestMethod]
        public void Parse_ValidSatelliteData_ValidEntity()
        {
            String testData = "$GPGSV,3,2,12,04,32,058,44,,,,,,,,,,,,*71";
            SatelliteEntity expectedEntity = new SatelliteEntity()
            {
                PRN = "04",
                Elevation = 32,
                Azimuth = 58,
                SNR = 44
            };

            List<SatelliteEntity> entityList = NMEAParser.Parse(new List<String>(new String[] { testData }));
            SatelliteEntity entity = entityList[0];

            Assert.AreEqual(expectedEntity.PRN, entity.PRN);
            Assert.AreEqual(expectedEntity.Elevation, entity.Elevation);
            Assert.AreEqual(expectedEntity.Azimuth, entity.Azimuth);
            Assert.AreEqual(expectedEntity.SNR, entity.SNR);
        }
    }
}
