using BlazorAppDev;
using BlazorAppDev.Server.Repositories.MyDb;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcontainers.MsSql;

namespace Test.Integration;

public class UserServiceTestIntegration : IAsyncLifetime
{
    private readonly MsSqlContainer _msSqlContainer = new MsSqlBuilder().Build();

    Task IAsyncLifetime.InitializeAsync()
    {
        return _msSqlContainer.StartAsync();
    }

    Task IAsyncLifetime.DisposeAsync()
    {
        return _msSqlContainer.DisposeAsync().AsTask();
    }

    [Fact]
    public async Task GreetingService()
    {
        var webAppFacotry = new WebApplicationFactory<Program>();
        var httpClient = webAppFacotry.CreateDefaultClient();

        var response = await httpClient.GetAsync("/User/Greeting");
        var stringResult = await response.Content.ReadAsStringAsync();

        Assert.Equal("Service Running On ", stringResult);
    }

    [Fact]
    public void ConnectionStateReturnsOpen()
    {
        // Given
        using DbConnection connection = new SqlConnection(_msSqlContainer.GetConnectionString());

        // When
        connection.Open();

        // Then
        Assert.Equal(ConnectionState.Open, connection.State);
    }

    [Fact]
    public async Task ExecScriptReturnsSuccessful()
    {
        // Given
        const string scriptContent = "SELECT 1;";

        // When
        var execResult = await _msSqlContainer.ExecScriptAsync(scriptContent)
            .ConfigureAwait(false);

        // When
        Assert.True(0L.Equals(execResult.ExitCode), execResult.Stderr);
    }
}
