using System;
using System.Collections.Generic;
using System.Text;

namespace TrainEngine
{

    interface ITrainPlanner
    {
        int TrainID { get; set; }
        string TrainName { get; set; }
        int MaxSpeed { get; set; }
        int StationID { get; set; } // Till station class
        string StationName { get; set; }
        bool IsActive { get; set; }
        ITrainPlanner StartTrain();
        ITrainPlanner StopTrain();
        ITrainPlanner LevelCrossing();
        ITrainPlanner RailSwitch();
        ITrainPlanner ToPlan();
    }
    public class TrainPlanner : ITrainPlanner
    {
        public TrainPlanner(int trainID, string trainName, int maxSpeed, int stationID, string stationName, bool isActive)
        {
            TrainID = trainID;
            TrainName = trainName;
            MaxSpeed = maxSpeed;
            StationID = stationID;
            StationName = stationName;
            IsActive = isActive;
        }

        public int TrainID { get; set; }
        public string TrainName { get; set; }
        public int MaxSpeed { get; set; }
        public int StationID { get; set; }
        public string StationName { get; set; }
        public bool IsActive { get; set; }

        public ITrainPlanner LevelCrossing()
        {
            throw new NotImplementedException();
        }

        public ITrainPlanner RailSwitch()
        {
            throw new NotImplementedException();
        }

        public ITrainPlanner StartTrain()
        {
            throw new NotImplementedException();
        }

        public ITrainPlanner StopTrain()
        {
            throw new NotImplementedException();
        }

        public ITrainPlanner ToPlan()
        {
            throw new NotImplementedException();
        }
    }




    // var travelPlan1 = new TrainPlaner(train1)

    //ITrainPlanner FollowSchedule(TrainSchedule schedule);
    //ITrainPlanner LevelCrossing();
    // .CloseAt("10:23")
    // .OpenAt("10:25")
    // .SetSwitch(switch1, SwitchDirection.Left)
    // .SetSwitch(switch2, SwitchDirection.Right)
    // .ToPlan();

    /*public class Station
    {
        public int ID { get; set; }
        public string Name { get; set; }


    }

    public class Train
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int MaxSpeed { get; set; }
        public bool Active { get; set; }

        public void StartTrain()
        {

        }

        public void StopTrain()
        {

        }
    }*/

    public class TimeSchedule
        {
            //public int TrainID;
            public string StartStation;
            public DateTime DepartureTime;
            public string StopStation;
            public DateTime ArrivalTime;
        }
        

}
