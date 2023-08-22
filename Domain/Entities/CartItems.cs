using Domain.Common;

namespace Domain.Entities;

public class CartItems:BaseEntity
{
    public string ProductId { get; set; }
    public Product Product { get; set; }
    public decimal CostPrice { get; set; }
    public int Unit { get; set; }
    public Carts Carts { get; set; }
}
