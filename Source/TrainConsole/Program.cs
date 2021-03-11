using System;
using System.Collections.Generic;
using System.Linq;
using TrainEngine;

namespace TrainConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var trains = FileIO.ReadTrainInfo("Data/trains.txt"); // test
            var stations = FileIO.LoadStation(); // test

            //Fungerar inte, FileIO och metoden är static
            //Test metod
            //var to = new FileIO();
            //var stations = to.LoadStation();

            Console.WriteLine("Train track!");
            // Step 1:
            // Parse the traintrack (Data/traintrack.txt) using ORM (see suggested code)
            // Parse the trains (Data/trains.txt)

            // Step 2:
            // Make the trains run in treads

        }
    }
}
