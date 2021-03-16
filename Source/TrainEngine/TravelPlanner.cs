using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace TrainEngine
{
    public class TravelPlanner : ITravelPlanner
    {
        public Train Train { get; set; }
        public TrackDescription TrackDescription { get; set; }

        private Station DepartureStation;
        private Station ArrivalStation;
        private TimeSpan DepartureTime;
        private TimeSpan ArrivalTime;
        private bool hasLevelCrossing;


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
            DepartureTime = TimeSpan.Parse(time);
            return this;
        }
        public ITravelPlanner AddStop(Station stationIntermidiet, TimeSpan span)
        {
            ArrivalStation = stationIntermidiet;
            List<StationConnection> trackSections = TrackDescription.StationConnections;
            int distance = 10*((StationConnection)trackSections.Single(s => s.StationID == DepartureStation.ID && s.StationIDDestination == ArrivalStation.ID)).Distance;
            int travelTime = 60*distance / Train.MaxSpeed;
            ArrivalTime = DepartureTime.Add(new TimeSpan(0,travelTime,0));

            StationConnection connectionWithCrossing = trackSections.FirstOrDefault(s => s.StationID == DepartureStation.ID && s.StationIDDestination == ArrivalStation.ID && s.TrackParts.Count == 1);

            if (connectionWithCrossing != null)
            {
                hasLevelCrossing = true;
            }
            else
            {
                hasLevelCrossing = false;
            }

            TimeTable.Add(new Event(Train, DepartureTime, ArrivalTime, DepartureStation, ArrivalStation, distance, hasLevelCrossing));

            DepartureStation = ArrivalStation;
            DepartureTime = ArrivalTime;
            ArrivalTime = DepartureTime.Add(span);

            TimeTable.Add(new Event(Train, DepartureTime, ArrivalTime, DepartureStation, ArrivalStation, 0, false));
            DepartureTime = ArrivalTime;
            return this;
        }
        public ITravelPlanner Destination(Station stationDestination)
        {
            ArrivalStation = stationDestination;            // upprepning
            List<StationConnection> trackSections = TrackDescription.StationConnections;
            int distance = 10*((StationConnection)trackSections.Single(s => s.StationID == DepartureStation.ID && s.StationIDDestination == ArrivalStation.ID)).Distance;
            int travelTime = 60 * distance / Train.MaxSpeed;
            ArrivalTime = DepartureTime.Add(new TimeSpan(0, travelTime, 0));
            StationConnection connectionWithCrossing = trackSections.FirstOrDefault(s => s.StationID == DepartureStation.ID && s.StationIDDestination == ArrivalStation.ID && s.TrackParts.Count == 1);

            if (connectionWithCrossing != null)
            {
                hasLevelCrossing = true;
            }
            else
            {
                hasLevelCrossing = false;
            }

            TimeTable.Add(new Event(Train, DepartureTime, ArrivalTime, DepartureStation, ArrivalStation, distance, hasLevelCrossing));
            DepartureStation = ArrivalStation;
            DepartureTime = ArrivalTime;
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

            // skriva ut Travel plan på consolen 
            Console.WriteLine("Travel plan:");
            for (int i = 0; i < TimeTable.Count; i++)
            {
                Event e = TimeTable[i];
                Console.WriteLine(e.Train.Name + " "+ e.TimeDeparture + "-" +e.TimeArrival + "  "+ e.StationDeparture.StationName+"-"+e.StationArrival.StationName+" "+e.Distance+" level crossing: "+e.HasLevelCrossing);
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
