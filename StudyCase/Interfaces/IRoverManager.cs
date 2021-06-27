namespace StudyCase.Interfaces
{
    public interface IRoverManager
    {
        void ExecuteCommand(string rotationCommand);
        string GetStatusText();

    }
}
