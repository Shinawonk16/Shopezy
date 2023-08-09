using Domain.Common;

namespace Domain.Entities;

public class Verification : BaseEntity
{
    public string Code { get; set; }
    public string CustomerId { get; set; }
    public Customer Customer { get; set; }
}
