using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TrainEngine;

namespace TrainConsole
{
    class Program
    {
        public static List<Train> trains = new List<Train>();
        public static List<Station> stations = new List<Station>();
        public static List<TrackDescription> tracks = new List<TrackDescription>();

        static void Main(string[] args)
        {
            
            LoadInfo();

            ITravelPlanner travelPlanTrack2 =
                new TravelPlanner(trains[1], stations[0])
                .AddTrack(tracks[1])
                .StartAt("10:00")
                .AddStop(stations[1], new TimeSpan(0,5,0))
                .Destination(stations[2])
                .GeneratePlan();
            Console.WriteLine("===============================================================");

            //ITravelPlanner travelPlanTrack1 =
            //    new TravelPlanner(trains[1], stations[0])
            //    .AddTrack(tracks[0])
            //    .StartAt("10:00")
            //    .Destination(stations[2])
            //    .GeneratePlan();
            //Console.WriteLine("===============================================================");

            IFakeClock myFakeClock = new FakeClock(new TimeSpan(9, 0, 0));
            myFakeClock.Start();

            ITrainMovement myFirstTravel = new TrainMovement();
            myFirstTravel.FollowTravelPlan(travelPlanTrack2);

        }
        public static void LoadInfo()
        {
            string trainsPath = "Data/trains.txt";
            //string stationsPath = "Data/stations.txt";
            string track1Path = "Data/traintrack1.txt";
            string track2Path = "Data/traintrack2.txt";

            trains = FileIO.ReadTrainInfo(trainsPath);
            stations = FileIO.LoadStations();

            TrackOrm trackOrm = new TrackOrm();
            TrackDescription track1 = trackOrm.LoadTrack(track1Path);
            TrackDescription track2 = trackOrm.LoadTrack(track2Path);
            tracks.Add(track1);
            tracks.Add(track2);
        }
    }
}
