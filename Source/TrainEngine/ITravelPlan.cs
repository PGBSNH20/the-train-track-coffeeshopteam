using System;
using System.Collections.Generic;
using System.Text;


namespace TrainEngine
{
    public interface ITravelPlan
    {
        public TrackDescription TrackDescription { get; }
        public List<Station> Stations { get; }
        public List<Train> Trains { get; }
        void Save();
        void Load();
    }
}
