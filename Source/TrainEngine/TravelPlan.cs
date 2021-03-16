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
        public static bool isOpen = true;
        private bool hasLevelCrossing;

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
            TimeSpan earliestStartTime = travelPlanDatas.Min(d => d.StartTime);
            Clock.Time = earliestStartTime.Subtract(TimeSpan.FromMinutes(10));
            Console.WriteLine($"Clock set to: {earliestStartTime}.");
            Clock.IsPrintingTime = true;

            int maxEventAmount = travelPlanDatas.Count * 2;
            int eventCounter = 0;

            while (true)
            {
                TimeSpan clockTime = Clock.Time;
                string timeString = clockTime.ToString(@"hh\:mm", CultureInfo.InvariantCulture);
                for (int i = 0; i < travelPlanDatas.Count; i++)
                {

                    TravelPlanData data = travelPlanDatas[i];

                    // check if data HasStarted
                    if (!data.HasStarted && data.StartTime <= clockTime)
                    {
                        string trainName = Trains.Find(train => train.ID == data.TrainID).Name;
                        string stationName = Stations.Find(station => station.ID == data.StartStationID).StationName;
                        travelPlanDatas[i].HasStarted = true;

                        TimeSpan travelTime = CalculateTravelTime(data);
                        int timeDeviationInMinutes = GetTimeDeviationInMinutes(data.StartTime, data.ArriveTime, travelTime);
                        travelPlanDatas[i].TimeDeviationInMinutes = timeDeviationInMinutes;
                        travelPlanDatas[i].ArriveTime = data.StartTime.Add(travelTime);

                        eventCounter++;

                        string output = $"[{timeString}]: {trainName} is departing from {stationName}.";
                        Console.WriteLine(output);
                    }

                    // check if data HasArrived
                    else if (!data.HasArrived && data.ArriveTime <= clockTime)
                    {
                        string trainName = Trains.Find(train => train.ID == data.TrainID).Name;
                        string stationName = Stations.Find(station => station.ID == data.ArriveStationID).StationName;

                        travelPlanDatas[i].HasArrived = true;

                        eventCounter++;

                        string output = $"[{timeString}]: {trainName} is arriving at {stationName}.";
                        if (data.TimeDeviationInMinutes > 0)
                        {
                            output += $" The train was delayed by {data.TimeDeviationInMinutes} minutes.";
                        }
                        else if (data.TimeDeviationInMinutes < 0)
                        {
                            output += $" The train is early by {Math.Abs(data.TimeDeviationInMinutes)} minutes.";
                        }
                        Console.WriteLine(output);
                    }
                }

                if (eventCounter == maxEventAmount)
                {
                    break;
                }
                Thread.Sleep(10);
            }
        }



        private int GetTimeDeviationInMinutes(TimeSpan startTime, TimeSpan arriveTime, TimeSpan travelTime)
        {
            int assumedDurationInMinutes = (int)(arriveTime - startTime).TotalMinutes;
            int actualDurationInMinutes = (int)travelTime.TotalMinutes;
            return actualDurationInMinutes - assumedDurationInMinutes;
        }

        private TimeSpan CalculateTravelTime(TravelPlanData data)
        {
            int trainSpeed = Trains.Find(t => t.ID == data.TrainID).MaxSpeed;
            StationConnection currentConnection = GetStationConnection(data.StartStationID, data.ArriveStationID);
            int distanceFactor = 10;
            int distanceInKm = currentConnection.Distance * distanceFactor;
            
            int travelDurationInMinutes = (int)Math.Ceiling(Convert.ToDouble(distanceInKm) / trainSpeed * 60);

            return new TimeSpan().Add(TimeSpan.FromMinutes(travelDurationInMinutes));
        }

        private StationConnection GetStationConnection(int startStationID, int arriveStationID)
        {
            // looking for the station connection to find the right connection (the connection between station1 and station 2)
            int[] stationSegment = { startStationID, arriveStationID };
            return TrackDescription.StationConnections.Find(
                sc => stationSegment.Contains(sc.StartStationID) &&
                stationSegment.Contains(sc.ArriveStationID));
        }

        public void Save()
        {
            string jsonString = JsonSerializer.Serialize(this);
            File.WriteAllText("Data/TravelPlan.json", jsonString);
        }

        public void Load()
        {
            string jsonString = File.ReadAllText("Data/TravelPlan.json");
            TravelPlan travelPlan = JsonSerializer.Deserialize<TravelPlan>(jsonString);

            travelPlanDatas = travelPlan.travelPlanDatas;
        }

    }
}
