using System.Runtime.Serialization;

namespace Finance.Core.Exceptions.UserExcepTions;

public class InvalidUsernameException : Exception
{
    public InvalidUsernameException()
    {
    }

    protected InvalidUsernameException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public InvalidUsernameException(string? message) : base(message)
    {
    }

    public InvalidUsernameException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}