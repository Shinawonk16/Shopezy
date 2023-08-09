namespace Application.Dtos;

public class OrderProductDto
{
    public ProductDto ProductDto { get; set; }
    public OrderDto OrderDto { get; set; }
    public int Quantity { get; set; }
}
