namespace Application.Dtos;

public class LikeDto
{
    public int NumberOfLikes { get; set; }
    public ICollection<CustomerDto> CustomerDto { get; set; }

}
  public class CreateLikeRequestModel
    {
        public int UserId { get; set; }
        public int ReviewId { get; set; }
    }
