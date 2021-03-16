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
        Train Train { get; set; }
        List<Event> TimeTable { get; set;}
        ITravelPlanner StartAt(string time);
        ITravelPlanner AddTrack(TrackDescription trackDescription);
        ITravelPlanner AddStop(Station stationIntermidiet, TimeSpan span);
        ITravelPlanner Destination(Station stationDestination);
        ITravelPlanner OpenLevelCrossing();
        ITravelPlanner CloseLevelCrossing();
        ITravelPlanner GeneratePlan();
    }
}
