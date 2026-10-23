using ElectronicsApi.Dtos;
using ElectronicsApi.Models;

namespace ElectronicsApi.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductsAsync(string? sku, int? producerId, string? name);
        Task<Product?> GetProductByIdAsync(int id);
        Task<Product> CreateProductAsync(ProductDto productDto);
    }
}