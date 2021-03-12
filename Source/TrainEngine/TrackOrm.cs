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
            //List<int> StationIDs { get; set; }
            //Dictionary<int, List<int>> StationConnections { get; set; }
            //Dictionary<(int, int), int> StationDistances { get; set; }


            TrackDescription trackDescription = new TrackDescription();

            Coordinate startPosition = FindStart(track);
            int distance = 0;
            List<char> trackParts = new List<char>();
            int currentStationId = 0;

            for (int i = 0; i < track[startPosition.LinePosition].Length; i++)
            {
                char symbol = track[startPosition.LinePosition][i];
                // check what symbol it is
                // if - add +10 to our distance variable
                // if [ => must be a station number following
                // if station number, skip next symbol (because it's a ]) and there is no previous station, then save it as from station
                // if it is a station, check if we found a station before (to store distance)
                // and if it's a station pair and all data is stored for it, reset distance and set "from station" to current station
                // if space character => stop parsing this line

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
                       // trackDescription.NumberOfTrackParts += trackParts.Count;

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

        // Metoder för att läsa in filer
        public List<Station> ReadStation()
      
        public static List<Train> ReadTrainInfo(string path)
        {
            List<Train> trains = new List<Train>();
            var data = ReadFile(path, ',');

            try
            {
                foreach (var item in data.Skip(1))
                {
                    int trainID = int.Parse(item[0]);
                    string name = item[1];
                    int maxSpeed = int.Parse(item[2]);
                    bool isActive = bool.Parse(item[3]);

                    trains.Add(new Train(trainID, name, maxSpeed, isActive));
                }
            }
            catch
            {
                throw new Exception("Something went wrong with trains.txt");
            }
            return trains;
        }
    
        private static List<Station> ParseStation(List<string[]> csvData)
        {
            var list = new List<Station>();

            foreach (string[] line in csvData)
            {
                if (!int.TryParse(line[0], out int _ID))
                {
                    var columns = line.Split('|');
                    Station s = new Station
                    {
                        ID = int.Parse(columns[0]),
                        StationName = columns[1],
                       // EndStation = bool.Parse(columns[2])
                    };
                    list.Add(s);
                    continue;
                }
                Station s = new Station
                {
                    ID = _ID,
                    StationName = line[1],
                    EndStation = bool.Parse(line[2])
                };

                list.Add(s);
            }

            return list;
        }

        public static List<Station> LoadStation()
        {
            var path = "Data/stations.txt";
            var stationData = ReadFile(path, '|');
            List<Station> stations = ParseStation(stationData);
            return stations;
        }

        private static List<Passenger> ParsePassenger(List<string[]> csvData)
        {
            var list = new List<Passenger>();

            foreach (string[] line in csvData)
            {
                if (!int.TryParse(line[0], out int _ID))
                {
                    continue;
                }

                Passenger p = new Passenger
                {
                    ID = _ID,
                    Name = line[1],
             
                };

                list.Add(p);
            }
            return list;
          
        }

    public class TrackOrm
    {
        public TrackDescription ParseTrackDescription(string track)
        {
            throw new NotImplementedException();
        }
    }


}
