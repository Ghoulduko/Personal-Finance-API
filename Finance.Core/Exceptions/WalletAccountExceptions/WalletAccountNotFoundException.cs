using System.Runtime.Serialization;

namespace Finance.Core.Exceptions.WalletAccountExceptions;

public class WalletAccountNotFoundException : Exception
{
    public WalletAccountNotFoundException()
    {
    }

    protected WalletAccountNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public WalletAccountNotFoundException(string? message) : base(message)
    {
    }

    public WalletAccountNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}