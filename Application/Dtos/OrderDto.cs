using Domain.Entities;

namespace Application.Dtos;

public class OrderDto
{
    public bool IsDelivered { get; set; }
    public string Id { get; set; }
    public CustomerDto Customer { get; set; }
    public PaymentDto PaymentDto { get; set; }
    public string CreatedAt { get; set; }
    // public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    // public SalesDto SalesDto{get;set;}
    public string OrderedDate { get; set; }
    public int Quantity { get; set; }
    public decimal AmountPaid { get; set; }


    public string DeliveredDate { get; set; }
    public Address Address { get; set; }
    public ProductDto Product { get; set; }
}
public class CreateOrderRequestModel
{
    public bool IsDelivered { get; set; } = false;
    public Address Address { get; set; }
    // public int Quantity { get; set; }
    public List<OrderRequestModel> OrderRequestModels { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

}
public class OrderRequestModel
{
        public string ProductId { get; set; }
        public int Quantity { get; set; }

}
public class UpdateOrderRequestModel
{
    public bool IsDelivered { get; set; }
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // public Address Address { get; set; }
}
