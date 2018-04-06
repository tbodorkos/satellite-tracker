using System.Collections.Generic;

namespace SatelliteTracker.Web.Models
{
    /// <summary>
    /// Map Model
    /// </summary>
    public class MapModel
    {
        /// <summary>
        /// File names in UploadedFiles folder
        /// </summary>
        public IEnumerable<string> Files { get; set; }
    }
}
