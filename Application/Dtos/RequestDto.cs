namespace Application.Dtos;

public class RequestDto
{
    public string ProductId { get; set; }
    public string Id { get; set; }
    public string AdditionalNote { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public decimal Cost { get; set; }
    public ProductDto Product { get; set; }
    public bool IsApproved { get; set; }
    public int Quantity { get; set; }
}
public class CreateRequestRequestModel
{
    public decimal Cost { get; set; }
    public int Quantity { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string AdditionalNote { get; set; }
    public bool IsApproved { get; set; }




}
public class UpdateRequestRequestModel : CreateRequestRequestModel
{
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

}
