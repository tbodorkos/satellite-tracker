using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SatelliteTracker.Backend.Test
{
    [TestClass]
    public class DataProviderUnitTest
    {
        [TestMethod]
        public void GetDataByPRN_InvalidPRN_UnknownSatelliteData()
        {
            string testPrn = "00";
            string result = DataProvider.GetDataByPRN(testPrn);

            Assert.AreEqual(
                $@"System: Unknown
                    <br />
                    PRN number: 00
                    <br />
                    Launch date: Unknown",
                result
                );
        }

        [TestMethod]
        public void GetDataByPRN_ValidPRN_ValidSatelliteData()
        {
            string testPrn = "01";
            string result = DataProvider.GetDataByPRN(testPrn);

            Assert.AreEqual(
                $@"System: GPS
                    <br />
                    PRN number: 01
                    <br />
                    Launch date: 2011.07.16 06:41",
                result
                );
        }

        [TestMethod]
        public void GetNameByPRN_InvalidPRN_UndefinedSatelliteName()
        {
            string testPrn = "00";
            string result = DataProvider.GetNameByPRN(testPrn);

            Assert.AreEqual("Undefined", result);
        }

        [TestMethod]
        public void GetNameByPRN_ValidPRN_ValidSatelliteName()
        {
            string testPrn = "01";
            string result = DataProvider.GetNameByPRN(testPrn);

            Assert.AreEqual("USA-232", result);
        }
    }
}
