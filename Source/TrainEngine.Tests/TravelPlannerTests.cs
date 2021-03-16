using System;
using System.Collections.Generic;
using Xunit;



namespace TrainEngine.Tests
{
    public class TravelPlannerTests
    {
        [Fact]
        public void StartAt_InvalidTime_Expect_Exception_Message()
        {
            TravelPlanner travelPlanner = new TravelPlanner();
            Action action2 = () => travelPlanner.StartAt(2, "hh");
            Exception exception = Assert.Throws<Exception>(action2);
            Assert.Equal("Invalid time format", exception.Message);
        }

        [Fact]
        public void GeneratePlan_expect_exception()
        {
            Action action = () => FileIO.ReadTrainInfo("Data/trains_wrong_format.txt");
            Exception exception = Assert.Throws<Exception>(action);
            Assert.Equal("Something went wrong with trains.txt", exception.Message);
        }
    }
}
