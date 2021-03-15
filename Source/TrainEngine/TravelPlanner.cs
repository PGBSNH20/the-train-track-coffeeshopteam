using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace TrainEngine
{
    public class TravelPlanner : ITravelPlanner
    {
        /* travel planner */
        // AddTrain();  *
        // AddTrack();  *
        // StartAt();   *
        // ArriveAt();  *
        // GeneratePlan();
        public TrackDescription TrackDescription { get; set; }
        public List<Station> stations;
        public List<Train> Trains { get; set; } = new List<Train>();

        private List<TravelPlanData> travelPlanDatas;
        private int startStationId;
        private int arriveStationId;
        private TimeSpan startTime;
        private TimeSpan arriveTime;
        private int trainID;

        public TravelPlanner()
        {
            stations = FileIO.LoadStations();
        }

        //public TimeSpan ToTimeSpan(string time)
        //{
        //    TimeSpan timeSpan = TimeSpan.Parse(time);
        //    return timeSpan;
        //}

        public ITravelPlanner AddTrain(Train train)
        {
            // Only want to add the train once to the list
            if (!Trains.Contains(train))
            {
                Trains.Add(train);
            }
            trainID = train.ID;
            return this;
        }

        public ITravelPlanner AddTrack(TrackDescription trackDescription)
        {
            TrackDescription = trackDescription;
            return this;
        }

        public ITravelPlanner StartAt(int stationId, string time)
        {
            // startStation = GetStationById(stationId);
            startStationId = stationId;
            startTime = TimeSpan.Parse(time);
            bool dataExists = false;

            for (int i = 0; i < travelPlanDatas.Count; i++)
            {
                TravelPlanData travelPlanData = travelPlanDatas[i];
                if (travelPlanData.TrainID == trainID &&
                    travelPlanData.ArriveStationID == stationId &&
                    travelPlanData.ArriveTime == startTime)
                {
                    dataExists = true;
                    travelPlanData.StartStationID = stationId;
                    travelPlanData.StartTime = startTime;
                    break;
                }
            }

            travelPlanDatas.Add(new TravelPlanData { TrainID = trainID, StartStationID = stationId, StartTime = startTime });

            return this;
        }

        public ITravelPlanner ArriveAt(int stationId, string time)
        {
            //arriveStationId = stationId;
            //arriveTime = TimeSpan.Parse(time);
            //bool dataExists = false;

            //for (int i = 0; i < travelPlanDatas.Count; i++)
            //{
            //    TravelPlanData data = travelPlanDatas[i];
            //    if (data.TrainID == trainID &&
            //        data.StartStationID == stationId &&
            //        data.StartTime == startTime)
            //    {
            //        dataExists = true;
            //        data.ArriveStationID = stationId;
            //        data.ArriveTime = arriveTime;
            //        break;
            //    }
            //}

            //if (!dataExists)
            //{
            //    travelPlanDatas.Add(new TravelPlanData { TrainID = trainID, ArriveStationID = stationId, ArriveTime = arriveTime });
            //}

            return this;
        }

        private Station GetStationById(int stationId)
        {
            // find stationID int to the file stationid
            foreach (Station station in stations)
            {
                if (station.ID == stationId)
                {
                    return station;
                }
            }
            throw new Exception("Can't find the station.");
        }

        // Later it should return a ITravelPlan not a ITravelPlanner
        public ITravelPlanner GeneratePlan()
        {
            //Console.WriteLine($"The train starts in {startStation} station at {startTime}.");
            //Console.WriteLine($"The train arrives in {arriveStation} station at {arriveTime}.");
            Trains.ForEach(train => Console.WriteLine("Train name: " + train.Name));
            return this;
        }

        public ITravelPlanner OpenLevelCrossing()
        {
            TrackDescription.OpenLevelCrossing();
            return this;
        }

        public ITravelPlanner CloseLevelCrossing()
        {
            TrackDescription.CloseLevelCrossing();
            return this;
        }
    }
}
