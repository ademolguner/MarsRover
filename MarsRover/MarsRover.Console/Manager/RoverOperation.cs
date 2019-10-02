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

        static readonly EnumOperation EnumOperation = new EnumOperation();
        static Entities.Rover _activeRover = new Entities.Rover();

        public ReturnDataResult IstPositionValuesCorrect(List<string> values)
        {
            ReturnDataResult data = new ReturnDataResult
            {
                IsCorrect = true
            };
            // 3 adet parametre olucak
            if (values.Count != 3)
            {
                data.IsCorrect = false;
                data.ReturnMessage = "Pozisyon için 3 ADET parametre girşi yapılmalıdır. X(int)-Y(int)-Z(text N-S-E-W)";
                return data;
            }
            if (!CharacterIsControl(Convert.ToChar(values[0]), true))
            {
                data.IsCorrect = false;
                data.ReturnMessage = "X Koordinatı için bir sayı giriniz";
                return data;
            }
            if (!CharacterIsControl(Convert.ToChar(values[1]), true))
            {
                data.IsCorrect = false;
                data.ReturnMessage = "Y Koordinatı için bir sayı giriniz";
                return data;
            }

            return data;
        }

        public bool CharacterIsControl(char charValue, bool controlStatus)
        {
            var data = char.IsNumber(charValue) == controlStatus ? true : false;
            return data;
        }

        public Rover Motion(Rover rover, List<int> maxBorderValues, string orientationValues)
        {
            _activeRover = rover;
            foreach (var move in orientationValues)
            {
                var rotateDestination = EnumOperation.GetValueFromDescription<Destination>(move.ToString());
                if (rotateDestination == Rotate.Destination.Stable)
                    StablePointNextGrid();
                var activeRoverValue = (int)_activeRover.RoverDirection;
                var rotatedDestinationValue = (int)rotateDestination;
                RoverRotateMovement(activeRoverValue, rotatedDestinationValue);

                GridPointControl(maxBorderValues);
            }

            return rover;
        }

        private void RoverRotateMovement(int roverInfoValue, int rotateDestinationInfoValue)
        {
            var operationStepValue = (roverInfoValue) + (rotateDestinationInfoValue);
            if (roverInfoValue == EnumOperation.GetEnumItemCount<Direction>() && rotateDestinationInfoValue > 0)
            {
                operationStepValue = 1;
            }
            if (operationStepValue == 0)
            {
                operationStepValue = EnumOperation.GetEnumItemCount<Direction>();
            }
            _activeRover.RoverDirection = (Direction)int.Parse(operationStepValue.ToString());
        }


        public void GridPointControl(List<int> maxBorderValues)
        {
            if (
                   _activeRover.PositionX < 0
                || _activeRover.PositionX > maxBorderValues[0]
                || _activeRover.PositionY < 0
                || _activeRover.PositionY > maxBorderValues[1]
               )
                throw new Exception($"Durrrrr! Sınırları aşıyorsun :) (0 , 0) --  ({maxBorderValues[0]} , {maxBorderValues[1]})");
        }


        private static void StablePointNextGrid()
        {
            var direction = EnumOperation.GetValueFromDescription<Direction>(_activeRover.RoverDirection.ToString().Substring(0, 1));
            if (direction == Direction.North)
                _activeRover.PositionY += 1;
            if (direction == Direction.South)
                _activeRover.PositionY -= 1;
            if (direction == Direction.East)
                _activeRover.PositionX += 1;
            if (direction == Direction.West)
                _activeRover.PositionX -= 1;
        }

    }
}
