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
        private readonly List<Owner> _owners;


        public FakePropertyRepository(IEnumerable<Property>? initial = null, List<Owner>? initialOwners = null)
        {
            _store = initial?.ToList() ?? new List<Property>();
            _owners = initialOwners ?? new List<Owner>();

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

        public Task<List<PropertyImage>> GetImagesByPropertyIdAsync(string idProperty, CancellationToken ct = default)
        {
            var prop = _store.FirstOrDefault(x => x.IdProperty == idProperty);
            var images = prop?.Images?.Where(i => i.Enabled).ToList() ?? new List<PropertyImage>();
            return Task.FromResult(images);
        }

        public Task<List<PropertyTrace>> GetTracesByPropertyIdAsync(string idProperty, CancellationToken ct = default)
        {
            var prop = _store.FirstOrDefault(x => x.IdProperty == idProperty);
            var traces = prop?.Traces?.ToList() ?? new List<PropertyTrace>();
            return Task.FromResult(traces);
        }

        public Task<List<Owner>> GetOwnersByIdsAsync(string idOwner, CancellationToken ct = default)
        {
            var result = _owners.Where(o => o.IdOwner == idOwner).ToList();
            return Task.FromResult(result);
        }
    }
}