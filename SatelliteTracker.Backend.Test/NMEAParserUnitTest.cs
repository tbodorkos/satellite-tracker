using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace SatelliteTracker.Backend.Test
{
    [TestClass]
    public class NMEAParserUnitTest
    {
        [TestMethod]
        public void Parse_ZeroSatelliteData_ZeroListLength()
        {
            string testData = "$GPGSV,3,1,12,,,,,,,,,,,,,,,,*76";
            int expectedCount = 0;

            List<SatelliteEntity> entityList = NMEAParser.Parse(new List<string>(new string[] { testData }));

            Assert.AreEqual(expectedCount, entityList.Count);
        }

        [TestMethod]
        public void Parse_OneSatelliteData_OneListLength()
        {
            string testData = "$GPGSV,3,1,12,30,72,253,36,,,,,,,,,,,,*76";
            int expectedCount = 1;

            List<SatelliteEntity> entityList = NMEAParser.Parse(new List<string>(new string[] { testData }));

            Assert.AreEqual(expectedCount, entityList.Count);
        }

        [TestMethod]
        public void Parse_TwoSatelliteData_TwoListLength()
        {
            string testData = "$GPGSV,3,1,12,30,72,253,36,05,70,124,42,,,,,,,,*76";
            int expectedCount = 2;

            List<SatelliteEntity> entityList = NMEAParser.Parse(new List<string>(new string[] { testData }));

            Assert.AreEqual(expectedCount, entityList.Count);
        }

        [TestMethod]
        public void Parse_ThreeSatelliteData_ThreeListLength()
        {
            string testData = "$GPGSV,3,1,12,30,72,253,36,05,70,124,42,24,37,082,43,,,,*76";
            int expectedCount = 3;

            List<SatelliteEntity> entityList = NMEAParser.Parse(new List<string>(new string[] { testData }));

            Assert.AreEqual(expectedCount, entityList.Count);
        }

        [TestMethod]
        public void Parse_FourSatelliteData_FourListLength()
        {
            string testData = "$GPGSV,3,1,12,30,72,253,36,05,70,124,42,24,37,082,43,02,37,112,45*76";
            int expectedCount = 4;

            List<SatelliteEntity> entityList = NMEAParser.Parse(new List<string>(new string[] { testData }));

            Assert.AreEqual(expectedCount, entityList.Count);
        }

        [TestMethod]
        public void Parse_ValidSatelliteData_ValidEntity()
        {
            string testData = "$GPGSV,3,2,12,04,32,058,44,,,,,,,,,,,,*71";
            SatelliteEntity expectedEntity = new SatelliteEntity()
            {
                PRN = "04",
                Elevation = 32,
                Azimuth = 58,
                SNR = 44
            };

            List<SatelliteEntity> entityList = NMEAParser.Parse(new List<string>(new string[] { testData }));
            SatelliteEntity entity = entityList[0];

            Assert.AreEqual(expectedEntity.PRN, entity.PRN);
            Assert.AreEqual(expectedEntity.Elevation, entity.Elevation);
            Assert.AreEqual(expectedEntity.Azimuth, entity.Azimuth);
            Assert.AreEqual(expectedEntity.SNR, entity.SNR);
        }
    }
}
