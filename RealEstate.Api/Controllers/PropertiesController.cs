using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Services;
using RealEstate.Infrastructure.Repositories;

namespace RealEstate.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertiesController : ControllerBase
    {
        private readonly PropertyService _svc;
        public PropertiesController(PropertyService svc) => _svc = svc;

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? name, [FromQuery] string? address, [FromQuery] decimal? priceFrom, [FromQuery] decimal? priceTo, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var filter = new FilterParams { Name = name, Address = address, PriceFrom = priceFrom, PriceTo = priceTo };
            var result = await _svc.GetAsync(filter, page, pageSize);
            return Ok(result);
        }

        [HttpGet("{propertyId}")]
        public async Task<IActionResult> GetById(string propertyId)
        {
            var dto = await _svc.GetByIdAsync(propertyId);
            if (dto == null) return NotFound();
            return Ok(dto);
        }

    }
}