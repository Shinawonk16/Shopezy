using Application.Dtos;

namespace Application.Abstractions.IServices;

public interface IRoleService
{
    Task<BaseResponse<RoleDto>> AddRoleAsync(CreateRoleRequestModel model);
    Task<BaseResponse<RoleDto>> UpdateRoleAsync(UpdateRoleRequestModel model,string id);
    Task<BaseResponse<RoleDto>> GetRoleByUserIdAsync(string userId);
}
