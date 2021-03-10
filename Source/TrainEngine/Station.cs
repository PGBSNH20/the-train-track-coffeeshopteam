using System.Collections.Generic;

namespace TrainEngine
{
    public class Station
    {
        public int ID { get; set; }
        public string StationName { get; set; }
        public bool EndStation { get; set; }

        public List<Station> Stations { get; set; }

    }
}

        


