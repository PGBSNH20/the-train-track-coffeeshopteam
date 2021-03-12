using System;
using System.Collections.Generic;
using Xunit;


namespace TrainEngine.Tests
{
    public class TrackOrmTests
    {
        //[Fact]
        //public void When_OnlyAStationIsProvided_Expect_TheResultOnlyToContainAStationWithId1()
        //{
        //    // Arrange
        //    string track = "[1]";
        //    TrackOrm trackOrm = new TrackOrm();

        //    // Act
        //    var result = trackOrm.ParseTrackDescription(track);

        //    // Assert
        //    //Assert.IsType<Station>(result.TackPart[0]);
        //    //Station s = (Station)result.TackPart[0];
        //    //Assert.Equal(1, s.Id);
        //}

        //[Fact]
        //public void When_ProvidingTwoStationsWithOneTrackBetween_Expect_TheTrackToConcistOf3Parts()
        //{
        //    // Arrange
        //    string track = "[1]-[2]";
        //    TrackOrm trackOrm = new TrackOrm();
            
        //    // Act
        //    var result = trackOrm.ParseTrackDescription(track);

        //    // Assert
        //    Assert.Equal(3, result.NumberOfTrackParts);
        //}

        [Fact]
        public void Train_Typical_Expect_Success()
        {
            List<Train> trains = FileIO.ReadTrainInfo("Data/trains_typical.txt");
            Train[] trainsArray = trains.ToArray();

            Assert.Equal(1, trainsArray[0].ID);
            Assert.Equal("Flying Scotsman", trainsArray[0].Name);
            Assert.Equal(100, trainsArray[0].MaxSpeed);
            Assert.False(trainsArray[0].IsActive);
            Assert.Equal(2, trainsArray[1].ID);
            Assert.Equal("Golden Arrow", trainsArray[1].Name);
            Assert.Equal(120, trainsArray[1].MaxSpeed);
            Assert.True(trainsArray[1].IsActive);
        }

        [Fact]
        public void Train_Fel_Format_Expect_Exseption_Message()
        {
            Action action = () => FileIO.ReadTrainInfo("Data/trains_wrong_format.txt");
            Exception exception = Assert.Throws<Exception>(action);
            Assert.Equal("Something went wrong with trains.txt", exception.Message);
        }

        [Fact]
        public void Train_Empty_File_Expect_Empty_List()
        {
            List<Train> trains = FileIO.ReadTrainInfo("Data/trains_empty.txt");
            Assert.Empty(trains);
        }
        [Fact]
        public void Train_No_File_Expect_Empty_List()
        {
            List<Train> trains = FileIO.ReadTrainInfo("Data/trains_.txt");
            Assert.Empty(trains);
        }
    }
}
