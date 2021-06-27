namespace StudyCase.Entities
{
    public class Location
    {

        public Location(int pointX, int pointY)
        {
            PointX = pointX;
            PointY = pointY;
        }

        public int PointX { get; set; }
        public int PointY { get; set; }
    }
}
