using Domain.Enum;

namespace Application.Dtos;

public class UserDto
{
    public string FirstName { get; set; }
    public string UserName { get; set; }
    public string Id { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string ProfilePicture { get; set; }
    public string Password { get; set; }
    public Gender Gender { get; set; }
    public IEnumerable<RoleDto> RoleDtos { get; set; }
}
public class LoginUserRequsetModel
{

    public string Password { get; set; }
    public string Email { get; set; }
}
