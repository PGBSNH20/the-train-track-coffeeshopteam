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

            TrackOrm trackOrm = new TrackOrm();
            string track1Path = "Data/traintrack1.txt";
            TrackDescription track1 = trackOrm.LoadTrack(track1Path);

            List<string> track2Path = FileIO.GetDataFromFile("Data/traintrack2.txt");
            TrackDescription track2 = trackOrm.ParseTrackDescription(track2Path);

            //string trainsPath = "Data/trains.txt";
            //List<Train> trains = FileIO.ReadTrainInfo(trainsPath);

            // ITravelPlan travelPlanSofie = new TravelPlanner().AddTrack(track2).SelectTrain(2).StartAt(1, "10:00").ArriveAt(2, "12:12").GeneratePlan();
            ITravelPlan travelPlanAle = new TravelPlanner().AddTrack(track2).SelectTrain(2).StartAt(1, "10:00").ArriveAt(2, "12:12").StartAt(2, "12:17").ArriveAt(3, "14:53").SelectTrain(3).StartAt(1, "12:30").ArriveAt(2, "17:00").GeneratePlan();
            // ITravelPlan travelPlanShouldCrash = new TravelPlanner().AddTrack(track2).SelectTrain(2).StartAt(1, "10:00").ArriveAt(2, "12:12").SelectTrain(3).StartAt(2, "11:30").ArriveAt(1, "17:00").GeneratePlan();

            //ITravelPlan travelplan2 = new TravelPlanner().AddTrain(trains[0]).StartAt(1, "10:30").ArriveAt(2, "12:30").GeneratePlan();
            //travelplan2.Simulate(clock1);
        }
    }
}
