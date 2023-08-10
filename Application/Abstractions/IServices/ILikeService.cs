using Application.Dtos;

namespace Application.Abstractions.IServices;

public interface ILikeService
{
    Task<BaseResponse<LikeDto>> CreateLike(CreateLikeRequestModel model);
        Task<BaseResponse<LikeDto>> GetLikesByReviewIdAsync(int reveiwId);
}
