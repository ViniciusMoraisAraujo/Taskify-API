using System.Security.Cryptography;

namespace TaskifyAPI.Services.PasswordHasher;

public class PasswordHasherService : IPasswordHasherService
{
    private const int SaltSize = 16;
    private const int HashSize = 32;
    private const int HashIterations = 1000;
    
    public string HashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
        
        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, HashIterations, HashAlgorithmName.SHA256);
        
        byte[] hash = pbkdf2.GetBytes(HashSize);
        
        byte [] hashBytes = new byte[SaltSize + HashSize];
        Array.Copy(salt, 0, hashBytes, 0, SaltSize);
        Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);
        return Convert.ToBase64String(hashBytes);
    }

    public bool VerifyHashedPassword(string password, string hashedPassword)
    {
        byte[] hashBytes = Convert.FromBase64String(hashedPassword);
        byte[] salt = new byte[SaltSize];
        Array.Copy(hashBytes, 0, salt, 0, SaltSize);
        byte[] storedHash = new byte[HashSize];
        Array.Copy(hashBytes, SaltSize, storedHash, 0, HashSize);
        
        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, HashIterations, HashAlgorithmName.SHA256);
        
        byte[] computedHash = pbkdf2.GetBytes(HashSize);
        
        return CryptographicOperations.FixedTimeEquals(storedHash,computedHash);
    }
}