using System;
using System.Collections.Generic;
using System.Text;

namespace TrainEngine
{
    public class Event
    {
        public TimeSpan Time { get; set;}
        public Train Train { get; set; }
        public Station Station { get; set; }
        public Action Action { get; set; }
        public Event(TimeSpan time, Train train, Station station, Action action)
        {
            Time = time;
            Train = train;
            Station = station;
            Action = action;
        }
    }
    public enum Action
    {
        departure,
        arrival
    }
}
