using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using NUnit.Framework;
using RealEstate.Api;
using RealEstate.Application.Dtos;
using RealEstate.Application.Services;
using RealEstate.Infrastructure.Repositories;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace RealEstate.Api.Tests.Controllers
{
    public class PropertiesControllerTests
    {
        private WebApplicationFactory<Program>? _factory;
        private Mock<IPropertyService>? _propertyServiceMock;

        [SetUp]
        public void SetUp()
        {
            _propertyServiceMock = new Mock<IPropertyService>();

            _factory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.AddSingleton(_propertyServiceMock.Object);
                    });
                });
        }

        [TearDown]
        public void TearDown()
        {
            _factory?.Dispose();
        }

        [Test]
        public async Task GetProperties_ReturnsOkAndList()
        {
            _propertyServiceMock!
                .Setup(s => s.GetAsync(It.IsAny<FilterParams>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new List<PropertyListDto> { new PropertyListDto { IdProperty = "1", Name = "Fisher Island House" } });

            var client = _factory!.CreateClient();
            var resp = await client.GetAsync("/api/properties");

            resp.StatusCode.Should().Be(HttpStatusCode.OK);

            var list = await resp.Content.ReadFromJsonAsync<List<PropertyListDto>>();
            list.Should().NotBeNull();
            list!.Count.Should().BeGreaterThan(0);
            list[0].Name.Should().NotBeNullOrEmpty();
            list[0].Name.Should().Be("Fisher Island House");
        }
    }
}