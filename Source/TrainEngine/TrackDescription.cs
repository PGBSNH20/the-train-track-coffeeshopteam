using System.Collections.Generic;

namespace TrainEngine
{
    public class StationConnection
    {
        public int Distance { get; set; }
        public int StartStationID { get; set; }
        public int ArriveStationID { get; set; }
        public List<char> TrackParts { get; set; }

        public StationConnection(int stationID, int stationIDDestination, int distance, List<char> trackParts)
        {
            Distance = distance;
            StartStationID = stationID;
            ArriveStationID = stationIDDestination;
            TrackParts = trackParts;
        }

        public override string ToString()
        {
            return $"Station1: {StartStationID}, Station2: {ArriveStationID}, Distance: {Distance}, TrackPartsCount: {TrackParts.Count}";
        }
    }

    public class TrackDescription
    {
        public List<StationConnection> StationConnections { get; set; } = new List<StationConnection>();
        public bool LevelCrossingIsOpen { get; private set; }
        public int NumberOfTrackParts { get; set; }

        public bool HasLevelCrossing()
        {
            foreach (var stationConnection in StationConnections)
            {
                if (stationConnection.TrackParts.Contains('='))
                {
                    return true;
                }
            }
            return false;
        }

        public void OpenLevelCrossing()
        {
            if (HasLevelCrossing())
            {
                LevelCrossingIsOpen = true;
            }
        }

        public void CloseLevelCrossing()
        {
            if (HasLevelCrossing())
            {
                LevelCrossingIsOpen = false;
            }
        }
    }
}