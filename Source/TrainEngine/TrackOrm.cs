using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TrainEngine
{

    public class Coordinate
    {
        public int LinePosition { get; private set; }
        public int CharacterPosition { get; private set; }

        public Coordinate(int linePosition, int characterPosition)
        {
            LinePosition = linePosition;
            CharacterPosition = characterPosition;
        }
    }

    public class TrackOrm
    {
        public TrackDescription ParseTrackDescription(List<string> track)
        {
            TrackDescription trackDescription = new TrackDescription();

            Coordinate startPosition = FindStart(track);
            int distance = 0;
            List<char> trackParts = new List<char>();
            int currentStationId = 0;

            for (int i = 0; i < track[startPosition.LinePosition].Length; i++)
            {
                char symbol = track[startPosition.LinePosition][i];
               
                // *[1]-------[3]
                // *[1]---=--------[2]-------------[3]

                if (char.IsDigit(symbol))
                {
                    int stationId = int.Parse(symbol.ToString());
                    if (currentStationId == 0)
                    {
                        currentStationId = stationId;
                    }
                    else
                    {
                        var stationConnection = new StationConnection(currentStationId, stationId, distance, trackParts);
                        trackDescription.StationConnections.Add(stationConnection);

                        // Reset values
                        currentStationId = stationId;
                        distance = 0;
                        trackParts = new List<char>();
                    }
                }
                else
                {
                    switch (symbol)
                    {
                        case '-':
                            distance++;
                            break;
                        case '=':
                            trackParts.Add(symbol);
                            break;
                        default:
                            break;
                    }
                }
            }

            return trackDescription;
        }

        public TrackDescription LoadTrack(string path)
        {
            List<string> trackData = FileIO.GetDataFromFile(path);

            return ParseTrackDescription(trackData);
        }

        public Coordinate FindStart(List<string> track)
        {
            for (int i = 0; i < track.Count; i++)
            {
                string line = track[i];
                for (int j = 0; j < line.Length; j++)
                {
                    char symbol = line[j];
                    if (symbol == '*')
                    {
                        return new Coordinate(i, j);
                    }
                }
            }
            throw new Exception("Invalid track data. Couldn't find start(Are you missing an '*'?)");
        }
    }
}
