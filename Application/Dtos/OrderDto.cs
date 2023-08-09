using Domain.Entities;

namespace Application.Dtos;

public class OrderDto
{
    public bool IsDelivered { get; set; }
    public string Id { get; set; }
    public CustomerDto Customer { get; set; }
    public PaymentDto PaymentDto { get; set; }
    // public SalesDto SalesDto{get;set;}
    public string OrderedDate { get; set; }
    public string DeliveredDate { get; set; }
    public Address Address { get; set; }
    public ICollection<OrderProductDto> OrderProductDtos { get; set; } = new HashSet<OrderProductDto>();
}
public class CreateOrderRequestModel
{
    public bool IsDelivered { get; set; } = false;
    public Address Address { get; set; }
    public int Quantity { get; set; }

}
public class UpdateOrderRequestModel
{
    public bool IsDelivered { get; set; }
    // public Address Address { get; set; }
}
