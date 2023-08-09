using Domain.Common;

namespace Domain.Entities;

public class Rider:BaseEntity
{
    public User User { get; set; }
    public string UserId { get; set; }
}
