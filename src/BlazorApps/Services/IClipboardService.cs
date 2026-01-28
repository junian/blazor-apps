namespace BlazorApps.Services;

public interface IClipboardService
{
    Task WriteTextAsync(string text);
}
