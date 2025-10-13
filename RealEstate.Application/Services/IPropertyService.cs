using System.Collections.Generic;
using System.Threading.Tasks;
using RealEstate.Application.Dtos;
using RealEstate.Infrastructure.Repositories;

namespace RealEstate.Application.Services
{
    public interface IPropertyService
    {
        Task<IEnumerable<PropertyListDto>> GetAsync(FilterParams filter, int page, int rows);
        Task<PropertyDto?> GetByIdAsync(string id);
        Task<PropertyDto> CreateAsync(PropertyDto property);
        Task<bool> UpdateAsync(string id, PropertyDto property);
        Task<bool> DeleteAsync(string id);
    }
}