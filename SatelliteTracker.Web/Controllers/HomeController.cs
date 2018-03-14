using System.Web.Mvc;

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
            return View();
        }

        [Route("Upload")]
        public ActionResult Upload()
        {
            return View();
        }
    }
}