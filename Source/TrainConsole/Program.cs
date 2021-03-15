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
            {
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


                //List<string> trackData = FileIO.GetDataFromFile("Data/traintrack2.txt");
                //TrackDescription trackDescription = trackOrm.ParseTrackDescription(trackData);

                //ITravelPlanner travelPlanSofie = new TravelPlanner().AddTrain(trains[3]).StartAt(1, "10:00").ArriveAt(2, "12:12").StartAt(2, "12:17").ArriveAt(3, "14:53").GeneratePlan();

                //ITravelPlanner travelplan2 = new TravelPlanner().AddTrain(trains[0]).StartAt(1, "10:30").ArriveAt(2, "12:30").GeneratePlan();
                //travelplan2.Simulate(clock1);
            }
            ITravelPlanner travelPlanSvetlana =
                new TravelPlanner(trains[1], stations[0])
                .AddTrack(tracks[1])
                .StartAt("10:00")
                .ArriveAt(stations[1], "10:30")
                .StartAt("10:35")
                .ArriveAt(stations[2], "11:05")
                .GeneratePlan();
            Console.WriteLine("===============================================================");

            IFakeClock myFakeClock = new FakeClock(new TimeSpan(9,0,0));
            myFakeClock.Start();

            ITrainMovement myFirstTravel = new TrainMovement();
            myFirstTravel.FollowTravelPlan(travelPlanSvetlana);

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
