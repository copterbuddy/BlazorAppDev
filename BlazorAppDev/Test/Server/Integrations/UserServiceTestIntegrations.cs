using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAppDev.Test.Server.Integrations
{
    public class UserServiceTestIntegrations
    {
        public UserServiceTestIntegrations() { 

        }

        [Fact]
        public async Task GreetingService()
        { 
            var webAppFacotry = new WebApplicationFactory<Program>();
            var httpClient = webAppFacotry.CreateDefaultClient();

            var response = await httpClient.GetAsync("/User/Greeting");
            var stringResult = await response.Content.ReadAsStringAsync();

            Assert.Equal("Service Running On ",stringResult);
        }
    }
}
