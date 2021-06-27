using StudyCase.Entities;
using StudyCase.Enums;
using StudyCase.Exceptions;
using StudyCase.Managers;
using System;
using Xunit;

namespace StudyCase.Test
{
    public class MarsRoverExceptionTests
    {
         
        [Theory]
        [InlineData(5, 5, 3, 3, Rotation.E, "MMRMMRMRRMMMMMM")]
        public void ExecuteCommand_ShouldBeException_GetOutOfBorderException(int width, int height, int pointX, int pointY, Rotation rotation, string command)
        {
            var plateau = new Plateau(width, height);
            var location = new Location(pointX, pointY);
            var rover = new MarsRover();
            rover.SetPlateau(plateau);
            rover.SetLocation(location, rotation);
            var roverManager = new MarsRoverManager(rover);
            Assert.Throws<OutOfBorderException>(() => roverManager.ExecuteCommand(command));
        }


        [Theory]
        [InlineData(5, 5, 3, 3, Rotation.E, "MMRMMRMRRMA")]
        public void ExecuteCommand_ShouldBeException_GetInvalidCommandException(int width, int height, int pointX, int pointY, Rotation rotation, string command)
        {
            var plateau = new Plateau(width, height);
            var location = new Location(pointX, pointY);
            var rover = new MarsRover();
            rover.SetPlateau(plateau);
            rover.SetLocation(location, rotation);
            var roverManager = new MarsRoverManager(rover);
            Assert.Throws<InvalidCommandException>(() => roverManager.ExecuteCommand(command));
        }
    }
}
