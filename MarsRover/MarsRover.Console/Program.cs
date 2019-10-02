using System;
using System.Collections.Generic;
using System.Linq;
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
        static Rover _rover = new Rover();
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
                _positionMaxValues = maxDigits; StepTwo();
            StepOne();
        }
        private static void StepTwo()
        {
            System.Console.WriteLine();
            System.Console.WriteLine($"Lütfen hareket değerlerini girin X-Y-Z");
            var stepValues = GetRetrieveEnteredData();
            if (stepValues.Count == 3)
                _positionValues = stepValues; StepThree();
            StepTwo();
        }
        private static void StepThree()
        {
            var positionRetunValu = RoverOperation.IstPositionValuesCorrect(_positionValues);
            if (positionRetunValu.IsCorrect)
            {
                // sayı kontrolü yapıldığına göre diziyi parcalayarak x-y-ve yön olarak  değerleri atayabiliriz.
                _rover.PositionX = Convert.ToInt32(_positionValues[0]);
                _rover.PositionY = Convert.ToInt32(_positionValues[1]);
                // yön için pozition kontrolü yapıcaz enum
                try
                {
                    var rotateValue = EnumOperation.GetValueFromDescription<Compass.Direction>(_positionValues[2]);
                    _rover.RoverDirection = rotateValue;
                    StepFour();
                }
                catch (InvalidOperationException)
                {
                    System.Console.WriteLine();
                    System.Console.WriteLine($"Girilen yön hatalı !  {_positionValues[2].ToString() } tipinde bir yön bulunmamaktadır");
                    StepTwo();
                }
            }
            else
            {
                System.Console.WriteLine();
                System.Console.WriteLine(positionRetunValu.ReturnMessage);
                StepOne();
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
                Rover lasRoverValues = RoverOperation.Motion(_rover, _positionMaxValues, orientationValues);
                System.Console.WriteLine();
                System.Console.WriteLine($"Rover is last Position X-Y-Z :" +
                                         $"{lasRoverValues.PositionX.ToString()}-" +
                                         $"{lasRoverValues.PositionY.ToString()}-" +
                                         $"{EnumOperation.GetEnumDescription<Direction>(lasRoverValues.RoverDirection)}");
                //  işlem basarı ile bitince tekrar step1 diyip basa dönüyoruz
                StepOne();
            }
            catch (Exception)
            {
                System.Console.WriteLine();
                System.Console.WriteLine($"Bir hata oluştu ");
                StepFour();
            }
        }
        #endregion


        #region//Yardımcı methodlar
        private static List<int> GetMaxPositionRead()
        {
            List<int> values = new List<int>();
            var readValue = GetRetrieveEnteredData();
            if (readValue != null && readValue.Count == 2)
                foreach (var readKey in readValue)
                {
                    if (RoverOperation.CharacterIsControl(Convert.ToChar(readKey), true))
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
