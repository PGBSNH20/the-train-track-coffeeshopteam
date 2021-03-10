﻿using System;
using System.Collections.Generic;
using System.IO;

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

        //public int TrainID { get; set; }
        //public int StationID { get; set; }
        //public DateTime DepartureTime { get; set; }
        //public DateTime ArrivalTime { get; set; }

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
    }

    public class TrackOrm
    {
        // Metoder för att läsa in filer


        public TrackDescription ParseTrackDescription(string track)
        {
            throw new NotImplementedException();
        }
    }
}