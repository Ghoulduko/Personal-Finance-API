using System.Runtime.Serialization;

namespace Finance.Core.Exceptions;

public class RoleNotFoundException : Exception
{
    public RoleNotFoundException()
    {
    }

    protected RoleNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public RoleNotFoundException(string? message) : base(message)
    {
    }

    public RoleNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}