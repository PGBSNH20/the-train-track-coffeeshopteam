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

        public int NumberOfTrackParts { get; set; }

        // * = START             looks like its for our parsing file
        // [number] = station
        // - or / or \ = track
        // = = level crossing
        // < or > = switch
        // If you need the notion of length, assume that each track ( - ) in the trackfile is eg 10 km and the takes the train 1 min drive that distance.

        // List<TrainSwitch> trainSwitches { get; set; }
        // Dictionary<int, List<int>> StationConnections { get; set;}

        // int => actual station id, List<int> => ids of connected stations
        //List<int> StationIDs { get; set; }

        // list<int> is taking into consideration that 1 station might be connected to several stations
        // <inital stationId, List of connected stationIds>
        //Dictionary<int, List<int>> StationConnections { get; set; }

        // <(station1, station2), distance> 
        //Dictionary<(int, int), int> StationDistances { get; set; }
    }
}