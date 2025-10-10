using RealEstate.Application.Dtos;
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

        private string BuildImageUrl(string idProperty, string fileName)
        {
            var requestPath = _imagesOptions.ImagesRequestPath?.TrimEnd('/') ?? "/assets/img/propertyImgs";
            var relativePath = $"{requestPath}/{Uri.EscapeDataString(idProperty)}/{Uri.EscapeDataString(fileName)}";
            if (!string.IsNullOrWhiteSpace(_imagesOptions.PublicBaseUrl))
            {
                var baseUrl = _imagesOptions.PublicBaseUrl!.TrimEnd('/');
                return $"{baseUrl}{relativePath}";
            }
            return relativePath;
        }

        public async Task<PropertyDetailDto?> GetByIdAsync(string id)
        {
            var p = await _repo.GetByIdAsync(id);
            if (p == null) return null;

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


            var images = imagesMeta?
                .Where(i => i.Enabled)
                .Select(i => BuildImageUrl(p.IdProperty, i.File))
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
                Traces = tracesDto
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
                var imageUrl = first == null ? null : BuildImageUrl(p.IdProperty, first.File);
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