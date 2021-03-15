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

            //ITravelPlan travelplan2 = new TravelPlanner().AddTrain(trains[0]).StartAt(1, "10:30").ArriveAt(2, "12:30").GeneratePlan();
            //travelplan2.Simulate(clock1);
        }
    }
}
