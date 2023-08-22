namespace Application.Dtos;

public class ProductQuantityDto
{
    public ProductDto ProductDto { get; set; }
    public RestockEventDto RestockEventDto { get; set; }
    public IList<SalesDto> SalesDto { get; set; }



}
public class RestockEventDto
{
    public DateTime RestockDate { get; set; } = DateTime.UtcNow;
    public int QuantityAdded { get; set; }
    public string RestockedBy { get; set; }
    public string Notes { get; set; }
}
public class UpdateProductQuantityRequestModel
{
    public DateTime RestockDate { get; set; }
    public int QuantityAdded { get; set; }
    public string RestockedBy { get; set; }
    public string Notes { get; set; }
}
