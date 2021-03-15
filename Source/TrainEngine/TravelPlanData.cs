using System;
using System.Collections.Generic;
using System.Text;

namespace TrainEngine
{
    public struct TravelPlanData
    {
        public int TrainID { get; set; }
        public int StartStationID { get; set; }
        public int ArriveStationID { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan ArriveTime { get; set; }
    }
}
