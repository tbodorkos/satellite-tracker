using System;
using System.Web;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using SatelliteTracker.Web.Models;
using SatelliteTracker.Backend;
using SatelliteTracker.Backend.Entites;

namespace SatelliteTracker.Web.Controllers
{
    /// <summary>
    /// Main controller
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Loads Index page
        /// </summary>
        /// <returns>Index page</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Loads Map page
        /// </summary>
        /// <returns>Map page</returns>
        [Route("Map")]
        public ActionResult Map()
        {
            MapModel mapModel = new MapModel
            {
                Files = FileHandler.GetFileNames()
            };

            return View(mapModel);
        }

        /// <summary>
        /// Uploads file
        /// </summary>
        /// <param name="file">Uploaded file</param>
        [HttpPost]
        [Route("Upload")]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            if (file != null)
            {
                FileHandler.Save(file);

                return View();
            }

            return View();
        }

        /// <summary>
        /// Opens selected file by name and creates NMEA Model
        /// </summary>
        /// <param name="fileName">Selected file name</param>
        /// <returns>NMEA Model in JSON</returns>
        [Route("Open")]
        public ActionResult Open(string fileName)
        {
            if (!String.IsNullOrEmpty(fileName))
            {
                var lines = FileHandler.Open(fileName);

                var entityList = NMEAParser.Parse(lines);

                NMEAModel model = new NMEAModel
                {
                    UserCoordinatesList = entityList.Value.ToList(),
                    SatelliteList = GetSatelliteList(entityList.Key).ToList(),
                };

                return Json(model);
            }

            return Json("");
        }

        private IEnumerable<SatelliteModel> GetSatelliteList(IEnumerable<SatelliteEntity> entities)
        {
            var satelliteElevations = entities.GroupBy(
                p => p.PRN,
                p => p.Elevation,
                (key, g) => new { PRN = key, ElevationList = g.ToList() });

            var satelliteAzimuths = entities.GroupBy(
                p => p.PRN,
                p => p.Azimuth,
                (key, g) => new { PRN = key, AzimuthList = g.ToList() });

            var result = new List<SatelliteModel>();
            foreach (var satelliteElevation in satelliteElevations)
            {
                result.Add(new SatelliteModel()
                {
                    Name = DataProvider.GetNameByPRN(satelliteElevation.PRN),
                    ElevationList = satelliteElevation.ElevationList,
                    AzimuthList = satelliteAzimuths.FirstOrDefault(a => a.PRN == satelliteElevation.PRN).AzimuthList,
                    Information = DataProvider.GetDataByPRN(satelliteElevation.PRN)
                });
            }

            return result;
        }
    }
}
