using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SatelliteTracker.Backend.Test
{
    [TestClass]
    public class DataProviderUnitTest
    {
        [TestMethod]
        public void GetDataByPRN_InvalidPRN_UnknownSatelliteData()
        {
            String testPrn = "00";
            String result = DataProvider.GetDataByPRN(testPrn);

            Assert.AreEqual(
                "System: Unknown<br />PRN number: 00<br />Launch date: Unknown",
                result
                );
        }

        [TestMethod]
        public void GetDataByPRN_ValidPRN_ValidSatelliteData()
        {
            String testPrn = "01";
            String result = DataProvider.GetDataByPRN(testPrn);

            Assert.AreEqual(
                "System: GPS<br />PRN number: 01<br />Launch date: 2011.07.16 06:41",
                result
                );
        }

        [TestMethod]
        public void GetNameByPRN_InvalidPRN_UndefinedSatelliteName()
        {
            String testPrn = "00";
            String result = DataProvider.GetNameByPRN(testPrn);

            Assert.AreEqual("Undefined", result);
        }

        [TestMethod]
        public void GetNameByPRN_ValidPRN_ValidSatelliteName()
        {
            String testPrn = "01";
            String result = DataProvider.GetNameByPRN(testPrn);

            Assert.AreEqual("USA-232", result);
        }
    }
}
