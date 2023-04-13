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
using BlazorAppDev.Shared.Models;
using System.Net.Http.Json;
using CuratedArt.IntegrationTests.Setup;
using BlazorAppDev.Test.Integration.Creators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DotNet.Testcontainers.Containers;

namespace Test.Integration;

public class UserServiceTestIntegration : IntegrationTestBase
{
    private readonly IntegrationTestFactory<Program, MyDbContext> _integrationTestFactory;
    private readonly UserCreators _userCreators;

    public UserServiceTestIntegration(IntegrationTestFactory<Program, MyDbContext> factory) : base(factory)
    {
        _integrationTestFactory = factory;
        var scope = factory.Services.CreateScope();
        _userCreators = scope.ServiceProvider.GetRequiredService<UserCreators>();
    }

    [Fact]
    public async Task GreetingService()
    {
        //var webAppFacotry = new WebApplicationFactory<Program>();
        var _client = _integrationTestFactory.CreateDefaultClient();

        HttpResponseMessage response = await _client.GetAsync("/User/Greeting");
        var stringResult = await response.Content.ReadAsStringAsync();

        Assert.Equal("Service Running On ", stringResult);
    }

    [Fact]
    public async Task RegisterService()
    {
        var _client = _integrationTestFactory.CreateDefaultClient();

        var request = new RegisterRequest
        {
            Email = "test@example.com",
            Password = "password",
            ConfirmPassword = "password"
        };

        HttpResponseMessage resp = await _client.PostAsJsonAsync("/User/Register", request);
        RegisterResponse respObj = await resp.Content.ReadFromJsonAsync<RegisterResponse>();

        Assert.True(resp.IsSuccessStatusCode);
        Assert.True(respObj.Result);
    }
}
