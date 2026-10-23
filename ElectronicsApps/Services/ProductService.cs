using ElectronicsApi.Data;
using ElectronicsApi.Dtos;
using ElectronicsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ElectronicsApi.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        // Inyección del contexto de base de datos
        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(string? sku, int? producerId, string? name)
        {
            var query = _context.Products.AsQueryable();

            // Filtro por SKU
            if (!string.IsNullOrEmpty(sku))
                query = query.Where(p => p.Sku == sku);

            // Filtro por Productor/Fabricante
            if (producerId.HasValue)
                query = query.Where(p => p.ProducerId == producerId);

            // 🔥 NUEVA LÓGICA: Búsqueda por Nombre (coincidencia parcial e insensible a mayúsculas)
            if (!string.IsNullOrEmpty(name))
                query = query.Where(p => p.Name.ToLower().Contains(name.ToLower()));

            return await query.ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<Product> CreateProductAsync(ProductDto productDto)
        {
            var product = new Product
            {
                Sku = productDto.Sku,
                Name = productDto.Name,
                Brand = productDto.Brand,
                Category = productDto.Category,
                Price = productDto.Price,
                ProducerId = productDto.ProducerId
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }
    }
}