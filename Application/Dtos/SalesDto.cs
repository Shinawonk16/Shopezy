namespace Application.Dtos;

public class SalesDto
{
    public decimal AmountPaid { get; set; }
    public CustomerDto CustomerDto { get; set; }
    public string OrderId { get; set; }
    public string AddressId { get; set; }
    public OrderDto OrderDtos { get; set; }
}

public class CreateSalesRequestModel
{
    public string CustomerId { get; set; }
    public string ProductId { get; set; }
}