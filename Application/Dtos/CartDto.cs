using Domain.Entities;

namespace Application.Dtos;

public class CartDto
{
    public string Id { get; set; }

    public CustomerDto CustomerDto { get; set; }
    public ICollection<CartItems> Items { get; set; } = new HashSet<CartItems>();
    public List<string>? Responses { get; set; }
}
public class CreateCartRequestModel
{
    public List<CartItemModel> CartItems { get; set; } = new List<CartItemModel>();

}
public class CartItemModel
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }