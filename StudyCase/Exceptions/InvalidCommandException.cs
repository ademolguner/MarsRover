using System;

namespace StudyCase.Exceptions
{
    public class InvalidCommandException : Exception
    {
        public InvalidCommandException() : base(@"Invalid Command.")
        {
            
        } 
    }
}
