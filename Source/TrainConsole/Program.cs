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
            string track2Path = "Data/traintrack2.txt";
            TrackDescription track1 = trackOrm.LoadTrack(track1Path);
            TrackDescription track2 = trackOrm.LoadTrack(track2Path);

            ITravelPlan travelPlan =
                new TravelPlanner()
                .AddTrack(track2)
                .SelectTrain(2)
                .StartAt(1, "10:00")
                .ArriveAt(2, "10:56")
                .StartAt(2, "12:17")
                .ArriveAt(3, "13:20")
                //.SelectTrain(3)
                //.StartAt(1, "12:30")
                //.ArriveAt(2, "14:46")
                .GeneratePlan();

            travelPlan.Save();

            //  test to see if the trains would crash:
            // ITravelPlan travelPlanShouldCrash =
            // new TravelPlanner()
            // .AddTrack(track2)
            // .SelectTrain(2).StartAt(1, "10:00").ArriveAt(2, "12:12").SelectTrain(3).StartAt(2, "11:30").ArriveAt(1, "17:00").GeneratePlan();
        }
    }
}
