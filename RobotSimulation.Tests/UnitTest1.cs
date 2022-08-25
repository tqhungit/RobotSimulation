using Microsoft.Extensions.DependencyInjection;
using RobotSimulation.Models;

namespace RobotSimulation.Tests
{
    public class Tests
    {
        private RobotOperation _robotOperation;

        [SetUp]
        public void Setup()
        {
            var host = RobotSimulation.Configuration.HostBuilder.CreateHostBuilder(new string[] { }).Build();

            _robotOperation = host.Services.GetService<RobotOperation>();
        }

        [Test]
        public void RebotOperation_Initial_Valid()
        {
            var host = RobotSimulation.Configuration.HostBuilder.CreateHostBuilder(new string[] { }).Build();

            var robotOperation = host.Services.GetService<RobotOperation>();

            Assert.IsNotNull(robotOperation);
        }


        [Test]
        public void RebotOperation_InvalidCommand()
        {
            var response = _robotOperation.DoCommand("TEST");

            Assert.AreEqual(response, "Invalid command.");
        }

        [Test]
        public void RebotOperation_PlaceWithNoArgs()
        {
            var response = _robotOperation.DoCommand("PLACE");

            Assert.AreEqual(response, "Invalid command.");
        }

        [Test]
        public void RebotOperation_PlaceWithInValidArgs_SpaceOnly()
        {
            var response = _robotOperation.DoCommand("PLACE  ");

            Assert.AreEqual(response, "Invalid command.");
        }

        [Test]
        public void RebotOperation_PlaceWithInValidArgs_InvalidPosition_Character()
        {
            var response = _robotOperation.DoCommand("PLACE  a,b");

            Assert.AreEqual(response, "Invalid command.");
        }

        [Test]
        public void RebotOperation_PlaceWithInValidArgs_ValidPosition_MissingFacing()
        {
            var response = _robotOperation.DoCommand("PLACE  1,1");

            Assert.AreEqual(response, "Invalid command.");
        }


        [Test]
        public void RebotOperation_PlaceWithInValidArgs_ValidPosition_InvalidFacing()
        {
            var response = _robotOperation.DoCommand("PLACE  1,1,X");

            Assert.AreEqual(response, "Invalid command.");
        }

        [Test]
        public void RebotOperation_PlaceWithInValidArgs_ValidPosition_ValidFacing_ExtraArg()
        {
            var response = _robotOperation.DoCommand("PLACE  1,1,X,X");

            Assert.AreEqual(response, "Invalid command.");
        }


        [Test]
        public void RebotOperation_PlaceWithInValidArgs_InvalidPosition_OutOfTable()
        {
            var response = _robotOperation.DoCommand("PLACE  5,5,NORTH");
            Assert.AreEqual(response, "Invalid command.");
            response = _robotOperation.DoCommand("PLACE  -1,5,NORTH");
            Assert.AreEqual(response, "Invalid command.");
        }


        [Test]
        public void RebotOperation_PlaceValid()
        {
            var response = _robotOperation.DoCommand("PLACE 0,0,EAST");
            Assert.AreEqual(response, "Place Done.");
        }


        [Test]
        public void RebotOperation_PlaceAndMove_ReportCorrect()
        {
            var response = _robotOperation.DoCommand("PLACE 0,0,NORTH");
            _robotOperation.DoCommand("MOVE");
            response = _robotOperation.DoCommand("REPORT");
            Assert.AreEqual(response, "0,1,NORTH");
        }

        [Test]
        public void RebotOperation_PlaceAndTurnLeft_ReportCorrect()
        {
            var response = _robotOperation.DoCommand("PLACE 0,0,NORTH");
            _robotOperation.DoCommand("LEFT");
            response = _robotOperation.DoCommand("REPORT");
            Assert.AreEqual(response, "0,0,WEST");
        }


        [Test]
        public void RebotOperation_PlaceValid_MoveOutOffTable_ReportInvalid()
        {
            var response = _robotOperation.DoCommand("PLACE 4,4,EAST");
            response = _robotOperation.DoCommand("MOVE");
            Assert.AreEqual(response, "Robot cannot execute the command - out of table.");
        }


        [Test]
        public void RebotOperation_NoPlaceButMove_ReportInvalid()
        {
            var response = _robotOperation.DoCommand("MOVE");
            Assert.AreEqual(response, "Robot cannot execute the command until it has been placed.");
        }


        [Test]
        public void Robot_PlaceMovedAndTurnAround_ReportCorrect()
        {
            var response = _robotOperation.DoCommand("PLACE 1,2,NORTH");
            _robotOperation.DoCommand("MOVE");
            _robotOperation.DoCommand("MOVE");
            _robotOperation.DoCommand("RIGHT");
            _robotOperation.DoCommand("MOVE");
            response = _robotOperation.DoCommand("REPORT");
            Assert.AreEqual("2,4,EAST", response);
        }



    }
}