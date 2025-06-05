using Microsoft.AspNetCore.Identity;

namespace TEST_DEV_DAAR_03062025.Utils;

public static class PasswordUtils
{
    private static readonly PasswordHasher<object> hasher = new();

    public static string Hash(string password)
    {
        return hasher.HashPassword(new object(), password);
    }

    public static bool Verify(string hashedPassword, string plainPassword)
    {
        var result = hasher.VerifyHashedPassword(new object(), hashedPassword, plainPassword);
        return result == PasswordVerificationResult.Success;
    }
}
