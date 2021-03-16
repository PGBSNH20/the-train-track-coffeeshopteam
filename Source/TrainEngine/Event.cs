using System;
using System.Collections.Generic;
using System.Text;

namespace TrainEngine
{
    public class Event
    {

        public Train Train { get; set; }
        public TimeSpan TimeDeparture { get; set;}
        public TimeSpan TimeArrival { get; set; }
        public Station StationDeparture { get; set; }
        public Station StationArrival { get; set; }
        public int Distance { get; set; }
        public bool HasLevelCrossing { get; set; }
        public Event(Train train, TimeSpan timeDeparture, TimeSpan timeArrival, Station stationDeparture, Station stationArrival, int distance, bool hasLevelCrossing)
        {
            Train = train;
            TimeDeparture = timeDeparture;
            TimeArrival = timeArrival;
            StationDeparture = stationDeparture;
            StationArrival = stationArrival;
            Distance = distance;
            HasLevelCrossing = hasLevelCrossing;
        }
    }
}
