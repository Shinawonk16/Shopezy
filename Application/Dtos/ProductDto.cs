using Microsoft.AspNetCore.Http;

namespace Application.Dtos;

 public class ProductDto
    {
        public string Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int NumberOfPlates { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public CategoryDto CategoryDto{get;set;}
        public DateTime AvailableTime { get; set; }
        public ICollection<OrderProductDto> OrderProductDtos { get; set; } = new HashSet<OrderProductDto>();

    }
    public class AddProductRequestModel
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public IFormFile Image { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
    }
    public class UpdateProductRequestModel
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public IFormFile Image { get; set; }
        public string Description { get; set; }
        
    }