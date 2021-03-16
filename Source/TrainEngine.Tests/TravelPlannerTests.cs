using System;
using System.Collections.Generic;
using Xunit;



namespace TrainEngine.Tests
{
    public class TravelPlannerTests
    {


        [Fact]
        public void Train_Typical_Expect_Success()
        {
            List<Train> trains = FileIO.ReadTrainInfo("Data/trains_typical.txt");
            Train[] trainsArray = trains.ToArray();

            Assert.Equal(1, trainsArray[0].ID);
            Assert.Equal("Flying Scotsman", trainsArray[0].Name);
            Assert.Equal(100, trainsArray[0].MaxSpeed);
            Assert.False(trainsArray[0].IsOperated);
            Assert.Equal(2, trainsArray[1].ID);
            Assert.Equal("Golden Arrow", trainsArray[1].Name);
            Assert.Equal(120, trainsArray[1].MaxSpeed);
            Assert.True(trainsArray[1].IsOperated);
        }
    }
}
