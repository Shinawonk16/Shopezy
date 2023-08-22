namespace Application.Dtos;

public class OrderProductDto
{
    public ProductDto ProductDto { get; set; }
        public decimal NetAmount { get; set; }

    public List<OrderDto> OrderDto { get; set; }
    public AddressDto AddressDto { get; set; }
    public int Quantity { get; set; }
}
