using System;
using System.Collections.Generic;
using Xunit;



namespace TrainEngine.Tests
{
    public class TravelPlannerTests
    {


        [Fact]
        public void GeneratePlan_expect_exception()
        {
            Action action = () => FileIO.ReadTrainInfo("Data/trains_wrong_format.txt");
            Exception exception = Assert.Throws<Exception>(action);
            Assert.Equal("Something went wrong with trains.txt", exception.Message);
        }
    }
}
