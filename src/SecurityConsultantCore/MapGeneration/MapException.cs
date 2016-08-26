using System;

namespace SecurityConsultantCore.MapGeneration
{
    public class MapException : Exception
    {
        public MapException()
        {
        }

        public MapException(string message) : base(message)
        {
        }

        public MapException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}