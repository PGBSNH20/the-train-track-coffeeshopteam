using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TrainEngine
{
    public interface ITravelPlanner
    {
        TrackDescription TrackDescription { get; set; }
        //List<Train> Trains { get; set; }

        ITravelPlanner StartAt(int stationID, string time);
        ITravelPlanner ArriveAt(int stationID, string time);
        ITravelPlanner AddTrack(TrackDescription trackDescription);
        ITravelPlanner AddTrain(Train train);
        ITravelPlanner OpenLevelCrossing();
        ITravelPlanner CloseLevelCrossing();
        TravelPlan GeneratePlan();
    }
}
