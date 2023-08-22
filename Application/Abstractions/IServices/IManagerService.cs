using Application.Dtos;
using Domain.Entities;

namespace Application.Abstractions.IServices;

public interface IManagerService
{
    Task<BaseResponse<ManagerDto>> CreateManagersAsync(CreateManagerRequestModel model);
    Task<BaseResponse<ManagerDto>> CompleteRegistrationAsync(CompleteManagerRegistrationRequestModel model);

    Task<BaseResponse<ManagerDto>> UpdateManagerAsync(string id, UpdateManagerRequestModel model);
    // Task<BaseResponse<ManagerDto>> GetAllAsync();
    Task<BaseResponse<ManagerDto>> GetByIdAsync(string id);
    Task<BaseResponse<ManagerDto>> GetByRoleAsync(string roleId);
    Task<BaseResponse<IEnumerable<ManagerDto>>> GetSelectedAsync();
}
