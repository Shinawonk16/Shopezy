using Application.Abstractions.IRepositories;
using Application.Abstractions.IServices;
using Application.Dtos;
using Domain.Entities;

namespace Application.Services;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;
    private readonly IUserRepository _userRepository;

    public RoleService(IRoleRepository roleRepository, IUserRepository userRepository)
    {
        _roleRepository = roleRepository;
        _userRepository = userRepository;
    }

    public async Task<BaseResponse<RoleDto>> AddRoleAsync(CreateRoleRequestModel model)
    {
        var get = await _roleRepository.GetRoleAsync(x => x.RoleName == model.RoleName.ToLower());
        if (get != null)
        {
            var role = new Role
            {
                RoleName = get.RoleName,
                RoleDescription = get.RoleDescription,
            };
            await _roleRepository.CreateAsync(role);
            await _roleRepository.SaveAsync();

            return new BaseResponse<RoleDto>
            {
                Message = "Added Successfully",
                Status = true,
                Data = new RoleDto
                {
                    RoleName = get.RoleName,
                    RoleDescription = get.RoleDescription,
                }
            };
            
        }
        return new BaseResponse<RoleDto>
        {
            Message = "Already exist",
            Status = false
        };
    }

    public async Task<BaseResponse<RoleDto>> GetRoleByUserIdAsync(User user)
    {
        var get = await _roleRepository.GetRoleAsync(user.Id);
        if (get != null)
        {
            return new BaseResponse<RoleDto>
            {
                Message = "Found Successfully",
                Status = true,
                Data = new RoleDto
                {
                    RoleDescription = get.RoleDescription,
                    RoleName = get.RoleName,
                }
            };
        }
        return new BaseResponse<RoleDto>
        {
            Message = "",
            Status = false,
        };
    }

    public async Task<BaseResponse<RoleDto>> UpdateRoleAsync(UpdateRoleRequestModel model, string id)
    {
        var get = await _roleRepository.GetRoleAsync(x => x.Id == id);
        if (get != null)
        {
            return new BaseResponse<RoleDto>
            {
                Message = "Update Successfully",
                Status = true,
                Data = new RoleDto
                {
                    RoleName = get.RoleName,
                    RoleDescription = get.RoleDescription,
                }
            };
        }
        return new BaseResponse<RoleDto>
        {
            Message = "operation failed",
            Status = false,

        };
    }
}
