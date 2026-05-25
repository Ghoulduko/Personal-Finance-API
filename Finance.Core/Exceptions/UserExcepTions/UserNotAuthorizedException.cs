using System.Runtime.Serialization;

namespace Finance.Core.Exceptions.UserExcepTions;

public class UserNotAuthorizedException : Exception
{
    public UserNotAuthorizedException()
    {
    }

    protected UserNotAuthorizedException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public UserNotAuthorizedException(string? message) : base(message)
    {
    }

    public UserNotAuthorizedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}