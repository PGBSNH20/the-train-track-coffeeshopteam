using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace TrainEngine
{
    public class TravelPlan : ITravelPlan
    {

        private List<TravelPlanData> travelPlanDatas;
        public TrackDescription TrackDescription { get; set; }
        public List<Station> Stations { get; set; }
        public List<Train> Trains { get; set; }
        private Thread simulationThread;


        // variable(s): which train starts where/when and arrives where/when
        // simulate()   the trains driving (incl. opening/closing of level crossings), give each train a thread/access to the clock, so it runs by itself

        public TravelPlan(List<TravelPlanData> travelPlanDatas, TrackDescription trackDescription)
        {
            this.travelPlanDatas = travelPlanDatas;
            TrackDescription = trackDescription;
            Stations = FileIO.LoadStations();
            Trains = FileIO.ReadTrainInfo("Data/trains.txt");

            simulationThread = new Thread(Simulate);
            simulationThread.Start();
        }

        public void Simulate()
        {
            // create thread for each train?! that drives the train
            foreach(var data in travelPlanDatas)
            {
                //public int TrainID { get; set; }
                //public int StartStationID { get; set; }
                //public int ArriveStationID { get; set; }
                //public TimeSpan StartTime { get; set; }
                //public TimeSpan ArriveTime { get; set; }
                Console.WriteLine($"Origin: {data.StartStationID}, Destination: {data.ArriveStationID}, Start: {data.StartTime}, Stop: {data.ArriveTime}, Train: {data.TrainID}");
            }
            while (true)
            {
                TimeSpan clockTime = Clock.Time;
                string timeString = clockTime.ToString(@"hh\:mm", CultureInfo.InvariantCulture);
                foreach (var data in travelPlanDatas)
                {
                    if (data.StartTime == clockTime)
                    {
                        string trainName = Trains.Find(train => train.ID == data.TrainID).Name;
                        string stationName = Stations.Find(station => station.ID == data.StartStationID).StationName;
                        Console.WriteLine($"[{timeString}]: {trainName} is departing from {stationName}.");
                    }
                    if (data.ArriveTime == clockTime)
                    {
                        string trainName = Trains.Find(train => train.ID == data.TrainID).Name;
                        string stationName = Stations.Find(station => station.ID == data.StartStationID).StationName;
                        Console.WriteLine($"[{timeString}]: {trainName} is arriving at {stationName}.");
                    }
                }
                if (timeString == "23:59")
                {
                    break;
                }
                Thread.Sleep(5);
            }
        }

        public void Save()
        {
            string jsonString = JsonSerializer.Serialize(this);
            File.WriteAllText("Data/TravelPlan.txt", jsonString);
        }

        public void Load()
        {
            string jsonString = File.ReadAllText("Data/TravelPlan.txt");
            TravelPlan travelPlan = JsonSerializer.Deserialize<TravelPlan>(jsonString);

            travelPlanDatas = travelPlan.travelPlanDatas;
        }

    }
}
