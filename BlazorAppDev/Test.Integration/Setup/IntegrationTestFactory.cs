using BlazorAppDev.Test.Integration.Creators;
using CuratedArt.IntegrationTests.Setup;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MsSql;

public class IntegrationTestFactory<TProgram, TDbContext> : WebApplicationFactory<TProgram>, IAsyncLifetime
    where TProgram : class where TDbContext : DbContext
{
    private readonly MsSqlContainer _container;

    public IntegrationTestFactory()
    {
        _container = new MsSqlBuilder().Build();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveDbContext<TDbContext>();
            services.AddDbContext<TDbContext>(options => { options.UseSqlServer(_container.GetConnectionString()); });
            services.AddTransient<UserCreators>();
        });
    }

    private async Task InitialDbScript()
    {
        //ยังไม่มี script ให้ run
        List<string> scripts = new List<string>
        {
            "SELECT 1;",
        };

        scripts.ForEach(async script =>
        {
            await _container.ExecScriptAsync(script);
        });
    }

    public async Task InitializeAsync(){
        await _container.StartAsync();
        await InitialDbScript();

    }

    public new async Task DisposeAsync() => await _container.DisposeAsync();
}