using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace TrainEngine
{
    public class TrackOrm
    {
        // Metoder för att läsa in filer
        public List<Station> ReadStation()
        {
            var path = "Data/stations.txt";
            var list = new List<Station>();
            if (File.Exists(path))
            {
                // Läs in från fil
                var loadItems = File.ReadAllLines(path);

                // Lägg till filens innehåll i cartList
                foreach (string line in loadItems.Skip(1))
                {
                    var columns = line.Split('|');
                    Station s = new Station
                    {
                        ID = int.Parse(columns[0]),
                        StationName = columns[1],
                        EndStation = bool.Parse(columns[2])
                    };

                    list.Add(s);
                }
            }
            else
            {
                throw new Exception("Station information not available");
            }

            return list;
        }
      
        public TrackDescription ParseTrackDescription(string track)
        {
            throw new NotImplementedException();
        }
    }
}