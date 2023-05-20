using BlazorAppDev.Client.AuthProviders;
using BlazorAppDev.Client.Configuration;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorAppDev.Client;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);

        builder.Configuration
            .AddJsonFile($"appsettings.json",optional: true,reloadOnChange: true)
            .AddJsonFile($"appsettings.{builder.HostEnvironment.Environment}.json",optional: true,reloadOnChange: true);

        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services.AddAuthorizationCore();
        builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

        string baseUrl = string.Empty;
        BaseOptions baseOptions = BaseOptions.GetBaseOptions(builder);
        builder.Services.AddSingleton(baseOptions);

        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseOptions.Url) });

        await builder.Build().RunAsync();
    }
}