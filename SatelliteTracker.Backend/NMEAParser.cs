using System;
using System.Linq;
using System.Collections.Generic;

using SatelliteTracker.Backend.Entites;

namespace SatelliteTracker.Backend
{
    public static class NMEAParser
    {
        private static String SatelliteSentence = "$GPGSV";
        private static String PositionSentence = "$GPGGA";

        public static KeyValuePair<List<SatelliteEntity>, List<Coordinates>> Parse(List<String> lines)
        {
            String[] lineElements;
            String lineType;

            List<SatelliteEntity> satelliteEntityList = null;
            List<Coordinates> userCoordinatesList = null;

            foreach (String line in lines)
            {
                lineElements = line.Split('*').First().Split(',');
                lineType = lineElements[0];

                if (lineType != SatelliteSentence && lineType != PositionSentence)
                {
                    continue;
                }
                else if (lineType == PositionSentence)
                {
                    userCoordinatesList = GetUserCoordinates(lineElements);

                }
                else if (lineType == SatelliteSentence)
                {
                    satelliteEntityList = GetSatelliteEntities(lineElements);
                }
            }

            return new KeyValuePair<List<SatelliteEntity>, List<Coordinates>>(
                satelliteEntityList,
                userCoordinatesList
                );
        }

        private static List<SatelliteEntity> GetSatelliteEntities(string[] lineElements)
        {
            var satelliteEntityList = new List<SatelliteEntity>();
            var index = 4;

            while (index < lineElements.Length)
            {
                if (String.IsNullOrEmpty(lineElements[index]))
                {
                    index += 3;
                }
                else
                {
                    satelliteEntityList.Add(new SatelliteEntity()
                    {
                        PRN = lineElements[index++],
                        Elevation = ParseToInt(lineElements[index++]),
                        Azimuth = ParseToInt(lineElements[index++]),
                        SNR = ParseToInt(lineElements[index++])
                    });
                }
            }

            return satelliteEntityList;
        }

        private static List<Coordinates> GetUserCoordinates(String[] lineElements)
        {
            var coordinates = new List<Coordinates>();
            var index = 2;

            var latitude = lineElements[index++];
            var longitude = lineElements[++index];

            if (!String.IsNullOrEmpty(latitude) && !String.IsNullOrEmpty(longitude))
            {
                coordinates.Add(FormatToCoordinate(latitude, longitude));
            }

            return coordinates;
        }

        private static Coordinates FormatToCoordinate(String latitude, String longitude)
        {
            return new Coordinates(
                new String(latitude.Where(x => Char.IsLetterOrDigit(x)).ToArray()).Insert(2, "."),
                new String(longitude.Where(x => Char.IsLetterOrDigit(x)).ToArray()).Insert(3, ".")
                );
        }

        private static Int32 ParseToInt(String str)
        {
            return Int32.TryParse(str, out Int32 num) ? num : -1;
        }
    }
}
