using System;
using System.Runtime.Serialization;

namespace SecurityConsultantCore.Pathfinding
{
    [Serializable]
    internal class NonMatchingException : Exception
    {
        public NonMatchingException()
        {
        }

        public NonMatchingException(string message) : base(message)
        {
        }

        public NonMatchingException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NonMatchingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}