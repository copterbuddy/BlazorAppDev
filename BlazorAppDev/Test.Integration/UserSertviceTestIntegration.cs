using BlazorAppDev;
using BlazorAppDev.Server.Repositories.MyDb;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Integration.Creators;
using Test.Integration.Setup;

namespace Test.Integration;

public class UserServiceTestIntegration : IntegrationTestBase
{
    private readonly IntegrationTestFactory<Program, MyDbContext> _integrationTestFactory;
    private readonly UserCreator _userCreator;

    public UserServiceTestIntegration(IntegrationTestFactory<Program, MyDbContext> factory) : base(factory)
    {
        _integrationTestFactory = factory;
        var scope = factory.Services.CreateScope();
        _userCreator = scope.ServiceProvider.GetRequiredService<UserCreator>();
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
}
