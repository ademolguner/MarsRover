using StudyCase.Entities;
using StudyCase.Enums;


namespace StudyCase.Interfaces
{
    public interface IRover
    {
        bool IsOutOfBorder();
        Location GetLocation();
        Plateau GetPlateau();
        Rotation GetRotation();
        
        void SetLocation(Location location, Rotation rotation = Rotation.N);
        void SetLocation(int pointX, int pointY, Rotation rotation = Rotation.N);
        void SetPlateau(Plateau plateau);
        void SetRotation(Rotation rotation);
    }
}
