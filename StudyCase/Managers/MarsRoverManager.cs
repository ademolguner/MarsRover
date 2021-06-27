using System;
using System.Linq;
using StudyCase.Enums;
using StudyCase.Exceptions;
using StudyCase.Interfaces;

namespace StudyCase.Managers
{
    public class MarsRoverManager : IRoverManager
    {
        private readonly IRover _rover;

        #region Constructor

        public MarsRoverManager(IRover rover)
        {
            _rover = rover;
        }

        #endregion

        #region Public Methods
        public void ExecuteCommand(string rotationCommand)
        {
            rotationCommand.ToList().ForEach(cmd =>
            {
                switch (cmd)
                {
                    case (char)Command.M:
                        {
                            if (!Move())
                            {
                               throw new OutOfBorderException();
                            }
                            break;
                        }
                    case (char)Command.L:
                        TurnLeft();
                        break;
                    case (char)Command.R:
                        TurnRight();
                        break;
                    default:
                        throw new InvalidCommandException();
                }
            });
        }
        public string GetStatusText()
        {
            return $"{_rover.GetLocation().PointX} {_rover.GetLocation().PointY} {_rover.GetRotation()}";
        }

        #endregion

        #region Private Methods

        private void TurnLeft()
        {
            _rover?.SetRotation(_rover.GetRotation() - 1 < Rotation.N
                ? Rotation.W
                : _rover.GetRotation() - 1);
        }
        private void TurnRight()
        {
            _rover?.SetRotation(_rover.GetRotation() + 1 > Rotation.W
                ? Rotation.N
                : _rover.GetRotation() + 1);
        }
        private bool Move()
        {
            if (!_rover.IsOutOfBorder())
                return false;

            switch (_rover.GetRotation())
            {
                case Rotation.N:
                    _rover.GetLocation().PointY += 1;
                    break;
                case Rotation.E:
                    _rover.GetLocation().PointX += 1;
                    break;
                case Rotation.S:
                    _rover.GetLocation().PointY -= 1;
                    break;
                case Rotation.W:
                    _rover.GetLocation().PointX -= 1;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return true;
        }

        #endregion
    }
}
