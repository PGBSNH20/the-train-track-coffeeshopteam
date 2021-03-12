using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TrainEngine
{
    public static class FileIO
    {

        public static string[] ReadFile(string path)
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

            return linesOfCSV;
        }

        public static List<string[]> GetDataFromFile(string path, char seperator)
        {
            string[] linesOfCSV = ReadFile(path);

            List<string[]> csvData = new List<string[]>();
            foreach (string line in linesOfCSV)
            {
                string[] csvDataLine = line.Split(seperator);
                csvData.Add(csvDataLine);
            }
            return csvData;
        }

        // to read the train files
        public static List<string> GetDataFromFile(string path)
        {
            string[] linesOfCSV = ReadFile(path);

            List<string> csvData = linesOfCSV.ToList();

            return csvData;
        }

        public static List<Station> LoadStation()
        {
            var path = "Data/stations.txt";
            var stationData = GetDataFromFile(path, '|');
            List<Station> stations = ParseStation(stationData);
            return stations;
        }

        private static List<Station> ParseStation(List<string[]> csvData)
        {
            var list = new List<Station>();

            foreach (string[] line in csvData)
            {
                //if (!int.TryParse(line[0], out int _ID))
                //{
                //    var columns = line.Split('|');
                //    Station s = new Station
                //    {
                //        ID = int.Parse(columns[0]),
                //        StationName = columns[1],
                //        // EndStation = bool.Parse(columns[2])
                //    };
                //    list.Add(s);
                //    continue;
                //}
                Station s = new Station
                {
                    ID = _ID,
                    StationName = line[1],
                    IsEndStation = bool.Parse(line[2])
                };

                list.Add(s);
            }

            return list;
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

        // Moved it from trackorm 
        public static List<Train> ReadTrainInfo(string path)
        {
            List<Train> trains = new List<Train>();
            var data = GetDataFromFile(path, ',');

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
        private static List<TimeTableEvent> ParseTimeTable(List<string[]> csvData)
        {
            List<TimeTableEvent> events = new List<TimeTableEvent>();
            foreach (string[] line in csvData)
            {
                try
                {
                    //int trainId = int.Parse(line[0]);
                    if (!int.TryParse(line[0], out int trainId))
                    {
                        continue;
                    }

                    // trainId is being created in the if above, if it can be parsed to an int
                    int stationId = int.Parse(line[1]);
                    string departureTime = line[2];
                    string arrivalTime = line[3];

                    TimeTableEvent timeTableEvent = new TimeTableEvent();

                    if (departureTime == "null")
                    {
                        timeTableEvent.IsFinalDestination = true;
                    }
                    else if (arrivalTime == "null")
                    {
                        timeTableEvent.IsDeparture = true;
                    }

                    if (departureTime != "null")
                    {
                        timeTableEvent.DepartureTime = DateTime.Parse(departureTime);
                    }

                    if (arrivalTime != "null")
                    {
                        timeTableEvent.ArrivalTime = DateTime.Parse(arrivalTime);
                    }

                    timeTableEvent.TrainID = trainId;
                    timeTableEvent.StationID = stationId;

                    events.Add(timeTableEvent);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e);
                    throw;
                }

                //if (departureTime == "null")
                //{
                //    // final destination                    
                //    timeTableEvent = new TimeTableEvent
                //    {
                //        TrainID = trainId,
                //        StationID = stationId,
                //        ArrivalTime = DateTime.Parse(arrivalTime),
                //        IsFinalDestination = true
                //    };
                //}
                //else if (arrivalTime == "null")
                //{
                //    // departure
                //    timeTableEvent = new TimeTableEvent
                //    {
                //        TrainID = trainId,
                //        StationID = stationId,
                //        DepartureTime = DateTime.Parse(departureTime),
                //        IsDeparture = true
                //    };
                //}
                //else
                //{
                //    timeTableEvent = new TimeTableEvent
                //    {
                //        TrainID = trainId,
                //        StationID = stationId,
                //        DepartureTime = DateTime.Parse(departureTime),
                //        ArrivalTime = DateTime.Parse(arrivalTime)
                //    };
                //}

                // add the timeTableEvent variable to the list of events
                
            }
            return events;
        }

        public static List<TimeTableEvent> LoadTimeTable(string path)
        {
            List<string[]> timeTableData = GetDataFromFile(path, ',');
            List<TimeTableEvent> events = ParseTimeTable(timeTableData);
            return events;
        }

        // Added to chars in order so when the file is getting read it wont think its a new element when reading the file
        public static string Sanitize(string text)
        {
            string sanitized = "";
            foreach (var letter in text)
            {
                if (letter == ',' || letter == '\'' || letter == '-')
                {
                    sanitized += '"' + letter.ToString() + '"';
                }
                else
                {
                    sanitized += letter.ToString();
                }
            }
            return sanitized;
        }
    }
}