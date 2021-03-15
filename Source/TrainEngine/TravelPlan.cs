using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace TrainEngine
{
    class TravelPlan : ITravelPlan
    {
        public List<TravelPlanData> TravelPlanDatas { get; set; }
        /* travel plan */
        // tain track
        // List of stations
        // List of trains
        // variable(s): which train starts where/when and arrives where/when
        // simulate() the trains driving (incl. opening/closing of level crossings)
        // load()   *
        // save()   *

        public void Save()
        {
            string jsonString = JsonSerializer.Serialize(this);
            File.WriteAllText("Data/TravelPlan.txt", jsonString);
        }

        public void Load()
        {
            string jsonString = File.ReadAllText("Data/TravelPlan.txt");
            TravelPlan travelPlan = JsonSerializer.Deserialize<TravelPlan>(jsonString);

            TravelPlanDatas = travelPlan.TravelPlanDatas;
        }
    }
}
