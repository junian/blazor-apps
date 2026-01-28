using System.Security.Cryptography;
using System.Text;

namespace BlazorApps.Services;

public class HashService : IHashService
{
    public string ComputeHash(string input, string algorithmName)
    {
        byte[] inputBytes = Encoding.UTF8.GetBytes(input);
        return ComputeHash(inputBytes, algorithmName);
    }

    public string ComputeHash(byte[] input, string algorithmName)
    {
        byte[] hashBytes = algorithmName.ToUpperInvariant() switch
        {
            "MD5" => MD5.HashData(input),
            "SHA-1" => SHA1.HashData(input),
            "SHA-256" => SHA256.HashData(input),
            "SHA-384" => SHA384.HashData(input),
            "SHA-512" => SHA512.HashData(input),
            _ => throw new ArgumentException($"Unsupported algorithm: {algorithmName}")
        };

        return Convert.ToHexString(hashBytes).ToLowerInvariant();
    }

    public async Task<string> ComputeHashAsync(Stream input, string algorithmName)
    {
        using var memoryStream = new MemoryStream();
        await input.CopyToAsync(memoryStream);
        byte[] buffer = memoryStream.ToArray();
        return ComputeHash(buffer, algorithmName);
    }
}
