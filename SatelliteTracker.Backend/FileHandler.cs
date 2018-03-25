using System;
using System.IO;
using System.Web;
using System.Linq;
using System.Collections.Generic;

namespace SatelliteTracker.Backend
{
    public static class FileHandler
    {
        private static String Path = AppDomain.CurrentDomain.BaseDirectory + "UploadedFiles\\";
        private static String Ext = ".nmea";

        public static List<String> Open(String fileName)
        {
            return File.ReadAllLines(Path + fileName + Ext).ToList();
        }

        public static void Save(HttpPostedFileBase file)
        {
            file.SaveAs(Path + file.FileName);
        }

        public static List<String> GetFileNames()
        {
            List<String> fileNameList = new List<String>();

            String[] fileNameArray = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory
                + "UploadedFiles\\", "*.nmea");

            foreach (String fileName in fileNameArray)
            {
                fileNameList.Add(fileName.Split(new String[] { "\\" }, StringSplitOptions.None)
                        .Last().Split('.').First());
            }

            return fileNameList;
        }
    }
}
