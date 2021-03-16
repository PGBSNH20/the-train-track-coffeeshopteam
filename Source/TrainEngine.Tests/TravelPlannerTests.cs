using System;
using System.Collections.Generic;
using Xunit;



namespace TrainEngine.Tests
{
    public class TravelPlannerTests
    {

        [Fact]
        public void SelectTrain_TrainNotInList_Expect_Exception_Message()
        {
            TravelPlanner travelPlanner = new TravelPlanner();
            Action action = () => travelPlanner.SelectTrain(7);
            Exception exception = Assert.Throws<Exception>(action);
            Assert.Equal("Can´t find this train, please choose another train", exception.Message);
        }

        [Fact]
        public void SelectTrain_NotOperatedTrain_Expect_Exception_Message()
        {
            TravelPlanner travelPlanner = new TravelPlanner();
            Action action = () => travelPlanner.SelectTrain(1);
            Exception exception2 = Assert.Throws<Exception>(action);
            Assert.Equal("This train is not running", exception2.Message);
        }

        [Fact]
        public void StartAt_StationNotInList_Expect_Exception_Message()
        {
            TravelPlanner travelPlanner = new TravelPlanner();
            Action action = () => travelPlanner.StartAt(11, "9");
            Exception exception = Assert.Throws<Exception>(action);
            Assert.Equal("Can´t find this station, please choose another station", exception.Message);
        }

        [Fact]
        public void StartAt_InvalidTimeFormat_Expect_Exception_Message()
        {
            TravelPlanner travelPlanner7 = new TravelPlanner();
            Action action2 = () => travelPlanner7.StartAt(2, "hh");
            Exception exception4 = Assert.Throws<Exception>(action2);
            Assert.Equal("Invalid time format", exception4.Message);
        }

        [Fact]
        public void ArriveAt_StationNotInList_Expect_Exception_Message()
        {
            TravelPlanner travelPlanner = new TravelPlanner();
            Action action = () => travelPlanner.StartAt(8, "9");
            Exception exception = Assert.Throws<Exception>(action);
            Assert.Equal("Can´t find this station, please choose another station", exception.Message);
        }

        [Fact]
        public void ArriveAt_InvalidTimeFormat_Expect_Exception_Message()
        {
            TravelPlanner travelPlanner8 = new TravelPlanner();
            Action action3 = () => travelPlanner8.ArriveAt(2, "jj");
            Exception exception = Assert.Throws<Exception>(action3);
            Assert.Equal("Invalid time format", exception.Message);
        }

    }




}

