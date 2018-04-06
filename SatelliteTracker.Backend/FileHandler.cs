using System;
using System.IO;
using System.Web;
using System.Linq;
using System.Collections.Generic;

namespace SatelliteTracker.Backend
{
    /// <summary>
    /// File I/O handler
    /// </summary>
    public static class FileHandler
    {
        private static string Path = AppDomain.CurrentDomain.BaseDirectory + "UploadedFiles\\";
        private static string Ext = ".nmea";

        /// <summary>
        /// Open file
        /// </summary>
        /// <param name="fileName">File name without extension</param>
        /// <returns>Lines in file</returns>
        public static IEnumerable<string> Open(string fileName)
        {
            return File.ReadAllLines(Path + fileName + Ext);
        }

        /// <summary>
        /// Save file
        /// </summary>
        /// <param name="file">Uploaded file</param>
        public static void Save(HttpPostedFileBase file)
        {
            file.SaveAs(Path + file.FileName);
        }

        /// <summary>
        /// Get file names in UploadedFiles folder
        /// </summary>
        /// <returns>File names</returns>
        public static IEnumerable<string> GetFileNames()
        {
            var fileNameList = new List<string>();

            string[] fileNameArray = Directory.GetFiles(Path, "*" + Ext);

            foreach (string fileName in fileNameArray)
            {
                fileNameList.Add(fileName.Split(new string[] { "\\" }, StringSplitOptions.None)
                        .Last().Split('.').First());
            }

            return fileNameList;
        }
    }
}
