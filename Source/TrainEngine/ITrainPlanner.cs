using System;
using System.Collections.Generic;
using System.Text;

namespace TrainEngine
{

    interface ITrainPlanner
    {
        Station Station { get; }
        Train Train { get;}
        ITrainPlanner StartTrain();
        ITrainPlanner StopTrain();
        ITrainPlanner LevelCrossing();
        ITrainPlanner RailSwitch();
        ITrainPlanner ToPlan();
    }
    public class TrainPlanner : ITrainPlanner
    {
        public TrainPlanner(Train train, Station station)
        {
            Train = train;
            Station = station;
        }
        public Train Train;
        public Station Station;

        public ITrainPlanner StartTrain(DateTime time; Station nextStation)
        {
            throw new NotImplementedException();
        }

        public ITrainPlanner LevelCrossing()
        {
            throw new NotImplementedException();
        }

        public ITrainPlanner RailSwitch()
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

    public class Station;
    {
        public int ID { get; set; }
        public string Name { get; set; }
    public bool EndStation;

    }
  
    public class Train;
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int MaxSpeed { get; set; }
        public bool Active { get; set; }

       
    }
   

    public class TimeSchedule
        {
            //public int TrainID;
            public string StartStation;
            public DateTime DepartureTime;
            public string StopStation;
            public DateTime ArrivalTime;
        }
        

}
