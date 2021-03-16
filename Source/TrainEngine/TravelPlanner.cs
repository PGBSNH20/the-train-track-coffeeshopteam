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
        /* travel planner */
        // AddTrain();  *    check that element 1 and 2 are the only ones being able to be used.  *
        // AddTrack();  *
        // StartAt();   *    Always write startAt() first                 *
        // ArriveAt();  *    Inserts Arrival data into the StartAt data, also throws exception if no StartAt data exists     *
        // GeneratePlan();   check for overlaps(train crash) and return a TravelPlan object
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

        //public TimeSpan ToTimeSpan(string time)
        //{
        //    TimeSpan timeSpan = TimeSpan.Parse(time);
        //    return timeSpan;
        //}

        public ITravelPlanner SelectTrain(int id)
        {
            // we check if the provided train id is in the list
            if (!Trains.Contains(Train[id]))
            {
                throw new Exception("Can´t find this train, please choose another train");
            }

            // we take our Train list, and we try to Find the train with the same trainID, if its is operated
            if (!Trains.Find(train => train.ID == id).IsOperated)
            {
                throw new Exception("This train is not running");
            }

            selectedTrainID = id;
            return this;
        }

        public ITravelPlanner AddTrain(Train train)
        {
            if (!train.IsOperated)
            {
                throw new Exception("This train is not running");
            }

            // We want to check if it contains the same train , and if not you are adding a new train. If the train is there already then its selectedTrainID
            //if (!Trains.Contains(train))
            if (!Trains.Any(t => t.ID == train.ID))
            {
                Trains.Add(train);
            }
            else
            {
                throw new Exception("A train with that ID already exists.");
            }

            selectedTrainID = train.ID;
            return this;
        }

        public ITravelPlanner AddTrack(TrackDescription trackDescription)
        {
            TrackDescription = trackDescription;
            return this;
        }

        public ITravelPlanner StartAt(int stationId, string time)
        {
            if(!Stations.Contains(stationId))

            {

            }
            TimeSpan parsedTime = TimeSpan.Parse(time);
            travelPlanDatas.Add(new TravelPlanData { TrainID = selectedTrainID, StartStationID = stationId, StartTime = parsedTime });

            return this;
        }

        public ITravelPlanner ArriveAt(int stationId, string time)
        {
            TimeSpan parsedTime = TimeSpan.Parse(time);
            // I want to find the last spot of the travelPlanDatas list to add to it, we are adding the arrive at data
            // TravelPlanData workingData   = travelPlanDatas[travelPlanDatas.Count - 1] 
            TravelPlanData workingData = travelPlanDatas[^1];
            workingData.ArriveStationID = stationId;
            workingData.ArriveTime = parsedTime;
            travelPlanDatas[^1] = workingData;

            return this;
        }

        private Station GetStationById(int stationId)
        {
            // find stationID int to the file stationid
            foreach (Station station in Stations)
            {
                if (station.ID == stationId)
                {
                    return station;
                }
            }
            throw new Exception("Can't find the station.");
        }

        // Later it should return a ITravelPlan not a ITravelPlanner
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
