using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Console.Const
{
    public class Compass
    {
        public enum Direction
        {
            [Description("N")]
            North = 1,
            [Description("E")]
            East = 2,
            [Description("S")]
            South = 3,
            [Description("W")]
            West = 4
        }
    }
}
