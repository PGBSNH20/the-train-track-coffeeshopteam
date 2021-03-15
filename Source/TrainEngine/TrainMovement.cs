using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TrainEngine
{
    public class TrainMovement : ITrainMovement
    {
        private Thread trainThread;

        private static Train Train;
        private static List<Event> TimeTable;

        public TrainMovement( )
        {
        }
        public void FollowTravelPlan(ITravelPlanner travelPlan)
        {
            Train = travelPlan.Train;
            TimeTable = travelPlan.TimeTable;
            Console.WriteLine($"Train {Train.Name} is at the {TimeTable[0].Station.StationName} station");

            trainThread = new Thread(Drive);
            trainThread.Start();
            
        }
        private static void Drive()
        {
            bool isMoving = true;
            int i = 0;
            Event nextEvent = TimeTable[i];

            while (isMoving)
            {
                Thread.Sleep(100);

                if (nextEvent.Time <= FakeClock.FakeTime)
                {
                    Console.WriteLine(FakeClock.FakeTime + " : "+ "  object: "+nextEvent.Train.Name+"  station: "+ nextEvent.Station.StationName+"  event:"+nextEvent.Action);
                    i++;
                    if (i < TimeTable.Count)
                    {
                        nextEvent = TimeTable[i];
                    }
                    else
                    {
                        isMoving = false;
                        FakeClock.Stop();
                    }
                }
            }
        }
    }
}
