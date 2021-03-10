using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TrainEngine;

namespace TrainConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            List<Train> trains = ReadTrainInfo(); // test

            Console.WriteLine("Train track!");
            // Step 1:
            // Parse the traintrack (Data/traintrack.txt) using ORM (see suggested code)
            // Parse the trains (Data/trains.txt)

            // Step 2:
            // Make the trains run in treads

        }
        public static List<Train> ReadTrainInfo()
        {
            string path = "trains.txt";
            List<Train> trains = new List<Train>();
            foreach (var item in File.ReadAllLines(path).Skip(1).Select(a => a.Split(',')))
            {
                int trainID = int.Parse(item[0]);
                string name = item[1];
                int maxSpeed = int.Parse(item[2]);
                bool isActive = bool.Parse(item[3]);

                trains.Add(new Train(trainID, name, maxSpeed, isActive));
            }
            return trains;
        }
    }
}
