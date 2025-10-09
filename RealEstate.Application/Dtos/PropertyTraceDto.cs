namespace RealEstate.Application.Dtos
{
    public class PropertyTraceDto
    {
        public string IdPropertyTrace { get; set; } = null!;
        public string DateSale { get; set; } = null!;
        public string Name { get; set; } = null!;
        public decimal Value { get; set; }
        public decimal Tax { get; set; }
        public string IdProperty { get; set; } = null!;
    }
}
