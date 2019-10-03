using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsRover.Console.Abstract;
using MarsRover.Console.Const;
using MarsRover.Console.Entities;
using static MarsRover.Console.Const.Compass;
using static MarsRover.Console.Const.Rotate;

namespace MarsRover.Console.Manager
{
    public class RoverOperation : IRoverOperation
    {
        private static readonly EnumOperation _enumOperation = new EnumOperation();
        public static Rover _activeRover = new Rover();




        public ReturnDataResult IsPositionValuesCorrect(List<string> values)
        {
            ReturnDataResult data = new ReturnDataResult
            {
                IsCorrect = true
            };
            if (values.Count != 3)
            {
                data.IsCorrect = false;
                data.ReturnMessage = "Pozisyon için 3 ADET parametre girşi yapılmalıdır. X(int)-Y(int)-Z(text [ N-S-E-W] )";
                return data;
            }
            if (!IsCharacterCorrect(Convert.ToChar(values[0]), true))
            {
                data.IsCorrect = false;
                data.ReturnMessage = "X Koordinatı için bir sayı giriniz";
                return data;
            }
            if (!IsCharacterCorrect(Convert.ToChar(values[1]), true))
            {
                data.IsCorrect = false;
                data.ReturnMessage = "Y Koordinatı için bir sayı giriniz";
                return data;
            }

            return data;
        }

        public bool IsCharacterCorrect(char charValue, bool controlStatus)
        {
            var data = char.IsNumber(charValue) == controlStatus ? true : false;
            return data;
        }

        public Rover Motion(Rover rover, List<int> maxBorderValues, string orientationValues)
        {
            _activeRover = rover;
            foreach (var move in orientationValues)
            {
                var rotateDestination = _enumOperation.GetEnumFromDescription<Destination>(move.ToString());
                if (rotateDestination == Rotate.Destination.Stable)
                {  StablePointNextGrid();}
                var activeRoverValue = (int)_activeRover.RoverDirection;
                var rotatedDestinationValue = (int)rotateDestination;
                RoverRotateMovement(activeRoverValue, rotatedDestinationValue);

                var returnControl = GridPointControl(maxBorderValues);
                if (!returnControl.IsCorrect)
                {
                    System.Console.WriteLine(returnControl.ReturnMessage);
                    return null;
                };
            }

            return rover;
        }



        private void RoverRotateMovement(int roverInfoValue, int rotateDestinationInfoValue)
        {
            var operationStepValue = (roverInfoValue) + (rotateDestinationInfoValue);
            if (roverInfoValue == _enumOperation.GetEnumItemsCount<Direction>() && rotateDestinationInfoValue > 0)
            {
                operationStepValue = 1;
            }
            if (operationStepValue == 0)
            {
                operationStepValue = _enumOperation.GetEnumItemsCount<Direction>();
            }
            _activeRover.RoverDirection = (Direction)int.Parse(operationStepValue.ToString());
        }

        public ReturnDataResult GridPointControl(List<int> maxBorderValues)
        {
            ReturnDataResult returnData = new ReturnDataResult
            {
                IsCorrect = true
            };

            if (_activeRover.PositionX < 0 || _activeRover.PositionX > maxBorderValues[0] || _activeRover.PositionY < 0 || _activeRover.PositionY > maxBorderValues[1])
            {
                returnData.IsCorrect = false;
                returnData.ReturnMessage =
                    $"Durrrrr! Sınırları aşıyorsun " +
                    $":Max Boyutlar  ({maxBorderValues[0]} , {maxBorderValues[1]}) , " +
                    $"aktif konum bilginiz ({_activeRover.PositionX.ToString()}, {_activeRover.PositionY.ToString()} - {_activeRover.RoverDirection.ToString()})";
            }
            return returnData;
        }

        private static void StablePointNextGrid()
        {
            // North ve South boylam olduğu için Y eksenini  (+ -) değişir
            // East  ve West  enlem  olduğu için X ekseninde  (+ -) değişir.
            var direction = _enumOperation.GetEnumFromDescription<Direction>(_activeRover.RoverDirection.ToString().Substring(0, 1));
            if (direction == Direction.North)
            { _activeRover.PositionY += 1;}
            if (direction == Direction.South)
            { _activeRover.PositionY -= 1;}
            if (direction == Direction.East)
            {  _activeRover.PositionX += 1;}
            if (direction == Direction.West)
            {  _activeRover.PositionX -= 1;}
        }

    }
}
