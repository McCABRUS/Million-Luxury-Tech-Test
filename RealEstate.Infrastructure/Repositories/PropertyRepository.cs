using MongoDB.Bson;
using MongoDB.Driver;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Infrastructure.Repositories
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly MongoContext _context;
        public PropertyRepository(MongoContext context) => _context = context;

        public async Task<List<Property>> GetAsync(FilterParams filter, int page = 1, int pageSize = 20, CancellationToken ct = default)
        {
            var builder = Builders<Property>.Filter;
            var filters = new List<FilterDefinition<Property>>();

            if (!string.IsNullOrWhiteSpace(filter?.Name)) filters.Add(builder.Regex(p => p.Name, new BsonRegularExpression(filter.Name, "i")));
            if (!string.IsNullOrWhiteSpace(filter?.Address)) filters.Add(builder.Regex(p => p.Address, new BsonRegularExpression(filter.Address, "i")));
            if (filter?.PriceFrom.HasValue == true) filters.Add(builder.Gte(p => p.Price, filter.PriceFrom.Value));
            if (filter?.PriceTo.HasValue == true) filters.Add(builder.Lte(p => p.Price, filter.PriceTo.Value));

            var finalFilter = filters.Any() ? builder.And(filters) : builder.Empty;

            return await _context.Properties
                .Find(finalFilter)
                .Skip((page - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync(ct);
        }

        public async Task<Property?> GetByIdAsync(string id, CancellationToken ct = default)
        {
            return await _context.Properties.Find(p => p.IdProperty == id).FirstOrDefaultAsync(ct);
        }

        public async Task<List<PropertyImage>> GetImagesByPropertyIdAsync(string idProperty, CancellationToken ct = default)
        {
            return await _context.PropertyImages
                .Find(pi => pi.IdProperty == idProperty && pi.Enabled)
                .ToListAsync(ct);
        }

        public async Task<List<PropertyTrace>> GetTracesByPropertyIdAsync(string idProperty, CancellationToken ct = default)
        {
            return await _context.PropertyTraces
                .Find(pt => pt.IdProperty == idProperty)
                .ToListAsync(ct);
        }

        public async Task<List<Owner>> GetOwnersByIdsAsync(string idOwner, CancellationToken ct = default)
        {
            return await _context.Owners
                .Find(pt => pt.IdOwner == idOwner)
                .ToListAsync(ct);
        }


    }
}