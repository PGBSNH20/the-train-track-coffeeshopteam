using System;
using System.Collections.Generic;
using System.Text;

namespace TrainEngine
{
    public interface ITravelPlan
    {
        string StartStation { get; set; }
        string StartTime { get; set; }
        string ArriveStation { get; set; }
        string ArriveTime { get; set; }

        ITravelPlan StartAt(string station, string time);
        ITravelPlan ArriveAt(string station, string time);
        ITravelPlan GeneratePlan();
    }
}
