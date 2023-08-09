using Domain.Common;

namespace Domain.Entities;

public class OrderProduct : BaseEntity
{
    public Product Product { get; set; }
    public string ProductId { get; set; }
    public Order Order { get; set; }
    public string OrderId { get; set; }
    public int Quantity { get; set; }
}
