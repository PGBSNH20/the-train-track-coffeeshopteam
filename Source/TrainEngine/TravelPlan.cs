using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace TrainEngine
{
    public class TravelPlan : ITravelPlan
    {

        private List<TravelPlanData> travelPlanDatas;
        public TrackDescription TrackDescription { get; set; }
        public List<Station> Stations { get; set; }
        public List<Train> Trains { get; set; }
        private Thread simulationThread;


        // variable(s): which train starts where/when and arrives where/when
        // simulate()   the trains driving (incl. opening/closing of level crossings), give each train a thread/access to the clock, so it runs by itself

        public TravelPlan(List<TravelPlanData> travelPlanDatas, TrackDescription trackDescription)
        {
            this.travelPlanDatas = travelPlanDatas;
            TrackDescription = trackDescription;
            Stations = FileIO.LoadStations();
            Trains = FileIO.ReadTrainInfo("Data/trains.txt");

            simulationThread = new Thread(Simulate);
            simulationThread.Start();
        }

        public void Simulate()
        {
            // Set Clock
            TimeSpan earliestStartTime = travelPlanDatas.Min(d => d.StartTime);
            Clock.Time = earliestStartTime.Subtract(TimeSpan.FromMinutes(10));
            Console.WriteLine("Clock set to earliest start time.");

            int maxEventAmount = travelPlanDatas.Count * 2;
            int eventCounter = 0;

            while (true)
            {
                TimeSpan clockTime = Clock.Time;
                string timeString = clockTime.ToString(@"hh\:mm", CultureInfo.InvariantCulture);

                for (int i = 0; i < travelPlanDatas.Count; i++)
                {

                    // check if data HasStarted
                    if (!travelPlanDatas[i].HasStarted && travelPlanDatas[i].StartTime <= clockTime)
                    {
                        string trainName = Trains.Find(train => train.ID == travelPlanDatas[i].TrainID).Name;
                        string stationName = Stations.Find(station => station.ID == travelPlanDatas[i].StartStationID).StationName;
                        travelPlanDatas[i].HasStarted = true;

                        eventCounter++;
                        Console.WriteLine($"[{timeString}]: {trainName} is departing from {stationName}.");
                    }
                    // check if data HasArrived
                    if (!travelPlanDatas[i].HasArrived && travelPlanDatas[i].ArriveTime <= clockTime)
                    {
                        string trainName = Trains.Find(train => train.ID == travelPlanDatas[i].TrainID).Name;
                        string stationName = Stations.Find(station => station.ID == travelPlanDatas[i].StartStationID).StationName;
                        travelPlanDatas[i].HasArrived = true;
                        eventCounter++;
                        Console.WriteLine($"[{timeString}]: {trainName} is arriving at {stationName}.");
                    }
                }

                // TODO fix break condition to break after all trains did their thing instead of a specific time
                if (eventCounter == maxEventAmount)
                {
                    break;
                }
                Thread.Sleep(5);
            }
        }

        public void Save()
        {
            string jsonString = JsonSerializer.Serialize(this);
            File.WriteAllText("Data/TravelPlan.txt", jsonString);
        }

        public void Load()
        {
            string jsonString = File.ReadAllText("Data/TravelPlan.txt");
            TravelPlan travelPlan = JsonSerializer.Deserialize<TravelPlan>(jsonString);

            travelPlanDatas = travelPlan.travelPlanDatas;
        }

    }
}
