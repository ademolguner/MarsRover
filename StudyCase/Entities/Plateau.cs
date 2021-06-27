namespace StudyCase.Entities
{
    public class Plateau
    {
        #region Constructor
        public Plateau(int width, int height, int minWidth = 0, int minHeight = 0)
        {
            MinWidth = minWidth;
            MinHeight = minHeight;
            Width = width;
            Height = height;
        }
        #endregion
        #region Properties
        public int MinWidth { get; private set; }
        public int MinHeight { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        #endregion
    }
}
