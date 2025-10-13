using RealEstate.Application.Dtos;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.Repositories;
using RealEstate.Infrastructure.Settings;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
namespace RealEstate.Application.Services
{
    public class PropertyService
    {
        private readonly IPropertyRepository _repo;
        private readonly ImagesOptions _imagesOptions;
        public PropertyService(IPropertyRepository repo, ImagesOptions imagesOptions)
        {
            _repo = repo;
            _imagesOptions = imagesOptions;
        }

        private string BuildPropertyImageUrl(string subfolder, string relativePathOrFile)
        {
            if (!string.IsNullOrWhiteSpace(_imagesOptions.PublicBaseUrl))
            {
                return $"{_imagesOptions.PublicBaseUrl.TrimEnd('/')}{_imagesOptions.ImagesRequestPath}/{subfolder}/{relativePathOrFile.TrimStart('/')}";
            }
            return $"{_imagesOptions.ImagesRequestPath}/{subfolder}/{relativePathOrFile.TrimStart('/')}";
        }

        private string BuildOwnerImageUrl(string idOwner)
        {
            if (!string.IsNullOrWhiteSpace(_imagesOptions.PublicBaseUrl))
            {
                return $"{_imagesOptions.PublicBaseUrl.TrimEnd('/')}{_imagesOptions.ImagesRequestPath}/ownersImgs/{Uri.EscapeDataString(idOwner)}.jpg";
            }
            return $"{_imagesOptions.ImagesRequestPath}/ownersImgs/{Uri.EscapeDataString(idOwner)}.jpg";
        }


        public async Task<PropertyDetailDto?> GetByIdAsync(string id)
        {
            var p = await _repo.GetByIdAsync(id);
            if (p == null) return null;

            var ownerIds = new List<string>();
            if (!string.IsNullOrWhiteSpace(p.IdOwner)) ownerIds.Add(p.IdOwner);
            var imagesMeta = await _repo.GetImagesByPropertyIdAsync(p.IdProperty);
            var tracesEntities = await _repo.GetTracesByPropertyIdAsync(p.IdProperty);
            var tracesDto = tracesEntities.Select(t => new PropertyTraceDto
            {
                IdPropertyTrace = t.IdPropertyTrace,
                DateSale = t.DateSale,
                Name = t.Name,
                Value = t.Value,
                Tax = t.Tax,
                IdProperty = t.IdProperty
            }).ToList();

            var ownersDto = new List<OwnerDto>();
            if (ownerIds.Any())
            {
                var ownersEntities = await _repo.GetOwnersByIdsAsync(p.IdOwner);
                ownersDto = ownersEntities.Select(o => new OwnerDto
                {
                    IdOwner = o.IdOwner,
                    Name = o.Name,
                    Address = o.Address,
                    Birthday = o.Birthday,
                    Photo = BuildOwnerImageUrl(o.IdOwner)
                }).ToList();

            }

            var images = imagesMeta
                .Where(img => img.Enabled)
                .Select(img => BuildPropertyImageUrl("propertyImgs", $"{p.IdProperty}/{img.File}"))
                .ToList() ?? new List<string>();

            return new PropertyDetailDto
            {
                IdProperty = p.IdProperty,
                Name = p.Name,
                Address = p.Address,
                Price = p.Price,
                CodeInternal = p.CodeInternal,
                Year = p.Year,
                IdOwner = p.IdOwner,
                Images = images,
                Traces = tracesDto,
                Owners = ownersDto
            };
        }

        public async Task<List<PropertyListDto>> GetAsync(FilterParams filter, int page = 1, int pageSize = 20)
        {
            var props = await _repo.GetAsync(filter, page, pageSize);
            var list = new List<PropertyListDto>();
            foreach (var p in props)
            {
                var imagesMeta = await _repo.GetImagesByPropertyIdAsync(p.IdProperty);
                var first = imagesMeta?.Where(i => i.Enabled).FirstOrDefault();
                var imageUrl = first == null ? null : BuildPropertyImageUrl("propertyImgs", $"{p.IdProperty}/{first.File}");
                list.Add(new PropertyListDto
                {
                    IdProperty = p.IdProperty,
                    Name = p.Name,
                    Address = p.Address,
                    Price = p.Price,
                    Image = imageUrl,
                    IdOwner = p.IdOwner
                });
            }
            return list;
        }
    }
}