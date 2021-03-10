using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TrainEngine
{
    public static class FileIO
    {

        public static List<string[]> ReadFile(string path, char seperator)
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

        // to read the train files
        public static List<string> ReadFile(string path)
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

            List<string> csvData = linesOfCSV.ToList();

            return csvData;
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
            List<string[]> timeTableData = ReadFile(path, ',');
            List<TimeTableEvent> events = ParseTimeTable(timeTableData);
            return events;
        }
    }
}