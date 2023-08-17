using Application.Dtos;

namespace Application.Abstractions.IServices;

public interface IRequestService
{
    public Task<BaseResponse<RequestDto>> CreateRequestAsync(CreateRequestRequestModel model);
    public Task<BaseResponse<RequestDto>> GetByIdAsync(string id);
    public Task<BaseResponse<RequestDto>> GetAsync();
    public Task<BaseResponse<RequestDto>> UpdateRequestAsync(string id, UpdateRequestRequestModel model);
    public Task<BaseResponse<IEnumerable<RequestDto>>> GetAllAsync();
    public Task<BaseResponse<IEnumerable<RequestDto>>> GetSelectedAsync();
}
