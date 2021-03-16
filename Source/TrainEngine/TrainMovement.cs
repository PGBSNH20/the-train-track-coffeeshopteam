using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TrainEngine
{

    public class TrainMovement : ITrainMovement
    {
        public static bool isOpen = true;  // ???
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
            double border1 = 0;
            double border2 = 0;
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
                            if (nextEvent.HasLevelCrossing)
                            {
                                border1 = Math.Round((nextEvent.Distance / 2.0) - Train.MaxSpeed * 5.0 / 60);
                                border2 = Math.Round((nextEvent.Distance / 2.0) + Train.MaxSpeed * 5.0 / 60);
                            }
                        }
                    }
                    else if (nextEvent.HasLevelCrossing && currentPosition >= border1*0.96 && currentPosition <= border1 * 1.04 && isOpen)
                    {
                        Console.WriteLine(FakeClock.FakeTime + " : Level crossing closes");
                        isOpen = false;
                    }
                    else if (nextEvent.HasLevelCrossing && currentPosition >= border2 * 0.96 && currentPosition <= border2 * 1.04 && !isOpen)
                    {
                        Console.WriteLine(FakeClock.FakeTime + " : Level crossing opens");
                        isOpen = true;
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
