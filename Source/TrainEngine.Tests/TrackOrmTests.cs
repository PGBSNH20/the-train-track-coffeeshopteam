using System;
using System.Collections.Generic;
using Xunit;


namespace TrainEngine.Tests
{
    public class TrackOrmTests
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

        [Fact]
        public void Train_Fel_Format_Expect_Exception_Message()
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

        [Fact]
        public void ReadFile_Typical_stations_Expect_Count_Four()
        {
            var csvData = FileIO.GetDataFromFile("Data/stations_typical.txt", '|');
            var stations = FileIO.ParseStation(csvData);
            Assert.Equal(4, stations.Count);
        }

        [Fact]
        public void ReadFile_wrong_format_Expect_Count_Two()
        {
            var csvData = FileIO.GetDataFromFile("Data/stations_wrong_format.txt", '|');
            var stations = FileIO.ParseStation(csvData);
            Assert.Equal(2, stations.Count);
        }

        [Fact]
        public void FindStart_Expect_0_3()
        {
            TrackOrm trackOrm = new TrackOrm();
            var path = "Data/mock_track.txt";
            List<string> track = FileIO.GetDataFromFile(path);
            Coordinate c = trackOrm.FindStart(track);
            Assert.Equal(0, c.LinePosition);
            Assert.Equal(3, c.CharacterPosition);
        }

        [Fact]
        public void LoadTrack_expect_StationConnections_NotNull()
        {
            var path = "Data/mock_track.txt";
            TrackOrm trackOrm = new TrackOrm();
            TrackDescription track = trackOrm.LoadTrack(path);
            Assert.NotNull(track.StationConnections);
        }
    }
}

