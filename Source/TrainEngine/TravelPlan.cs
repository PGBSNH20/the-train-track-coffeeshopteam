using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace TrainEngine
{
    public class TravelPlan : ITravelPlan
    {
        public string StartStation { get; set; }
        public string StartTime { get; set; }
        public string ArriveStation { get; set; }
        public string ArriveTime { get; set; }
        public TrackDescription TrackDescription { get; set; }
        public List<Train> Trains { get; set; } = new List<Train>();

        public ITravelPlan ArriveAt(string station, string time)
        {
            StartStation = station;
            StartTime = time;
            return this;
        }

        public ITravelPlan StartAt(string station, string time)
        {
            ArriveStation = station;
            ArriveTime = time;
            return this;
        }

        public ITravelPlan GeneratePlan()
        {
            Console.WriteLine($"The train starts in {StartStation} station at {StartTime}.");
            Console.WriteLine($"The train arrives in {ArriveStation} station at {ArriveTime}.");
            Trains.ForEach(train => Console.WriteLine("Train name: " + train.Name));
            return this;
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

            StartStation = travelPlan.StartStation;
            StartTime = travelPlan.StartTime;
            ArriveStation = travelPlan.ArriveStation;
            ArriveTime = travelPlan.ArriveTime;
        }

        public ITravelPlan AddTrack(TrackDescription trackDescription)
        {
            TrackDescription = trackDescription;
            return this;
        }

        public ITravelPlan AddTrain(Train train)
        {
            Trains.Add(train);
            return this;
        }
    }
}
