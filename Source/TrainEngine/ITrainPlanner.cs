using System;
using System.Collections.Generic;


namespace TrainEngine
{

    public class Passenger
    {
        public int ID { get; set; }
        public string Name { get; set; }

    }

    public class Station
    {
        public int ID { get; set; }
        public string StationName { get; set; }
        public bool IsFinalDestination { get; set; }
    }

    public class Train
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int MaxSpeed { get; set; }
        public bool IsActive { get; set; }
        public Train(int iD, string name, int maxSpeed, bool isActive)
        {
            ID = iD;
            Name = name;
            MaxSpeed = maxSpeed;
            IsActive = isActive;
        }
    }

    public class TimeTable
    {
        public int TrainID { get; set; }
        public int StationID { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
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
        // Declare list of whatever here

    }

    public class TrainPlanner : ITrainPlanner
    {
        public Train Train { get; set; }
        public Station Station { get; set; }
        // Declare list of whatever here

        public TrainPlanner(Train train, Station station)
        {
            Train = train;
            Station = station;
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

        


