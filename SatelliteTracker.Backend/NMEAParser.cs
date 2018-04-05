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

        public static KeyValuePair<IEnumerable<SatelliteEntity>, IEnumerable<Coordinates>> Parse(
            IEnumerable<String> lines)
        {
            String[] lineElements;
            String lineType;

            var satelliteEntityList = new List<SatelliteEntity>();
            var userCoordinatesList = new List<Coordinates>();

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
                    userCoordinatesList.Add(GetUserCoordinates(lineElements));
                }
                else if (lineType == SatelliteSentence)
                {
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
                }
            }

            return new KeyValuePair<IEnumerable<SatelliteEntity>, IEnumerable<Coordinates>>(
                satelliteEntityList,
                userCoordinatesList
                );
        }

        private static Coordinates GetUserCoordinates(String[] lineElements)
        {
            var index = 2;

            var latitude = lineElements[index++];
            var longitude = lineElements[++index];

            if (!String.IsNullOrEmpty(latitude) && !String.IsNullOrEmpty(longitude))
            {
                return FormatToCoordinate(latitude, longitude);
            }

            throw new FormatException();
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
