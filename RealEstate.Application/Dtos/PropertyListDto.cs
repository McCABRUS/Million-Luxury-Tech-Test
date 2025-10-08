namespace RealEstate.Application.Dtos
{
    public class PropertyListDto
    {
        public string IdProperty { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public decimal Price { get; set; }
        public string? Image { get; set; }
        public string? IdOwner { get; set; }
    }
}