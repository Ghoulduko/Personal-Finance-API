using System.Security.Cryptography;
using Finance.Application.Interfaces;

namespace Finance.Application.Security;

public class RefreshTokenGenerator : IRefreshTokenGenerator
{
    public string Generate()
    {
        var bytes = RandomNumberGenerator.GetBytes(64);
        return Convert.ToBase64String(bytes);
    }
}