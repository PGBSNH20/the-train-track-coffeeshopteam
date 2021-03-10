using System;
using System.Collections.Generic;
using System.Linq;
using TrainEngine;

namespace TrainConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //var to = new TrackOrm();

            //var stations = to.ReadStation();

            // List<TimeTableEvent> timeTable = FileIO.LoadTimeTable("Data/timetable.txt");

            ITravelPlan travelplan1 = new TravelPlan().StartAt("station1", "10:30").ArriveAt("station2", "12:30").GeneratePlan();

            // Step 1:
            // Parse the traintrack (Data/traintrack.txt) using ORM (see suggested code)
            // Parse the trains (Data/trains.txt)

            // Step 2:
            // Make the trains run in treads

        }
    }
}
