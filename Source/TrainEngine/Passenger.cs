using System;
using System.Collections.Generic;


namespace TrainEngine
{
    class Passenger
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public List<Passenger> Passengers { get; set; }


        //public Passenger(int id, string name)
        //{
        //    this.ID = id;
        //    this.Name = name;
        //}
    }
}
