using MarsRover.Console.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Console.Abstract
{
   public  interface IRoverOperation
    {
        /// <summary>
        /// Accuracy of motion data
        /// Two pieces int and one pieces char
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        ReturnDataResult IsPositionValuesCorrect(List<string> values);


        /// <summary>
        /// Entered character check. To check numbers or text
        /// Used in state information comparison
        /// </summary>
        /// <param name="charValue"></param>
        /// <param name="controlStatus"></param>
        /// <returns></returns>
        bool IsCharacterCorrect(char charValue, bool controlStatus);


        /// <summary>
        /// Transaction signature for the transaction
        /// </summary>
        Rover Motion(Rover rover, List<int> maxBorderValues, string orientationValues);


        /// <summary>
        /// Check that the movement is within the maximum limit
        /// </summary>
        /// <param name="maxBorderValues"></param>
        /// <returns></returns>
        ReturnDataResult GridPointControl(List<int> maxBorderValues);
    }
}
