using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Console.Const
{
    public class Rotate
    {
        public enum Destination
        {
            [Description("L")]
            Left = -1,
            [Description("R")]
            Right = 1,
            [Description("M")]
            Stable = 0
        }

    }
}
