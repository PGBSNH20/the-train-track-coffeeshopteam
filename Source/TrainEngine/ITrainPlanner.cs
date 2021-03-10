using System;
using System.Collections.Generic;
using System.Text;

namespace TrainEngine
{

    public class Passenger
    {
        public int ID { get; set; }
        public string Name { get; set; }

    }

    public class Train
    { 
        public int ID { get; set; }
        public string Name { get; set; }
        public int MaxSpeed { get; set; }
        public bool IsActive { get; set; }
    }

    //TraindId,StationId,DepartureTime,ArrivalTime
    //2,1,10:20,null
    //2,2,10:45,10:43
    //2,3,null,10:59
    //3,3,10:23,null
    //3,4,10:55,10:53
    //3,1,null,11:15

    public class TimeTableEvent
    {
        public int TrainID { get; set; }
        public int StationID { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public bool IsDeparture { get; set; }
        public bool IsFinalDestination { get; set; }
    }

    //public class TrialAndError
    //{
    //    bool IsDeparture;
    //    public TrialAndError()
    //    {
    //        TimeTableEvent event1 = new TimeTableEvent { TrainID = 2, StationID = 1, DepartureTime = new DateTime(2021, 3, 10, 10, 20, 0) };
    //        if (event1.ArrivalTime == null)
    //        {
    //            IsDeparture = true;
    //        }
    //    }
    //}

    public class TimeTable
    {
        public List<TimeTableEvent> Events { get; set; }
    }

    // maybe use enum for the switch direction.
    enum SwitchDirection
    {
        Left,
        Right
    }

    // maybe we dont need. if not use a global bool that checks if true or not.
    public class TrainSwitch
    {
    }

    public class TrainTrack
    {
    }

    // Maybe its needed maybe not? if not, just make the boolean a global variable.
    public class LevelCrossing
    {
        // A bool to say if its open or closed
        public bool IsOpen { get; set; }
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

        


