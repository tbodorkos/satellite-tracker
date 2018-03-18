using System;
using System.Collections.Generic;
using System.Linq;

namespace SatelliteTracker.Backend
{
    public static class NMEAParser
    {
        private static string sentence = "$GPGSV";

        public static List<SatelliteEntity> Parse(List<string> lines)
        {
            string[] lineElements;
            List<SatelliteEntity> entityList = new List<SatelliteEntity>();

            foreach(string line in lines)
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

        private static Int32 ParseToInt(string str)
        {
            return Int32.TryParse(str, out int num) ? num : -1;
        }
    }
}
