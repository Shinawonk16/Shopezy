using Domain.Common;

namespace Domain.Entities;

public class Carts:BaseEntity
{
        public Customer Customer { get; set; } 
        public ICollection<CartItems> Items { get; set; } = new HashSet<CartItems>();
        public Order? Order { get; set; }
}
