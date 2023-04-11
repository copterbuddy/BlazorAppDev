using BlazorAppDev;
using BlazorAppDev.Server.Repositories.MyDb;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Integration.Setup;

public class IntegrationTestBase : IClassFixture<IntegrationTestFactory<Program, MyDbContext>>
{
    public readonly IntegrationTestFactory<Program, MyDbContext> Factory;
    public readonly MyDbContext DbContext;

    public IntegrationTestBase(IntegrationTestFactory<Program, MyDbContext> factory)
    {
        Factory = factory;
        var scope = factory.Services.CreateScope();
        DbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();
    }
}
