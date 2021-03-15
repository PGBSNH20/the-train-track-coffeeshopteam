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
        private int trainID; 
        public TrackDescription TrackDescription { get; set; }
        private int startStationId;
        private int arriveStationId;
        private TimeSpan startTime;
        private TimeSpan arriveTime;

        public List<Station> stations;
        //public List<Train> Trains = new List<Train>();
        private List<TravelPlanData> travelPlanDatas;
        
        
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
            if (!train.IsOperated)
            {
                throw new Exception("This train is not running"); // FÅr exception här
            }

            //// Only want to add the train once to the list
            //if (!Trains.Contains(train))
            //{
            //    Trains.Add(train);
            //}
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
            //foreach (StationConnection sc in TrackDescription.StationConnections)
            //{
            //    if (sc.StationID == startStationId)
            //    {
            //        stationConnectionOrder.Add(sc);
            //    }
            //}

            startTime = TimeSpan.Parse(time);
            //bool dataExists = false;

            //for (int i = 0; i < travelPlanDatas.Count; i++)
            //{
            //    TravelPlanData travelPlanData = travelPlanDatas[i];
            //    if (travelPlanData.TrainID == trainID &&
            //        travelPlanData.ArriveStationID == stationId &&
            //        travelPlanData.ArriveTime == startTime)
            //    {
            //        dataExists = true;
            //        travelPlanData.StartStationID = stationId;
            //        travelPlanData.StartTime = startTime;
            //        break;
            //    }
            //}

            //travelPlanDatas.Add(new TravelPlanData { TrainID = trainID, StartStationID = stationId, StartTime = startTime });

            return this;
        }

        public ITravelPlanner ArriveAt(int stationId, string time)
        {
            arriveStationId = stationId;
            arriveTime = TimeSpan.Parse(time);
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
        public TravelPlan GeneratePlan()
        {
            // Skapa en TravelPlan
            TravelPlan travelPlan = new TravelPlan
            {
                TravelPlanDatas = 
                    new List<TravelPlanData> 
                    { 
                        new TravelPlanData
                        { 
                            TrainID = trainID, 
                            StartStationID = startStationId, 
                            StartTime = startTime, 
                            ArriveStationID = arriveStationId, 
                            ArriveTime = arriveTime
                        } 
                    }
            };

            // Använd Save() för att spara planen. 
            Save(travelPlan); 
            
            //Trains.ForEach(train => Console.WriteLine("Train name: " + train.Name));
            return travelPlan;
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

        public void Save(TravelPlan travelPlan)
        {
            string jsonString = JsonSerializer.Serialize(travelPlan);
            File.WriteAllText("Data/TravelPlan.json", jsonString);
        }

        public void Load()
        {
            string jsonString = File.ReadAllText("Data/TravelPlan.txt");
            TravelPlan travelPlan = JsonSerializer.Deserialize<TravelPlan>(jsonString);

            //TravelPlanDatas = travelPlan.TravelPlanDatas;
        }
    }
}
