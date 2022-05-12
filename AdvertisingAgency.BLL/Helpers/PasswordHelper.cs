using System.Security.Cryptography;

namespace AdvertisingAgency.BLL.Helpers;

public static class PasswordHelper
{
    public static string GenerateToken(int length)
    {
        using var cryptRNG = new RNGCryptoServiceProvider();
        var tokenBuffer = new byte[length];
        cryptRNG.GetBytes(tokenBuffer);
        return Convert.ToBase64String(tokenBuffer);
    }
}