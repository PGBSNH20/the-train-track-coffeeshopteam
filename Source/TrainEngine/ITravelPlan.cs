using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TrainEngine
{
    public interface ITravelPlan
    {
        string StartStation { get; set; }
        string StartTime { get; set; }
        string ArriveStation { get; set; }
        string ArriveTime { get; set; }
        TrackDescription TrackDescription { get; set; }
        List<Train> Trains { get; set; }

        ITravelPlan StartAt(string station, string time);
        ITravelPlan ArriveAt(string station, string time);
        ITravelPlan AddTrack(TrackDescription trackDescription);
        ITravelPlan AddTrain(Train train);
        ITravelPlan GeneratePlan();

        void Save();
        void Load();
    }
}
