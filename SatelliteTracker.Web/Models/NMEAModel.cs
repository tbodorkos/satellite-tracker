using System.Collections.Generic;

using SatelliteTracker.Backend.Entites;

namespace SatelliteTracker.Web.Models
{
    public class NMEAModel
    {
        public IList<Coordinates> UserCoordinatesList { get; set; }

        public IList<SatelliteModel> SatelliteList { get; set; }
    }
}
