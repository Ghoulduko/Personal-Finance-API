using System.Runtime.Serialization;

namespace Finance.Core.Exceptions.UserExcepTions;

public class UserAlreadyRegisteredException : Exception
{
    public UserAlreadyRegisteredException()
    {
    }

    protected UserAlreadyRegisteredException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public UserAlreadyRegisteredException(string? message) : base(message)
    {
    }

    public UserAlreadyRegisteredException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}