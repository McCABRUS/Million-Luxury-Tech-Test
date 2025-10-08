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
            return props.Select(p => new PropertyListDto
            {
                IdProperty = p.IdProperty,
                Name = p.Name,
                Address = p.Address,
                Price = p.Price,
                Image = p.Images?.FirstOrDefault()?.File,
                IdOwner = p.IdOwner
            }).ToList();
        }

        public async Task<PropertyDetailDto?> GetByIdAsync(string id)
        {
            var p = await _repo.GetByIdAsync(id);
            if (p == null) return null;
            return new PropertyDetailDto
            {
                IdProperty = p.IdProperty,
                Name = p.Name,
                Address = p.Address,
                Price = p.Price,
                CodeInternal = p.CodeInternal,
                Year = p.Year,
                IdOwner = p.IdOwner,
                Images = p.Images?.Where(i => i.Enabled).Select(i => i.File).ToList(),
                Traces = p.Traces?.Select(t => new { t.DateSale, t.Name, t.Value, t.Tax }).Cast<object>().ToList()
            };
        }
    }
}