using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.FluentUI.AspNetCore.Components;
using BlazorApps;
using BlazorApps.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

ConfigureServices(builder.Services, builder.HostEnvironment.BaseAddress);

await builder.Build().RunAsync();

static void ConfigureServices(IServiceCollection services, string baseAddress)
{
    services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseAddress) });
    services.AddFluentUIComponents();
    services.AddScoped<IClipboardService, ClipboardService>();
    services.AddScoped<IHashService, HashService>();
    services.AddScoped<IStaticAssetService, ServerStaticAssetService>();
}