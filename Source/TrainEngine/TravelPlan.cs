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

        public void Load()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
        /* travel plan */
        // tain track
        // List of stations
        // List of trains
        // variable(s): which train starts where/when and arrives where/when
        // simulate() the trains driving (incl. opening/closing of level crossings)
        // load()   *
        // save()   *


    }
}
