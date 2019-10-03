using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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

        #region//İşlem adımları
        public static void StepOne()
        {
            System.Console.WriteLine();
            System.Console.WriteLine($"Max position degerlerini girin X-Y");
            var maxDigits = GetMaxPositionRead();
            if (maxDigits.Count == 2)
            {  _positionMaxValues = maxDigits; StepTwo();}
            StepOne();
        }
        private static void StepTwo()
        {
            System.Console.WriteLine();
            System.Console.WriteLine($"Lütfen hareket değerlerini girin X-Y-Z");
            var stepValues = GetRetrieveEnteredData();
            if (stepValues.Count == 3)
            {    _positionValues = stepValues; StepThree(stepValues);}
            StepTwo();
        }
        private static void StepThree(List<string> temporyPositionValues)
        {
            var positionRetunValu = RoverOperation.IsPositionValuesCorrect(temporyPositionValues);
            if (positionRetunValu.IsCorrect)
            {
                // yön için pozition kontrolü yapıcaz enum
                try
                {
                    var rotateValue = EnumOperation.GetEnumFromDescription<Compass.Direction>(_positionValues[2]);
                    Rover.RoverDirection = rotateValue;
                    Rover.PositionX = Convert.ToInt32(_positionValues[0]);
                    Rover.PositionY = Convert.ToInt32(_positionValues[1]);
                    StepFour();
                }
                catch (InvalidOperationException)
                {
                    System.Console.WriteLine();
                    System.Console.WriteLine($"Girilen yön hatalı !  {_positionValues[2].ToString() } tipinde bir yön bulunmamaktadır. ");
                    StepTwo();
                }
            }
            else
            {
                System.Console.WriteLine();
                System.Console.WriteLine(positionRetunValu.ReturnMessage);
                StepTwo();
            }
        }
        private static void StepFour()
        {
            // hareket bilgisi girin
            System.Console.WriteLine();
            System.Console.WriteLine($"Lütfen hareket bilgisi girin  (L,R,M) kombinayyonları ile boşluksuz yazınız.");
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
                    System.Console.WriteLine($"Rover is last Position X-Y-Z :" +
                                             $"{lastRoverValues.PositionX.ToString()}-" +
                                             $"{lastRoverValues.PositionY.ToString()}-" +
                                             $"{EnumOperation.GetEnumDescription<Direction>(lastRoverValues.RoverDirection)}");
                    //  işlem basarı ile bitince soruyoruz?
                    IsStepChoiceAnswer();
                }
                //StepTwo();
            }
            catch (Exception)
            {
                System.Console.WriteLine();
                System.Console.WriteLine($"Hatalı giriş ");
                StepFour();
            }
        }
        #endregion


        public static void IsStepChoiceAnswer()
        {
            System.Console.WriteLine($"Max boyutu değiştirmek için 1, yeni konum bilgisi girmek için 2 ye çıkmak için 0 ı tuşlayınız :)");
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
