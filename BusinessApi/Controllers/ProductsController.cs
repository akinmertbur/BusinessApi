using BusinessApi.Data;
using BusinessApi.DTOs.Product;
using BusinessApi.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BusinessApi.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase {
        private readonly AppDbContext _context;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(AppDbContext context, ILogger<ProductsController> logger) {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductReadDto>>> GetAll() {
            var products = await _context.Products
                .Select(p => new ProductReadDto {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt
                })
                .ToListAsync();

            return Ok(products);
        }
    }
}
