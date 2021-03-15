using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TrainEngine;

namespace TrainConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Clock clock1 = new Clock();
            for (int i = 0; i < 15; i++)
            {
                clock1.ShowTime();
                Thread.Sleep(100);
            }

            TrackOrm trackOrm = new TrackOrm();
            string track1Path = "Data/traintrack1.txt";
            TrackDescription track = trackOrm.LoadTrack(track1Path);

            string trainsPath = "Data/trains.txt";
            List<Train> trains = FileIO.ReadTrainInfo(trainsPath);

            
            // List<TimeTableEvent> timeTable = FileIO.LoadTimeTable("Data/timetable.txt");

            //ITravelPlan travelplan1 = new TravelPlan().AddTrain(trains[0]).StartAt("station1", "10:30").ArriveAt("station2", "12:30").GeneratePlan();

            //travelplan1.Save();

            //ITravelPlan travelplan2 = new TravelPlan();
            //travelplan2.Load();
            //travelplan2.GeneratePlan();

            //var trains = FileIO.ReadTrainInfo("Data/trains.txt"); // test
            //var stations = FileIO.LoadStation(); // test

            //Fungerar inte, FileIO och metoden är static
            //Test metod
            //var to = new FileIO();
            //var stations = to.LoadStation();


            List<string> track2Path = FileIO.GetDataFromFile("Data/traintrack2.txt");
            TrackDescription track2 = trackOrm.ParseTrackDescription(track2Path);

            TravelPlan travelPlanSofie = 
                new TravelPlanner()
                .AddTrain(trains[2])
                .AddTrack(track2)
                .StartAt(1, "10:00")
                //.ArriveAt(2, "12:12")
                //.StartAt(2, "12:17")
                .ArriveAt(3, "14:53")
                .GeneratePlan();

            TrainSimulator trainSimulator = new TrainSimulator(clock1);
            trainSimulator.RunSimulation(travelPlanSofie);
                
                

            Console.ReadLine();
            //ITravelPlanner travelplan2 = new TravelPlanner().AddTrain(trains[0]).StartAt(1, "10:30").ArriveAt(2, "12:30").GeneratePlan();
            //travelplan2.Simulate(clock1);
        }
    }
}
