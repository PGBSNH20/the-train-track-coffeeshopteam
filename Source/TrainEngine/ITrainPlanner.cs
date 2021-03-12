using System;
using System.Collections.Generic;
using System.Text;

namespace TrainEngine
{
    public class TimeTableEvent
    {
        public int TrainID { get; set; }
        public int StationID { get; set; }
        public DateTime? DepartureTime { get; set; }
        public DateTime? ArrivalTime { get; set; }
        public bool IsDeparture { get; set; }
        public bool IsFinalDestination { get; set; }
    }

    public interface ITrainPlanner
    {
        Station Station { get; }
        Train Train { get; }
        ITrainPlanner StartTrain(DateTime time, Station nextStation);
        ITrainPlanner StopTrain();
        ITrainPlanner LevelCrossing();
        ITrainPlanner RailSwitch();
        ITrainPlanner ToPlan();
        List<Station> Stations { get; }
        // Declare list of whatever here

    }

    public class TrainPlanner : ITrainPlanner
    {
        public Train Train { get; set; }
        public Station Station { get; set; }
        public List<Station> Stations { get; set; }

        // Declare list of whatever here

        public TrainPlanner(Train train, Station station)
        {
            Train = train;
            Station = station;
            Stations = new List<Station>();
        }

        public ITrainPlanner StartTrain(DateTime time, Station nextStation)
        {
            return this;
        }

        public ITrainPlanner LevelCrossing()
        {
            return this;
        }

        public ITrainPlanner RailSwitch()
        {
            return this;
        }

        public ITrainPlanner StopTrain()
        {
            return this;
        }

        public ITrainPlanner ToPlan()
        {

            return this;
        }

        public ITrainPlanner StartTrain()
        {
            return this;
        }
    }
}

        


