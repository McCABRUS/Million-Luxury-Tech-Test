using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealEstate.Application.Dtos;
using RealEstate.Infrastructure.Repositories;

namespace RealEstate.Application.Services
{
    public class PropertyService
    {
        private readonly IPropertyRepository _repo;
        public PropertyService(IPropertyRepository repo) => _repo = repo;

        public async Task<List<PropertyListDto>> GetAsync(FilterParams filter, int page = 1, int pageSize = 20)
        {
            var props = await _repo.GetAsync(filter, page, pageSize);
            var result = new List<PropertyListDto>();

            foreach (var p in props)
            {
                var images = await _repo.GetImagesByPropertyIdAsync(p.IdProperty);
                result.Add(new PropertyListDto
                {
                    IdProperty = p.IdProperty,
                    Name = p.Name,
                    Address = p.Address,
                    Price = p.Price,
                    Image = images.FirstOrDefault()?.File,
                    IdOwner = p.IdOwner
                });
            }
            return result;
        }

        public async Task<PropertyDetailDto?> GetByIdAsync(string id)
        {
            var p = await _repo.GetByIdAsync(id);
            if (p == null) return null;

            var images = await _repo.GetImagesByPropertyIdAsync(p.IdProperty);
            var traces = await _repo.GetTracesByPropertyIdAsync(p.IdProperty);

            return new PropertyDetailDto
            {
                IdProperty = p.IdProperty,
                Name = p.Name,
                Address = p.Address,
                Price = p.Price,
                CodeInternal = p.CodeInternal,
                Year = p.Year,
                IdOwner = p.IdOwner,
                Images = images?.Where(i => i.Enabled).Select(i => i.File).ToList() ?? new List<string>(),
                Traces = traces?.Select(t => new PropertyTraceDto
                {
                    DateSale = t.DateSale,
                    Name = t.Name,
                    Value = t.Value,
                    Tax = t.Tax,
                    IdPropertyTrace = t.IdPropertyTrace,
                    IdProperty = t.IdProperty
                }).Cast<object>().ToList() ?? new List<object>()
            };
        }
    }
}