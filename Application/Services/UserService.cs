using Application.Abstractions;
using Application.Abstractions.IRepositories;
using Application.Abstractions.IServices;
using Application.Dtos;
using Domain.Entities;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IShopezyImage _uploadImages;


    public UserService(IUserRepository userRepository, IShopezyImage uploadImages = null)
    {
        _userRepository = userRepository;
        _uploadImages = uploadImages;
    }

    public Task<BaseResponse<UserDto>> GetUserByTokenAsync(string token)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse<UserDto>> GetUsersByRoleAsync(Role role)
    {
        var get = await _userRepository.GetUserByRoleAsync(role.Id);
        if (get != null)
        {
            return new BaseResponse<UserDto>
            {
                Message = "Found Successfully",
                Status = true,
                Data = new UserDto
                {
                    UserName = $"{get.User.FirstName} {get.User.LastName}",
                    Email = get.User.Email,
                    PhoneNumber = get.User.PhoneNumber,
                    Id = get.User.Id,
                    ProfilePicture = get.User.ProfilePicture,
                    Role = get.Role.RoleName
                }
            };
        }

        return new BaseResponse<UserDto>
        {
            Message = "Not Found",
            Status = false
        };

    }

    public async Task<BaseResponse<UserDto>> LoginUserAsync(LoginUserRequsetModel model)
    {
        var login = await _userRepository.LoginAsync(model.Email, model.Password);
        if (login != null)
        {
            // var user = new UserDto
            // {
            //     UserName = $"{login.User.FirstName}  {login.User.LastName}",
            //     Id = login.User.Id,
            //     Email = login.User.Email,
            //     Role = login.Role.RoleName
            // };
            // var images = await _uploadImages.UploadFiles(model.);
            return new BaseResponse<UserDto>
            {
                Message = "Login Successful",
                Status = true,
                Data = new UserDto
                {
                    UserName = $"{login.User.FirstName}  {login.User.LastName}",
                    Id = login.User.Id,
                    Email = login.User.Email,
                    PhoneNumber = login.User.PhoneNumber,
                    Role = login.Role.RoleName
                }
            };
        }
        return new BaseResponse<UserDto>
        {
            Message = "Incorrect Password or Email try again",
            Status = false,
        };
    }
}
