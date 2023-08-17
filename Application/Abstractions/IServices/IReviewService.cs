using Application.Dtos;

namespace Application.Abstractions.IServices;

public interface IReviewService
{
    Task<BaseResponse<ReviewDto>> AddReviewAsync(CreateReviewRequestModel model);
    Task<BaseResponse<ICollection<ReviewDto>>> GetAllReviewAsync();
    Task<BaseResponse<ICollection<ReviewDto>>> GetAllReviewByProductIdAsync(string productId);
    Task<BaseResponse<ReviewDto>> UpdateReviewAsync(string id,UpdateReviewRequestModel model);


}
