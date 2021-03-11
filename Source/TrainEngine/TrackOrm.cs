using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TrainEngine
{
    public static class FileIO
    {
        private static List<string[]> ReadFile(string path, char seperator)
        {
            string[] linesOfCSV;
            try
            {
                linesOfCSV = File.ReadAllLines(path);
            }
            // if the file is not found, we dont close the program if it can't read a file, instead it will make a empty array and let you keep the program open
            catch (FileNotFoundException e)
            {
                linesOfCSV = new string[0];
            }
            // All other exceptions will be caught here, we write throw so the program will just close with all other exceptions.
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

            List<string[]> csvData = new List<string[]>(); 
            foreach (string line in linesOfCSV)
            {
                string[] csvDataLine = line.Split(seperator);
                csvData.Add(csvDataLine);
            }
            return csvData;
        }

        private static List<TimeTableEvent> ParseTimeTable(List<string[]> csvData)
        {
            List<TimeTableEvent> events = new List<TimeTableEvent>();
            foreach (string[] line in csvData)
            {
                TimeTableEvent timeTableEvent;
                //int trainId = int.Parse(line[0]);
                if (!int.TryParse(line[0], out int trainId))
                {
                    continue;
                }

                int stationId = int.Parse(line[1]);
                string departureTime = line[2];
                string arrivalTime = line[3];

                if (departureTime == "null")
                {
                    // final destination                    
                    timeTableEvent = new TimeTableEvent {
                        TrainID = trainId,
                        StationID = stationId,
                        ArrivalTime = DateTime.Parse(arrivalTime),
                        IsFinalDestination = true
                    };
                }
                else if (arrivalTime == "null")
                {
                    // departure
                    timeTableEvent = new TimeTableEvent
                    {
                        TrainID = trainId,
                        StationID = stationId,
                        DepartureTime = DateTime.Parse(departureTime),
                        IsDeparture = true
                    };
                }
                else
                {
                    timeTableEvent = new TimeTableEvent
                    {
                        TrainID = trainId,
                        StationID = stationId,
                        DepartureTime = DateTime.Parse(departureTime),
                        ArrivalTime = DateTime.Parse(arrivalTime)
                    };
                }

                // add the timeTableEvent variable to the list of events
                events.Add(timeTableEvent);
            }
            return events;
        }

        public static TimeTable LoadTimeTable(string path)
        {
            List<string[]> timeTableData = ReadFile(path, ',');
            List<TimeTableEvent> events = ParseTimeTable(timeTableData);
            return new TimeTable { Events = events };
        }
      
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
    } 

    public class TrackOrm
    {
        public TrackDescription ParseTrackDescription(string track)
        {
            throw new NotImplementedException();
        }
    }
}
