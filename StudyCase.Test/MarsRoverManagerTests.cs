using StudyCase.Entities;
using StudyCase.Enums;
using StudyCase.Managers;
using System;
using Xunit;

namespace StudyCase.Test
{
    public class MarsRoverManagerTests
    {
        [Theory]
        [InlineData(5, 5, 1, 2, Rotation.N, "LMLMLMLMM", "1 3 N")]
        [InlineData(5, 5, 3, 3, Rotation.E, "MMRMMRMRRM", "5 1 E")]
        public void ExecuteCommand_ShouldBeEquals_GetExpectedResult(int width, int height, int pointX, int pointY, Rotation rotation, string command, string expectedResult)
        {
            var plateau = new Plateau(width, height);
            var location = new Location(pointX, pointY);
            var _rover = new MarsRover();
            _rover.SetPlateau(plateau);
            _rover.SetLocation(location, rotation);
            var _roverManager = new MarsRoverManager(_rover);
            _roverManager.ExecuteCommand(command);
            Assert.Equal(expectedResult, _roverManager.GetStatusText());
        } 
    }
}
