using System;
using System.Collections.Generic;
using System.Text;

namespace TrainEngine
{
    public interface ITrainMovement
    {
      //  public void Start();
      //  void Stop();
        public void FollowTravelPlan(ITravelPlanner travelPlan);
    }
}
