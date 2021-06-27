using StudyCase.Enums;

namespace StudyCase.Entities
{
    public class Rover
    {
        protected Plateau Plateau { get; set; }
        protected Location Location { get; set; }
        protected Rotation Rotation { get; set; }
    }
}
