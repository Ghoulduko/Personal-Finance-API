using System.Runtime.Serialization;

namespace Finance.Core.Exceptions.TransactionExceptions;

public class TransactionFailedException : Exception
{
    public TransactionFailedException()
    {
    }

    protected TransactionFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public TransactionFailedException(string? message) : base(message)
    {
    }

    public TransactionFailedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}