using Application.Dtos;
using Domain.Entities;

namespace Application.Abstractions.IServices;

public interface IUserService
{
    Task<BaseResponse<UserDto>> LoginUserAsync(LoginUserRequsetModel model);
    Task<BaseResponse<UserDto>> GetUsersByRoleAsync(Role role);
    Task<BaseResponse<UserDto>> GetUserByTokenAsync(string token);
}
