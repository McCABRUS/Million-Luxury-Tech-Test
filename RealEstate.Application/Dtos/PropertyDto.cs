using System.Collections.Generic;

namespace RealEstate.Application.Dtos
{
    public class PropertyDto
    {
        public string IdProperty { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public decimal Price { get; set; }
        public string? CodeInternal { get; set; }
        public int? Year { get; set; }
        public string IdOwner { get; set; } = null!;
        public List<string>? Images { get; set; }
        public string? Description { get; set; }
        public bool Enabled { get; set; } = true;
    }
}