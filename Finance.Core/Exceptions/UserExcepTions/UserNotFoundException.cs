using System.Runtime.Serialization;

namespace Finance.Core.Exceptions.UserExcepTions;

public class UserNotFoundException : Exception
{
    public UserNotFoundException()
    {
    }

    protected UserNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public UserNotFoundException(string? message) : base(message)
    {
    }

    public UserNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}