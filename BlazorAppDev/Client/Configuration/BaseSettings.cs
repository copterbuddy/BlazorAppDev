using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
namespace BlazorAppDev.Client.Configuration;

public class BaseOptions
{
    public const string SectionName = "BaseSettings";
    public string Url { get; set; } = string.Empty;
    public string DockerUrl { get; set; } = string.Empty;

    public static BaseOptions GetBaseOptions(WebAssemblyHostBuilder builder)
    {
        BaseOptions baseOptions = new();
        builder.Configuration.Bind(SectionName, baseOptions);
        return baseOptions;
    }
}
