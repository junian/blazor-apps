namespace BlazorApps.Services;

public interface IHashService
{
    string ComputeHash(string input, string algorithmName);
    string ComputeHash(byte[] input, string algorithmName);
    Task<string> ComputeHashAsync(Stream input, string algorithmName);
}
