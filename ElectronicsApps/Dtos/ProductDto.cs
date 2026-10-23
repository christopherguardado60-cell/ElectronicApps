using System.ComponentModel.DataAnnotations;

namespace ElectronicsApi.Dtos
{
    public class ProductDto
    {
        [Required]
        public string Sku { get; set; } = string.Empty;
        
        [Required]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        public string Brand { get; set; } = string.Empty;
        
        [Required]
        public string Category { get; set; } = string.Empty;
        
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal Price { get; set; }
        
        public int? ProducerId { get; set; }
    }
}