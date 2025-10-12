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
using System.IO;

namespace RealEstate.Api.Tests.Services
{
    public class PropertyServiceTests
    {
        private FakePropertyRepository _fakeRepo = null!;
        private PropertyService _service = null!;
        private List<Property> _initial = null!;
        private ImagesOptions _imagesOptions = null!;


        [SetUp]
        public void Setup()
        {
            // Asignar a los campos, no declarar variables locales
            _initial = new List<Property>
            {
                new Property
                {
                    IdProperty = "60f5a3",
                    Name = "House 1",
                    Address = "Address 1",
                    Price = 1000m,
                    Images = new List<PropertyImage> { new PropertyImage { File = "img1.jpg", Enabled = true } },
                    IdOwner = "owner0"
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

            _fakeRepo = new FakePropertyRepository(_initial, new List<Owner> { ownerExample });

            _imagesOptions = new ImagesOptions
            {
                ImagesRootPath = Path.Combine(Directory.GetCurrentDirectory(), "assets", "img", "propertyImgs"),
                ImagesRequestPath = "./assets/img",
                PublicBaseUrl = null
            };

            _service = new PropertyService(_fakeRepo, _imagesOptions);
        }


        [Test]
        public async Task GetAsync_ShouldReturnMappedPropertyList()
        {
            var result = await _service.GetAsync(new FilterParams(), 1, 20);
            var imgExpectedPath = $"{_imagesOptions.ImagesRequestPath}/propertyImgs/{_initial?[0].IdProperty}/{_initial?[0].Images?[0].File}";
            
            result.Should().BeAssignableTo<IEnumerable<PropertyListDto>>();
            var list = result.ToList();
            list.Count.Should().Be(1);
            list[0].Image.Should().Be(imgExpectedPath);
            list[0].Name.Should().Be("House 1");
            list[0].Address.Should().Be("Address 1");
            list[0].IdProperty.Should().Be("60f5a3");
            list[0].Price.Should().Be(1000);
            list[0].IdOwner.Should().Be("owner0");
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
            var res = await _service.GetByIdAsync("60f5a3");
            var imgExpectedPath = $"{_imagesOptions.ImagesRequestPath}/ownersImgs/owner0.jpg";
            res.Should().NotBeNull();
            res!.Owners.Should().NotBeNullOrEmpty();
            res.Owners[0].IdOwner.Should().Be("owner0");
            res.Owners[0].Name.Should().Be("Miguel Prado");
            res.Owners[0].Photo.Should().Be(imgExpectedPath);
            res.Owners[0].Birthday.Should().Be("04/13/1982");
        }

    }
}