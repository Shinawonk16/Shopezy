using Application.Abstractions.IServices;
using Application.Dtos;

namespace Application.Services;

public class ReviewService : IReviewService
{
    public Task<BaseResponse<ReviewDto>> AddReviewAsync(CreateReviewRequestModel model)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse<ICollection<ReviewDto>>> GetAllReviewAsync()
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse<ICollection<ReviewDto>>> GetAllReviewByProductIdAsync(string productId)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse<ReviewDto>> UpdateReviewAsync(string id, UpdateReviewRequestModel model)
    {
        throw new NotImplementedException();
    }
}
