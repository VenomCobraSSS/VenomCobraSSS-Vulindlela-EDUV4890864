using System.Security.Cryptography;

public static class PasswordHasher
{
    public static byte[] GenerateSalt()
    {
        return RandomNumberGenerator.GetBytes(32);
    }

    public static byte[] HashPassword(string password, byte[] salt)
    {
        return Rfc2898DeriveBytes.Pbkdf2(
            password,
            salt,
            100000,
            HashAlgorithmName.SHA256,
            32
        );
    }
}