using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace TrainEngine
{
    public class TravelPlanner : ITravelPlanner
    {
        public TrackDescription TrackDescription { get; set; }
        public List<Station> Stations { get; set; }
        public List<Train> Trains { get; set; }
        private List<TravelPlanData> travelPlanDatas;
        private int selectedTrainID;

        public TravelPlanner()
        {
            travelPlanDatas = new List<TravelPlanData>();
            Stations = FileIO.LoadStations();
            Trains = FileIO.ReadTrainInfo("Data/trains.txt");
        }

        public ITravelPlanner SelectTrain(int id)
        {
            var train = Trains.FirstOrDefault(t => t.ID == id);
            if (train is null)
            {
                throw new Exception("Can´t find this train, please choose another train");
            }

            if (!train.IsOperated)
            {
                throw new Exception("This train is not running");
            }

            selectedTrainID = id;
            return this;
        }

        public ITravelPlanner AddTrack(TrackDescription trackDescription)
        {
            TrackDescription = trackDescription;
            return this;
        }

        public ITravelPlanner StartAt(int stationId, string time)
        {
            if (Stations.FirstOrDefault(s => s.ID == stationId) is null)
            {
                throw new Exception("Can´t find this station, please choose another station");
            }
            TimeSpan parsedTime;
            try
            {
                parsedTime = TimeSpan.Parse(time);
            }
            catch
            {
                throw new Exception("Invalid time format");

            }
            travelPlanDatas.Add(new TravelPlanData { TrainID = selectedTrainID, StartStationID = stationId, StartTime = parsedTime });

            return this;
        }

        public ITravelPlanner ArriveAt(int stationId, string time)
        {
            if (Stations.FirstOrDefault(s => s.ID == stationId) is null)
            {
                throw new Exception("Can´t find this station, please choose another station");
            }
            TimeSpan parsedTime;
            try
            {
                parsedTime = TimeSpan.Parse(time);
            }
            catch
            {
                throw new Exception("Invalid time format");

            }

            // I want to find the last spot of the travelPlanDatas list to add to it, we are adding the arrive at data
            TravelPlanData workingData = travelPlanDatas[^1];
            workingData.ArriveStationID = stationId;
            workingData.ArriveTime = parsedTime;
            travelPlanDatas[^1] = workingData;

            return this;
        }

        private Station GetStationById(int stationId)
        {
            foreach (Station station in Stations)
            {
                if (station.ID == stationId)
                {
                    return station;
                }
            }
            throw new Exception("Can't find the station.");
        }

        public ITravelPlan GeneratePlan()
        {
            if(AreTrainsGoingToCrash())
            {
                throw new Exception("Invalid travel plan, trains are going to crash into each other.");
            }
            return new TravelPlan(travelPlanDatas, TrackDescription);
        }

        private bool AreTrainsGoingToCrash()
        {
            // train1  station1: 10:30 ----------> station2: 12:30
            // train2  station2              11:30 -----------> station1: 13:30

            foreach (TravelPlanData data in travelPlanDatas)
            {
                // IF the plan data is the same as the current element in the foreach then we skip it, 
                // IF any of the stations if our foreach element is also in the planData element
                int[] segmentIds = { data.StartStationID, data.ArriveStationID };
                bool isGoingToCrash = travelPlanDatas.Any(planData =>
                    !planData.Equals(data) &&
                    // check if same track (if both stations exist in both data sets, it must be the same segment)
                    segmentIds.Contains(planData.StartStationID) &&
                    segmentIds.Contains(planData.ArriveStationID) &&
                    // check if times overlap
                    ((planData.StartTime <= data.StartTime && planData.ArriveTime >= data.StartTime) ||
                    (planData.StartTime <= data.ArriveTime && planData.ArriveTime >= data.ArriveTime)));
                if (isGoingToCrash)
                {
                    return isGoingToCrash;
                }
            }
            return false;
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
