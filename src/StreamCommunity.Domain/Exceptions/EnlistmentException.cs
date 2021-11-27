using System;

namespace StreamCommunity.Domain.Exceptions
{
    [Serializable]
    public sealed class EnlistmentException : Exception
    {
        public EnlistmentException(string? message) 
            : base(message)
        {
        }
        
        public EnlistmentException(string? message, Exception? innerException) 
            : base(message, innerException)
        {
        }
    }
}
