using FluentAssertions;
using NUnit.Framework;
using RealEstate.Api.Tests.TestDoubles;
using RealEstate.Application.Dtos;
using RealEstate.Application.Services;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.Repositories;
using RealEstate.Infrastructure.Settings;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.Api.Tests.Services
{
    public class PropertyServiceTests
    {
        private FakePropertyRepository _fakeRepo = null!;
        private PropertyService _service = null!;

        [SetUp]
        public void Setup()
        {
            var initial = new List<Property>
            {
                new Property
                {
                    IdProperty = "60f5a3",
                    Name = "House 1",
                    Address = "Address 1",
                    Price = 1000m,
                    Images = new List<PropertyImage> { new PropertyImage { File = "img1.jpg", Enabled = true } },
                    IdOwner = "owner-1"
                }
            };

            var ownerExample = new Owner
            {
                IdOwner = "owner0",
                Name = "Miguel Prado",
                Address = "32 PALM AVE, Miami Beach, FL 33139",
                Photo = "owner0.jpg",
                Birthday = "04/13/1982"
            };


            _fakeRepo = new FakePropertyRepository(initial, new List<Owner> { ownerExample });
            var imagesOptions = new ImagesOptions
            {
                ImagesRootPath = Path.Combine(Directory.GetCurrentDirectory(), "assets", "img", "propertyImgs"),
                ImagesRequestPath = "/assets/img/propertyImgs",
                PublicBaseUrl = null
            };

            _service = new PropertyService(_fakeRepo, imagesOptions);
        }

        [Test]
        public async Task GetAsync_ShouldReturnMappedPropertyList()
        {
            var result = await _service.GetAsync(new FilterParams(), 1, 20);

            result.Should().BeAssignableTo<IEnumerable<PropertyListDto>>();
            var list = result.ToList();
            list.Count.Should().Be(1);
            list[0].Image.Should().Be("img1.jpg");
            list[0].Name.Should().Be("House 1");
        }

        [Test]
        public async Task GetByIdAsync_WhenNotFound_ReturnsNull()
        {
            var res = await _service.GetByIdAsync("nope");
            res.Should().BeNull();
        }

        [Test]
        public async Task GetByIdAsync_ReturnsOwners()
        {
            var res = await _service.GetByIdAsync("A11745701");
            res.Should().NotBeNull();
            res!.Owners.Should().NotBeNullOrEmpty();
            res.Owners[0].IdOwner.Should().Be("owner1");
            res.Owners[0].Name.Should().Be("Juan Perez");
            res.Owners[0].Photo.Should().Be("owner1.jpg");
        }

    }
}