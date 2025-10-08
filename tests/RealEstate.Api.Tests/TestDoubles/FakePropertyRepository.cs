using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.Repositories;

namespace RealEstate.Api.Tests.TestDoubles
{
    public class FakePropertyRepository : IPropertyRepository
    {
        private readonly List<Property> _store;

        public FakePropertyRepository(IEnumerable<Property>? initial = null)
        {
            _store = initial?.ToList() ?? new List<Property>();
        }

        public Task<List<Property>> GetAsync(FilterParams filter, int page = 1, int pageSize = 20, CancellationToken ct = default)
        {
            var skip = (page - 1) * pageSize;
            var result = _store.Skip(skip).Take(pageSize).ToList();
            return Task.FromResult(result);
        }

        public Task<Property?> GetByIdAsync(string id, CancellationToken ct = default)
        {
            var p = _store.FirstOrDefault(x => x.IdProperty == id);
            return Task.FromResult(p);
        }
    }
}