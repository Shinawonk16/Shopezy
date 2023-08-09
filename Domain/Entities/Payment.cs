using Domain.Common;

namespace Domain.Entities;

public class Payment : BaseEntity
{
    public string ReferenceNumber { get; set; } = Guid.NewGuid().ToString().Substring(0, 6);
    public Customer Customer { get; set; }
    public string CustomerId { get; set; }
    public Order Order { get; set; }
    public string OrderId { get; set; }
}
