using System;
using System.Collections.Generic;
using System.Text;

namespace TrainEngine
{
    public class TrainSimulator
    {
        private readonly Clock clock;

        public TrainSimulator(Clock clock)
        {
            this.clock = clock;
        }

        public void RunSimulation(TravelPlan travelPlan)
        {
            // Starta klockan
            // Tåget rullar från Startstation mot stationconnection med startstation[startstationid].stationiddestination klockan starttime
            // Kolla om StationConnection med stationID[startstationID] har några trackparts 
            // Om ja, stäng bommar på crossing
            // CW "stänger bommar så tåget kan passera"
            // Räkna ut distance och arrival och sånt
        }
    }
}
