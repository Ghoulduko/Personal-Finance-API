using System.Security.Cryptography;
using System.Text;

namespace Finance.Application.ExtensionMethods;

public static class TokenHasher
{
    public static string GenerateTokenHash(this string token)
    {
        using var sha = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(token);
        var hashBytes = sha.ComputeHash(bytes);
        return Convert.ToBase64String(hashBytes);
    }
}