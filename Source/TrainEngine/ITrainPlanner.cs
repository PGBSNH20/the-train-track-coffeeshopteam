using System;
using System.Collections.Generic;
using System.Text;

namespace TrainEngine
{
    interface ITrainPlanner
    {
        

        // var travelPlan1 = new TrainPlaner(train1)

        ITrainPlanner FollowSchedule(TrainSchedule schedule);
        ITrainPlanner LevelCrossing();
        // .CloseAt("10:23")
        // .OpenAt("10:25")
        // .SetSwitch(switch1, SwitchDirection.Left)
        // .SetSwitch(switch2, SwitchDirection.Right)
        // .ToPlan();
       

    }
}
