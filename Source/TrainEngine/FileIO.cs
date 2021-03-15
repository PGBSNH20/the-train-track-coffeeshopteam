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

        public static List<Station> LoadStations()
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
                if (!int.TryParse(line[0], out int _ID))
                {
                    continue;
                }
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