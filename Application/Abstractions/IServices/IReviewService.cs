using Application.Dtos;

namespace Application.Abstractions.IServices;

public interface IReviewService
{
    Task<BaseResponse<ReviewDto>> AddReviewAsync(CreateReviewRequestModel model,string customerId);
    Task<BaseResponse<ICollection<ReviewDto>>> GetAllReviewAsync();
    Task<BaseResponse<ICollection<ReviewDto>>> GetAllUnSeenReviewAsync();
    Task<BaseResponse<ReviewDto>> GetAllReviewByIdAsync(string id);
    Task<BaseResponse<ICollection<ReviewDto>>> GetAllReviewByCustomerIdAsync(string customerId);
    Task<BaseResponse<ReviewDto>> UpdateReviewAsync(string id,UpdateReviewRequestModel model);

}
