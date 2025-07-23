namespace api.utils;

using Microsoft.AspNetCore.Identity;

public class Hash
{
    public static string HashPassword(string plainPassword)
    {
        var hasher = new PasswordHasher<object>();
        return hasher.HashPassword(null, plainPassword);
    }

    public static bool VerifyPassword(string hashedPassword, string inputPassword)
    {
        var hasher = new PasswordHasher<object>();
        var result = hasher.VerifyHashedPassword(null, hashedPassword, inputPassword);
        return result == PasswordVerificationResult.Success;
    }
}