using Domain.Common;

namespace Domain.Entities;

public class ProductQuantity:BaseEntity
{
    public string ProductId{get;set;}
    public Product Product{get;set;}
    public RestockEvent RestockEvents{get;set;}
    public List<Sales> SalesHistory { get; set; }
    public DateTime Timestamp { get; set; }

   
}
public class RestockEvent:BaseEntity
{
    public DateTime RestockDate { get; set; }
    public int QuantityAdded { get; set; }
    public string RestockedBy { get; set; }
    public string Notes { get; set; }
}
