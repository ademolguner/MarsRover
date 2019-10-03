using System;
using System.Collections.Generic;
using System.Linq;
using MarsRover.Console.Const;
using MarsRover.Console.Entities;
using MarsRover.Console.Manager;
using static MarsRover.Console.Const.Compass;

namespace MarsRover.Console
{
    public class Program
    {

        static readonly RoverOperation RoverOperation = new RoverOperation();
        static readonly EnumOperation EnumOperation = new EnumOperation();
        static readonly Rover Rover = new Rover();
        static List<int> _positionMaxValues = new List<int>();
        static List<string> _positionValues = new List<string>();


        static void Main(string[] args)
        {
            StepOne();
        }

        #region Process steps
        public static void StepOne()
        {
            System.Console.WriteLine();
            System.Console.WriteLine($"Enter MAX position X-Y");
            var maxDigits = GetMaxPositionRead();
            if (maxDigits.Count == 2)
            {  _positionMaxValues = maxDigits; StepTwo();}
            StepOne();
        }

        public static void StepTwo()
        {
            System.Console.WriteLine();
            System.Console.WriteLine($"Please enter transaction values X-Y-Z");
            var stepValues = GetRetrieveEnteredData();
            if (stepValues.Count == 3)
            {    _positionValues = stepValues; StepThree(stepValues);}
            StepTwo();
        }
        private static void StepThree(List<string> temporaryPositionValues)
        {
            var positionReturnValue = RoverOperation.IsPositionValuesCorrect(temporaryPositionValues);
            if (positionReturnValue.IsCorrect)
            {
                try
                {
                    //Direction for position control
                    var rotateValue = EnumOperation.GetEnumFromDescription<Compass.Direction>(_positionValues[2]);
                    Rover.RoverDirection = rotateValue;
                    Rover.PositionX = Convert.ToInt32(_positionValues[0]);
                    Rover.PositionY = Convert.ToInt32(_positionValues[1]);
                    StepFour();
                }
                catch (InvalidOperationException)
                {
                    System.Console.WriteLine();
                    System.Console.WriteLine($"Incorrect direction entered ! There is no {_positionValues[2].ToString() } type direction. ");
                    StepTwo();
                }
            }
            else
            {
                System.Console.WriteLine();
                System.Console.WriteLine(positionReturnValue.ReturnMessage);
                StepTwo();
            }
        }
        private static void StepFour()
        {
            System.Console.WriteLine();
            System.Console.WriteLine($"Please enter transaction values,  Write without spaces with (L,R,M) combinations.");
            var orientationValues = System.Console.ReadLine()?.Trim(' ').ToUpper();

            try
            {
                Rover lastRoverValues = RoverOperation.Motion(Rover, _positionMaxValues, orientationValues);
                if (lastRoverValues == null)
                {
                    IsStepChoiceAnswer();
                    // StepTwo();
                }
                else
                {
                    System.Console.WriteLine();
                    System.Console.WriteLine($"Rover's last position X-Y-Z :" +
                                             $"{lastRoverValues.PositionX.ToString()}-" +
                                             $"{lastRoverValues.PositionY.ToString()}-" +
                                             $"{EnumOperation.GetEnumDescription<Direction>(lastRoverValues.RoverDirection)}");
                    IsStepChoiceAnswer();
                }
            }
            catch (Exception)
            {
                System.Console.WriteLine();
                System.Console.WriteLine($"Incorrect entry ! ");
                StepFour();
            }
        }
        #endregion


        public static void IsStepChoiceAnswer()
        {
            System.Console.WriteLine($"For Max to change size 1, for new location information 2 ye for exit 0  press :)");
            dynamic choice = System.Console.ReadLine()?.Trim(' ').ToUpper();
            if (RoverOperation.IsCharacterCorrect(Convert.ToChar(choice), true))
            {
                choice = Convert.ToInt32(choice);
                switch (choice)
                {
                    case 0: Environment.Exit(0); break;
                    case 1: StepOne(); break;
                    case 2: StepTwo(); break;
                    default: IsStepChoiceAnswer(); ; break;
                }
            }


        }


        #region//Yardımcı methodlar
        private static List<int> GetMaxPositionRead()
        {
            List<int> values = new List<int>();
            var readValue = GetRetrieveEnteredData();
            if (readValue != null && readValue.Count == 2)
                foreach (var readKey in readValue)
                {
                    if (RoverOperation.IsCharacterCorrect(Convert.ToChar(readKey), true))
                    {
                        values.Add(Convert.ToInt32(readKey));
                    }
                    else
                    {
                        System.Console.WriteLine($"X ve Y değerleri sayı olmalıdır");
                        values = new List<int>();
                    }
                }
            else
            {
                System.Console.WriteLine($"Maximum pozisyon değeri X-Y olarak girilmelidir");
                values = new List<int>();
            }
            return values;
        }
        private static List<string> GetRetrieveEnteredData()
        {
            return System.Console.ReadLine()?.ToUpper().Trim().Split(' ').ToList();
        }
        #endregion
    }
}
