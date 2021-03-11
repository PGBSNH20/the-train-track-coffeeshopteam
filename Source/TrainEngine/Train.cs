namespace TrainEngine
{
    public class Train
    { 
        public int ID { get; set; }
        public string Name { get; set; }
        public int MaxSpeed { get; set; }
        public bool IsActive { get; set; }

        public Train(int iD, string name, int maxSpeed, bool isActive)
        {
            ID = iD;
            Name = name;
            MaxSpeed = maxSpeed;
            IsActive = isActive;
        }
    }
}

        


