using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace TrainEngine
{
    public class TravelPlanner : ITravelPlanner
    {
        public Train Train { get; set; }
        public TrackDescription TrackDescription { get; set; }

        private Station DepartureStation;
        private TimeSpan timeEvent;

        public List<Event> TimeTable { get; set; } = new List<Event>();
        public TravelPlanner(Train train, Station stationBirth)
        {
            Train = train;
            DepartureStation = stationBirth;
        }

        public ITravelPlanner AddTrack(TrackDescription trackDescription)
        {
            TrackDescription = trackDescription;
            return this;
        }

        public ITravelPlanner StartAt(string time)
        {
            timeEvent = TimeSpan.Parse(time);
            TimeTable.Add(new Event(timeEvent, Train, DepartureStation, Action.departure));
            return this;
        }

        public ITravelPlanner ArriveAt(Station stationArrival, string time)
        {
            timeEvent = TimeSpan.Parse(time);
            TimeTable.Add(new Event(timeEvent, Train, stationArrival, Action.arrival));
            DepartureStation = stationArrival;
            return this;
        }

        public ITravelPlanner GeneratePlan()
        {
            if (!Train.IsOperated)
            {
                throw new Exception("This train is not running");
            }
            // kontrollera allt här 
            // kontrollera att TimeTable.Count>0

            // skriva ut på consolen Travel plan
            Console.WriteLine("Travel plan:");
            for (int i = 0; i < TimeTable.Count; i++)
            {
                Event e = TimeTable[i];
                Console.WriteLine(e.Time +" : "+e.Train.Name+" "+e.Station.StationName+" "+e.Action);
            }
            return this;
        }

        public ITravelPlanner OpenLevelCrossing()
        {
            TrackDescription.OpenLevelCrossing();
            return this;
        }

        public ITravelPlanner CloseLevelCrossing()
        {
            TrackDescription.CloseLevelCrossing();
            return this;
        }

    //    public void Save()
    //    {
    //        string jsonString = JsonSerializer.Serialize(this);
    //        File.WriteAllText("Data/TravelPlan.txt", jsonString);
    //    }
    }
}
