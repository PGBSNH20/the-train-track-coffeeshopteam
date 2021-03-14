using System.Collections.Generic;

namespace TrainEngine
{
    public class StationConnection
    {
        public int Distance { get; set; }
        public int StationID { get; set; }
        public int StationIDDestination { get; set; }
        public List<char> TrackParts { get; set; }

        public StationConnection(int stationID, int stationIDDestination, int distance, List<char> trackParts)
        {
            Distance = distance;
            StationID = stationID;
            StationIDDestination = stationIDDestination;
            TrackParts = trackParts;
        }

        public override string ToString()
        {
            return $"Station1: {StationID}, Station2: {StationIDDestination}, Distance: {Distance}, TrackPartsCount: {TrackParts.Count}";
        }
    }

    public class TrackDescription
    {
        public List<StationConnection> StationConnections { get; set; } = new List<StationConnection>();
        public bool LevelCrossingIsOpen { get; private set; }
        public int NumberOfTrackParts { get; set; }

        // Here we can also find the distance to the levelcrossing by putting the trackparts through a foreach until we find the '='
        // if we need it for opening timing, for example using an int "crossingPosition += 10"

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