using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RealEstate.Domain.Entities;

namespace RealEstate.Infrastructure.Repositories
{
    public class FilterParams
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }
    }

    public interface IPropertyRepository
    {
        Task<List<Property>> GetAsync(FilterParams filter, int page = 1, int pageSize = 20, CancellationToken ct = default);
        Task<Property?> GetByIdAsync(string id, CancellationToken ct = default);
    }
}