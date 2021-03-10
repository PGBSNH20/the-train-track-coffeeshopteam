using System;
using System.Collections.Generic;
using System.Text;

namespace TrainEngine
{
    public class TravelPlan : ITravelPlan
    {
        public string StartStation { get; set; }
        public string StartTime { get; set; }
        public string ArriveStation { get; set; }
        public string ArriveTime { get; set; }

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
            return this;
        }
    }
}
