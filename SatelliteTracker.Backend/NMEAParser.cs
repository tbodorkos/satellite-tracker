using System;
using System.Linq;
using System.Collections.Generic;
using SatelliteTracker.Backend.Entites;

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
            var longitude = lineElements[++index];

            if (!String.IsNullOrEmpty(latitude) && !String.IsNullOrEmpty(longitude))
            {
                return FormatToCoordinate(latitude, longitude);
            }

            throw new FormatException();
        }

        private static Coordinates FormatToCoordinate(string latitude, string longitude)
        {
            return new Coordinates(
                new string(latitude.Where(x => Char.IsLetterOrDigit(x)).ToArray()).Insert(2, "."),
                new string(longitude.Where(x => Char.IsLetterOrDigit(x)).ToArray()).Insert(3, ".")
                );
        }

        private static int ParseToInt(string str)
        {
            return Int32.TryParse(str, out int num) ? num : -1;
        }
    }
}
