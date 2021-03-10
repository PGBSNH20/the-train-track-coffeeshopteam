using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TrainEngine
{
    public class TrackOrm
    {
        // Metoder för att läsa in filer
        public List<Train> ReadTrainInfo()
        {

            string path = "trains.txt";
            List<Train> trains = new List<Train>();
            try
            {
                foreach (var item in File.ReadAllLines(path).Skip(1).Select(a => a.Split(',')))
                {
                    int trainID = int.Parse(item[0]);
                    string name = item[1];
                    int maxSpeed = int.Parse(item[2]);
                    bool isActive = bool.Parse(item[3]);

                    trains.Add(new Train(trainID, name, maxSpeed, isActive));
                }
            }
            catch
            {
                throw new Exception("Something went wrong with trains.txt");
            }
            return trains;
        }
        public TrackDescription ParseTrackDescription(string track)
        {
            throw new NotImplementedException();
        }
    }
}