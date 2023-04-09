using BlazorAppDev.Server.Controllers;
using BlazorAppDev.Server.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace BlazorAppDev.Test.Client.Controllers
{
    public class UserControllerTest : IDisposable
    {
        UserController controller;

        // setup
        public UserControllerTest()
        {
            var inMemorySettings = new Dictionary<string, string>
            {
                {"ASPNETCORE_ENVIRONMENT", "Development"},
                {"SectionName:SomeKey", "SectionValue"},
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();
            var loggerMock = new Mock<ILogger<UserController>>();
            var configMock = configuration;
            var userServiceMock = new Mock<IUserService>();
            controller = new UserController(loggerMock.Object, configMock, userServiceMock.Object);
        }

        // teardown
        public void Dispose()
        {
            // Dispose here
        }

        [Fact]
        public void Get_ReturnsOkObjectResult()
        {
            // Arrange

            // Act
            var result = controller.Get();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Get_ReturnsExpectedValue()
        {
            // Arrange
            var expectedValue = "Service Running On Development";

            // Act
            var result = controller.Get() as OkObjectResult;

            // Assert
            Assert.Equal(expectedValue, result.Value);
        }
    }
}
