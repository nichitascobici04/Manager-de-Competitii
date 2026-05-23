using Client;
using Client.Pages;
using Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

// keep a usable HttpClient for browser fetches (PatternApiClient builds full URLs itself)
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton<RequestLogService>();
builder.Services.AddScoped<PatternApiClient>();
builder.Services.AddScoped<StorageService>();

var host = builder.Build();

// Initialize PatternApiClient.BaseUrl from localStorage (if present) so API calls use correct controller URL
using (var scope = host.Services.CreateScope())
{
    var api = scope.ServiceProvider.GetRequiredService<PatternApiClient>();
    var js = scope.ServiceProvider.GetRequiredService<IJSRuntime>();

    try
    {
        // read saved base URL (if user saved one from UI)
        var savedBase = await js.InvokeAsync<string>("localStorage.getItem", "apiBaseUrl");
        // default to the host environment (site) + api path — works when Server serves the app
        var defaultBase = builder.HostEnvironment.BaseAddress?.TrimEnd('/') + "/api/Patterns/";
        if (!string.IsNullOrWhiteSpace(savedBase))
        {
            api.SetBaseUrl(savedBase);
        }
        else
        {
            api.SetBaseUrl(defaultBase ?? "/api/Patterns/");
        }
    }
    catch
    {
        // JS interop failed (e.g. in unit tests); fall back to default
        var defaultBase = builder.HostEnvironment.BaseAddress?.TrimEnd('/') + "/api/Patterns/";
        scope.ServiceProvider.GetRequiredService<PatternApiClient>().SetBaseUrl(defaultBase ?? "/api/Patterns/");
    }
}

await host.RunAsync();