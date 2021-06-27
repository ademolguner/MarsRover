
using StudyCase.Entities;
using StudyCase.Enums;
using StudyCase.Interfaces;

namespace StudyCase.Managers
{
    public class MarsRover : Rover, IRover
    { 
        #region Public Methods

        public bool IsOutOfBorder()
        {
            return
                Plateau.MinWidth <= Location.PointX &&
                Location.PointX <= Plateau.Width &&
                Plateau.MinHeight <= Location.PointY &&
                Location.PointY <= Plateau.Height;
        }
        public Location GetLocation()
        {
            return Location;
        }
        public Rotation GetRotation()
        {
            return Rotation;
        }
        public Plateau GetPlateau()
        {
            return Plateau;
        }
        public void SetLocation(Location location, Rotation rotation = Rotation.N)
        {
            if (Location != null)
            {
                Location.PointX = location.PointX;
                Location.PointY = location.PointY;
            }
            else
            {
                Location = new Location(location.PointX, location.PointY);
            }
            Rotation = rotation;
        }
        public void SetLocation(int pointX, int pointY, Rotation rotation = Rotation.N)
        {
            if (Location != null)
            {
                Location.PointX = pointX;
                Location.PointY = pointY;
            }
            else
            {
                Location = new Location(pointX, pointY);
            }
            Rotation = rotation;
        }
        public void SetPlateau(Plateau plateau)
        {
            Plateau = plateau;
        }
        public void SetRotation(Rotation rotation)
        {
            Rotation = rotation;
        }

        #endregion
    }
}
