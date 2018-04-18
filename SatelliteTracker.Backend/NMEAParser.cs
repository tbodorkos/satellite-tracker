using System;
using System.Linq;
using System.Collections.Generic;
using SatelliteTracker.Backend.Entites;
using System.Globalization;

namespace SatelliteTracker.Backend
{
    /// <summary>
    /// NMEA file parser
    /// </summary>
    public static class NMEAParser
    {
        private static string SatelliteSentence = "$GPGSV";
        private static string PositionSentence = "$GPGGA";

        /// <summary>
        /// Parse NMEA lines
        /// </summary>
        /// <param name="lines">Lines from an NMEA file</param>
        /// <returns>A pair of user coordinates and satellite entities</returns>
        public static KeyValuePair<IEnumerable<SatelliteEntity>, IEnumerable<Coordinates>> Parse(
            IEnumerable<string> lines)
        {
            string[] lineElements;
            string lineType;

            var satelliteEntityList = new List<SatelliteEntity>();
            var userCoordinatesList = new List<Coordinates>();

            foreach (string line in lines)
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

        private static Coordinates GetUserCoordinates(string[] lineElements)
        {
            var index = 2;

            var latitude = lineElements[index++];
            var isNegativeLat = lineElements[index++] == "S";
            var longitude = lineElements[index++];
            var isNegativeLon = lineElements[index] == "W";

            if (!String.IsNullOrEmpty(latitude) && !String.IsNullOrEmpty(longitude))
            {
                return FormatToCoordinate(latitude, longitude, isNegativeLat, isNegativeLon);
            }

            throw new FormatException();
        }

        private static Coordinates FormatToCoordinate(string latitude, string longitude,
            bool isNegativeLat, bool isNegativeLon)
        {
            double latDeg = ParseToDouble(String.Concat(latitude[0], latitude[1]));
            double latMin = ParseToDouble(latitude.Substring(2));

            double lonDeg = ParseToDouble(String.Concat(longitude[0], longitude[1], longitude[2]));
            double lonMin = ParseToDouble(longitude.Substring(3));

            var resultLat = (latDeg + latMin / 60) * (isNegativeLat ? -1 : 1);
            var resultLon = (lonDeg + lonMin / 60) * (isNegativeLon ? -1 : 1);

            return new Coordinates()
            {
                Latitude = resultLat,
                Longitude = resultLon
            };
        }

        private static int ParseToInt(string str)
        {
            return Int32.TryParse(str, out int num) ? num : -1;
        }

        private static double ParseToDouble(string str)
        {
            return Double.TryParse(str, style: NumberStyles.Number,
                provider: CultureInfo.InvariantCulture, result: out double result) ? result : 200;
        }
    }
}
