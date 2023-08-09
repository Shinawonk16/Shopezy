namespace Application.Dtos;
public class ReviewDto
{
    public string Comment { get; set; }
    public CustomerDto CustomerDto { get; set; }
    public string CustomerId { get; set; }
    public ProductDto ProductDto { get; set; }
}

public class CreateReviewRequestModel
{
    public string Comment { get; set; }
}


public class UpdateReviewRequestModel
{
    public string Comment { get; set; }
}
