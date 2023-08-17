using Application.Dtos;

namespace Application.Abstractions.IServices;

public interface ILikeService
{
    Task<BaseResponse<LikeDto>> CreateLike(CreateLikeRequestModel model);
    Task<BaseResponse<LikeDto>> UpdateLike(string reveiwId, string customerId);

    Task<BaseResponse<LikeDto>> GetLikesByReviewIdAsync(string reveiwId);
}
