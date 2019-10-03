using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsRover.Console.Const;
using static MarsRover.Console.Const.Compass;

namespace MarsRover.Console.Entities
{
    public class Rover
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public Direction RoverDirection { get; set; }

        /// <summary>
        /// We assign default values ​​to properties in constructor
        /// </summary>
        public Rover()
        {
            PositionX = 0;
            PositionY = 0;
            RoverDirection = Direction.North;
        }
    }
}
