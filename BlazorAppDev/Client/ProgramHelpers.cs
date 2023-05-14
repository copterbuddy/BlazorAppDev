using BlazorAppDev.Client.Configuration;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

public static class ProgramHelpers
{

    public static BaseOptions NewMethod(WebAssemblyHostBuilder builder)
    {
        BaseOptions baseOptions = new();
        builder.Configuration.Bind(BaseOptions.SectionName, baseOptions);
        builder.Services.AddSingleton(baseOptions);
        return baseOptions;
    }
}