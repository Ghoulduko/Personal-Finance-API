using System.Runtime.Serialization;

namespace Finance.Core.Exceptions.UserExcepTions;

public class IncorrectPasswordException : Exception
{
    public IncorrectPasswordException()
    {
    }

    protected IncorrectPasswordException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public IncorrectPasswordException(string? message) : base(message)
    {
    }

    public IncorrectPasswordException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}