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
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Route("Map")]
        public ActionResult Map()
        {
            MapModel mapModel = new MapModel
            {
                Files = FileHandler.GetFileNames()
            };

            return View(mapModel);
        }

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

        [Route("Open")]
        public ActionResult Open(String fileName)
        {
            if (!String.IsNullOrEmpty(fileName))
            {
                List<String> lines = FileHandler.Open(fileName);

                var entityList = NMEAParser.Parse(lines);
                NMEAModel model = new NMEAModel
                {
                    UserCoordinatesList = entityList.Value,
                    SatelliteList = GetSatelliteList(entityList.Key),
                };

                return Json(model);
            }

            return Json("");
        }

        public List<SatelliteModel> GetSatelliteList(List<SatelliteEntity> entities)
        {
            var satelliteElevations = entities.GroupBy(
                p => p.PRN,
                p => p.Elevation,
                (key, g) => new { PRN = key, ElevationList = g.ToList() });

            var satelliteAzimuths = entities.GroupBy(
                p => p.PRN,
                p => p.Azimuth,
                (key, g) => new { PRN = key, AzimuthList = g.ToList() });

            List<SatelliteModel> result = new List<SatelliteModel>();
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
