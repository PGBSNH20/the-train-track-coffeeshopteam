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

            TrackOrm trackOrm = new TrackOrm();
            string travelPlanPath = "Data/TravelPlan.txt";
            string track1Path = "Data/traintrack1.txt";
            string track2Path = "Data/traintrack2.txt";
            string trainsPath = "Data/trains.txt";
            TrackDescription track1 = trackOrm.LoadTrack(track1Path);
            TrackDescription track2 = trackOrm.LoadTrack(track2Path);
            List<Train> trains = FileIO.ReadTrainInfo(trainsPath);
            
            // List<TimeTableEvent> timeTable = FileIO.LoadTimeTable("Data/timetable.txt");

            ITravelPlan travelplan1 = new TravelPlan().AddTrain(trains[0]).AddTrain(trains[2]).StartAt("station1", "10:30").ArriveAt("station2", "12:30").GeneratePlan();

            travelplan1.Save();

            ITravelPlan travelplan2 = new TravelPlan();
            travelplan2.Load();
            travelplan2.GeneratePlan();

            var trains = FileIO.ReadTrainInfo("Data/trains.txt"); // test
            var stations = FileIO.LoadStation(); // test

            //Fungerar inte, FileIO och metoden är static
            //Test metod
            //var to = new FileIO();
            //var stations = to.LoadStation();


            List<string> trackData = FileIO.GetDataFromFile("Data/traintrack2.txt");
            TrackDescription trackDescription = trackOrm.ParseTrackDescription(trackData);

            trackDescription.StationConnections.ForEach(connection => Console.WriteLine(connection));
        }
    }
}
