using BlazorAppDev;
using BlazorAppDev.Server.Repositories.MyDb;
using Microsoft.Extensions.DependencyInjection;

namespace CuratedArt.IntegrationTests.Setup
{
    public class IntegrationTestBase : IClassFixture<IntegrationTestFactory<Program, MyDbContext>>
    {
        public readonly IntegrationTestFactory<Program, MyDbContext> Factory;
        public readonly MyDbContext DbContext;

        public IntegrationTestBase(IntegrationTestFactory<Program, MyDbContext> factory)
        {
            Factory = factory;
            var scope = factory.Services.CreateScope();
            DbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();
            DbContext.Database.EnsureCreated();
        }
    }
}
