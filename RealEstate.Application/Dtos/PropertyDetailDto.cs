using System.Collections.Generic;

namespace RealEstate.Application.Dtos
{
    public class PropertyDetailDto
    {
        public string IdProperty { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public decimal Price { get; set; }
        public string? CodeInternal { get; set; }
        public int? Year { get; set; }
        public string? IdOwner { get; set; }
        public List<string>? Images { get; set; }
        public List<PropertyTraceDto>? Traces { get; set; }

    }
}