using ElectronicsApi.Dtos;
using ElectronicsApi.Models;
using ElectronicsApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        // Inyectamos la capa de servicios, NO el DbContext
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(
            [FromQuery] string? sku, 
            [FromQuery] int? producerId, 
            [FromQuery] string? name)
        {
            var products = await _productService.GetProductsAsync(sku, producerId, name);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null) return NotFound("Producto electrónico no encontrado");
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(ProductDto productDto)
        {
            var newProduct = await _productService.CreateProductAsync(productDto);
            return CreatedAtAction(nameof(GetProduct), new { id = newProduct.Id }, newProduct);
        }
    }
}