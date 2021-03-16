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

        public TrainMovement()
        {
        }
        public void FollowTravelPlan(ITravelPlanner travelPlan)
        {
            Train = travelPlan.Train;
            TimeTable = travelPlan.TimeTable;
            Console.WriteLine($"Train {Train.Name} is at the {TimeTable[0].StationDeparture.StationName} station");

            trainThread = new Thread(Drive);
            trainThread.Start();
        }


        private static void Drive()
        {
            bool isMoving = true;
            bool flag1 = true;
           
            int i = 0;
            Event nextEvent = TimeTable[i];
            double currentPosition;

            while (isMoving)
            {
                Thread.Sleep(100);

                if (nextEvent.StationDeparture == nextEvent.StationArrival) 
                {
                    if (FakeClock.FakeTime >= nextEvent.TimeDeparture)
                    {
                        i++;
                        nextEvent = TimeTable[i];
                    }
                }
                else
                {
                    currentPosition = 1.0 * (FakeClock.FakeTime.Subtract(nextEvent.TimeDeparture)).TotalHours * Train.MaxSpeed;

                    if (flag1)
                    {
                        if (currentPosition >= 0)
                        {
                            Console.WriteLine(FakeClock.FakeTime + " : " + nextEvent.Train.Name + " departs from the station " + nextEvent.StationDeparture.StationName + " to the " + nextEvent.StationArrival.StationName);
                            flag1 = false;
                        }
                    }
                    else if (currentPosition >= nextEvent.Distance)
                    {
                        Console.WriteLine(FakeClock.FakeTime + " : " + nextEvent.Train.Name + " arrives to the station " + nextEvent.StationArrival.StationName);
                        
                        i++;
                        if (i < TimeTable.Count)
                        {
                            nextEvent = TimeTable[i];
                            flag1 = true;
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
}
