using System;
using System.Linq;
using System.Collections.Generic;

using SatelliteTracker.Backend.Entites;

namespace SatelliteTracker.Backend
{
    public static class NMEAParser
    {
        private static String sentence = "$GPGSV";

        public static List<SatelliteEntity> Parse(List<String> lines)
        {
            String[] lineElements;
            List<SatelliteEntity> entityList = new List<SatelliteEntity>();

            foreach(String line in lines)
            {
                lineElements = line.Split('*').First().Split(',');
                if(lineElements[0] != sentence)
                {
                    continue;
                }

                var index = 4;
                while(index < lineElements.Length)
                {
                    if(String.IsNullOrEmpty(lineElements[index]))
                    {
                        index += 3;
                    }
                    else
                    {
                        entityList.Add(new SatelliteEntity()
                        {
                            PRN = lineElements[index++],
                            Elevation = ParseToInt(lineElements[index++]),
                            Azimuth = ParseToInt(lineElements[index++]),
                            SNR = ParseToInt(lineElements[index++])
                        });
                    }
                }
            }

            return entityList;
        }

        public static Coordinates GetCoordinates(String str)
        {
            string[] coordinates = str.Split(';');
            return new Coordinates(coordinates[0], coordinates[1]);
        }

        private static Int32 ParseToInt(String str)
        {
            return Int32.TryParse(str, out Int32 num) ? num : -1;
        }
    }
}
