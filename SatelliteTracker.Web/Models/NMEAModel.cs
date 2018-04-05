using System.Collections.Generic;

using SatelliteTracker.Backend.Entites;

namespace SatelliteTracker.Web.Models
{
    public class NMEAModel
    {
        public IEnumerable<Coordinates> UserCoordinatesList { get; set; }

        public IEnumerable<SatelliteModel> SatelliteList { get; set; }
    }
}
