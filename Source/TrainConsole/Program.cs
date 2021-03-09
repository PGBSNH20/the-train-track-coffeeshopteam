using System;
using System.Collections.Generic;
using System.Linq;

namespace TrainConsole
{
    class Program
    {
        public class Station
        {
            public int ID { get; set;}
            public string Name { get; set;}


        }

        public class Train
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public int MaxSpeed { get; set; }
            public bool Active { get; set; }

            public void StartTrain()
            {

            }

            public void StopTrain()
            {

            }
        }

        public class TimeSchedule
        {
            //public int TrainID;
            public string StartStation;
            public DateTime DepartureTime;
            public string StopStation;
            public DateTime ArrivalTime;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Train track!");
            // Step 1:
            // Parse the traintrack (Data/traintrack.txt) using ORM (see suggested code)
            // Parse the trains (Data/trains.txt)

            // Step 2:
            // Make the trains run in treads

        }
    }
}
