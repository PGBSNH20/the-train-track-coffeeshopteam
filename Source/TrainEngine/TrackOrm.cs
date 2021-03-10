using System;
using System.IO;
using System.Collections.Generic;

namespace TrainEngine
{
    public class TrackOrm
    {
        public static List <Passenger> Passengers = new List<Passenger>();
        public const string passengersInfoPath = @"DataFiles/passengers.txt";
        
        public static void PassengerOrm ()
        {
            string [] lines = File.ReadAllLines(passengersInfoPath);
            foreach (string line in lines)
            {
                string [] parts = line.Split(';');
                Passenger p = new Passenger
                {
                    Name = parts[0],
                    Name2 = parts[1],
                };
                Passengers.Add(p);

            }
        
        }
      








        public TrackDescription ParseTrackDescription(string track)
        {
            throw new NotImplementedException();
        }
    }
}