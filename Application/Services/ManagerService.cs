using Application.Abstractions;
using Application.Abstractions.IRepositories;
using Application.Abstractions.IServices;
using Application.Dtos;
using Domain.Entities;

namespace Application.Services;

public class ManagerService : IManagerService
{

    private readonly IManagerRepository _managerRepository;
    private readonly IEmailService _emailService;
    private readonly IShopezyImage _image;
    private readonly IRoleRepository _roleRepository;
    private readonly IVerificationRepository _verificationRepository;

    public ManagerService(IManagerRepository managerRepository, IEmailService emailService, IShopezyImage image, IRoleRepository roleRepository, IVerificationRepository verificationRepository)
    {
        _managerRepository = managerRepository;
        _emailService = emailService;
        _image = image;
        _roleRepository = roleRepository;
        _verificationRepository = verificationRepository;
    }

    public async Task<BaseResponse<ManagerDto>> CompleteRegistrationAsync(CompleteManagerRegistrationRequestModel model)
    {
        var valid = await _managerRepository.GetAsync(x => x.User.Email == model.Email);
        if (valid != null)
        {
            var image = _image.UploadFiles(model.ProfileImage);
            valid.User.UserName ??= $"{model.FirstName ?? valid.User.FirstName} {model.LastName ?? valid.User.LastName}";
            valid.User.FirstName = model.FirstName ?? valid.User.FirstName;
            valid.User.LastName = model.LastName ?? valid.User.LastName;
            valid.User.Email = model.Email ?? valid.User.Email;
            valid.User.PhoneNumber = model.PhoneNumber ?? valid.User.PhoneNumber;
            valid.User.ProfilePicture = image;
            valid.User.IsDeleted = false;
            valid.User.Password = model.Password;
            return new BaseResponse<ManagerDto>
            {
                Message = "Registration Completed Successfully",
                Status = true
            };

        }
        return new BaseResponse<ManagerDto>
        {
            Message = "Registration Failed",
            Status = false
        };

    }

    public async Task<BaseResponse<ManagerDto>> CreateManagersAsync(CreateManagerRequestModel model)
    {
        int code = new Random().Next(111111, 999999);
        var check = await _managerRepository.GetAsync(x => x.User.Email == model.Email);
        if (check != null)
        {
            // var image = _image.UploadFiles(model.ProfilePicture);
            var manager = new Managers
            {
                User = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = model.Password,
                    PhoneNumber = model.PhoneNumber,
                    // ProfilePicture = image,
                    Gender = model.Gender,
                    UserName = $"{model.FirstName} {model.LastName}"
                }
            };


            var role = await _roleRepository.GetRoleAsync(x => x.RoleName.ToLower() == model.RoleName.ToLower());
            if (role == null)
            {
                return new BaseResponse<ManagerDto>
                {
                    Message = " Not found",
                    Status = false
                };
            }
            var create = await _managerRepository.CreateAsync(manager);
            await _managerRepository.SaveAsync();
            var userRole = new UserRole
            {
                UserId = create.User.Id,
                RoleId = role.Id
            };
            create.User.UserRoles.Add(userRole);
            await _managerRepository.UpdateAsync(create);
            await _managerRepository.SaveAsync();
            var mailRequest = new MailRequest
            {
                Subject = "Complete Your Registration",
                ToEmail = manager.User.Email,
                ToName = $"{manager.User.FirstName} {manager.User.LastName}",
                // HtmlContent = $"<!DOCTYPE html><html><head><meta charset=\"utf-8\"><meta http-equiv=\"x-ua-compatible\" content=\"ie=edge\"><title>Email Confirmation</title><meta name=\"viewport\" content=\"width=device-width, initial-scale=1\"><style type=\"text/css\">@media screen {{@font-face {{font-family: 'Source Sans Pro';font-style: normal;font-weight: 400;src: local('Source Sans Pro Regular'), local('SourceSansPro-Regular'), url(https://fonts.gstatic.com/s/sourcesanspro/v10/ODelI1aHBYDBqgeIAH2zlBM0YzuT7MdOe03otPbuUS0.woff) format('woff');}}@font-face {{font-family: 'Source Sans Pro';font-style: normal;font-weight: 700;src: local('Source Sans Pro Bold'), local('SourceSansPro-Bold'), url(https://fonts.gstatic.com/s/sourcesanspro/v10/toadOcfmlt9b38dHJxOBGFkQc6VGVFSmCnC_l7QZG60.woff) format('woff');}}body,table,td,a {{-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;}}table,td {{mso-table-rspace: 0pt;mso-table-lspace: 0pt;}}img {{-ms-interpolation-mode: bicubic;}}a[x-apple-data-detectors] {{font-family: inherit !important;font-size: inherit !important;font-weight: inherit !important;line-height: inherit !important;color: inherit !important;text-decoration: none !important;}}div[style*=\"margin: 16px 0;\"] {{margin: 0 !important;}}body {{width: 100% !important;height: 100% !important;padding: 0 !important;margin: 0 !important;}}table {{border-collapse: collapse !important;}}a {{color: #1a82e2;}}img {{height: auto;line-height: 100%;text-decoration: none;border: 0;outline: none;}}</style></head><body style=\"background-color: #e9ecef;\"><div class=\"preheader\" style=\"display: none; max-width: 0; max-height: 0; overflow: hidden; font-size: 1px; line-height: 1px; color: #fff; opacity: 0;\">A preheader is the short summary text that follows the subject line when an email is viewed in the inbox.</div><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"><tr><td align=\"center\" bgcolor=\"#e9ecef\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\"><tr><td align=\"center\" valign=\"top\" style=\"padding: 36px 24px;\"><a href=\"https://sendgrid.com\" target=\"_blank\" style=\"display: inline-block;\"><img src=\"https://media.licdn.com/dms/image/C510BAQHtR8AdDc-aJg/company-logo_200_200/0/1519909536138?e=2147483647&v=beta&t=n-uF8UVHI5jdSuAZ61e6OVnV1n8PWocgp3lZ0igTpyg\" alt=\"Logo\" border=\"0\" width=\"100\" height=\"100\" style=\"display: block;border-radius: 50%;\"></a></td></tr></table></td></tr><tr><td align=\"center\" bgcolor=\"#e9ecef\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\"><tr><td align=\"left\" bgcolor=\"#ffffff\" style=\"padding: 36px 24px 0; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; border-top: 3px solid #d4dadf;\"><h2 style=\"margin: 0; font-size: 32px; font-weight: 700; letter-spacing: -1px; line-height: 48px;\">Confirm Your Email Address</h2></td></tr></table></td></tr><tr><td align=\"center\" bgcolor=\"#e9ecef\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\"><tr><td align=\"left\" bgcolor=\"#ffffff\" style=\"padding: 24px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 16px; line-height: 24px;\"><p style=\"margin: 0;\">Tap the button below to confirm your email address. If you didn't create an account with <strong>Dansnom</strong>, you can safely delete this email.</p></td></tr><tr><td align=\"left\" bgcolor=\"#ffffff\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"><tr><td align=\"center\" bgcolor=\"#ffffff\" style=\"padding: 12px;\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tr><td align=\"center\" bgcolor=\"#1a82e2\" style=\"border-radius: 6px;\"><a href=\"http://127.0.0.1:5501/FrontEnd/AdminFrontEnd/completeRegistration.html?token={create.Token}\" target=\"_blank\" style=\"display: inline-block; padding: 16px 36px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 16px; color: #ffffff; text-decoration: none; border-radius: 6px;\">Confirm</a></td></tr></table></td></tr></table></td></tr></td></tr></table><body></html>"
            };
            _emailService.SendEMailAsync(mailRequest);
            return new BaseResponse<ManagerDto>
            {
                Message = "",
                Status = true,
                Data = new ManagerDto
                {
                    UserDto = new UserDto
                    {
                        FirstName = manager.User.FirstName,
                        LastName = manager.User.LastName,
                        Email = manager.User.Email,
                        Password = manager.User.Password,
                        PhoneNumber = manager.User.PhoneNumber,
                        // ProfilePicture = manager.User.ProfilePicture,
                        Gender = manager.User.Gender,
                        UserName = $"{manager.User.FirstName} {manager.User.LastName}"
                    }
                }
            };
        }
        return new BaseResponse<ManagerDto>
        {
            Message = " Not found",
            Status = false
        };
    }

    // public async Task<BaseResponse<ManagerDto>> GetAllAsync()
    // {

    // }

    public async Task<BaseResponse<ManagerDto>> GetByIdAsync(string id)
    {
        var get = await _managerRepository.GetManagerAsync(id);
        if (get != null)
        {
            return new BaseResponse<ManagerDto>
            {
                Message = "Successful",
                Status = true,
                Data = new ManagerDto
                {
                    UserDto = new UserDto
                    {
                        UserName = $"{get.User.FirstName} {get.User.LastName}",
                        Email = get.User.Email,
                        Password = get.User.Password,
                        PhoneNumber = get.User.PhoneNumber,
                        ProfilePicture = get.User.ProfilePicture,
                        Gender = get.User.Gender,
                        Role = get.Role.RoleName,
                        RoleDescription = get.Role.RoleDescription
                    },
                }
            };
        }
        return new BaseResponse<ManagerDto>
        {
            Message = "Failed",
            Status = true,
        };
    }

    public async Task<BaseResponse<ManagerDto>> GetByRoleAsync(string roleId)
    {
        var role = await _managerRepository.GetManagerByRoleAsync(roleId);
        if (role != null)
        {
            return new BaseResponse<ManagerDto>
            {
                Message = "Successful",
                Status = true,
                Data = new ManagerDto
                {
                    UserDto = new UserDto
                    {
                        UserName = $"{role.User.FirstName} {role.User.LastName}",
                        Email = role.User.Email,
                        Password = role.User.Password,
                        PhoneNumber = role.User.PhoneNumber,
                        ProfilePicture = role.User.ProfilePicture,
                        Gender = role.User.Gender,
                        Role = role.Role.RoleName,
                        RoleDescription = role.Role.RoleDescription
                    },
                }
            };
        }
        return new BaseResponse<ManagerDto>
        {
            Message = "Failed",
            Status = true,
        };
    }

    public async Task<BaseResponse<IEnumerable<ManagerDto>>> GetSelectedAsync()
    {
        var all = await _managerRepository.GetAllManagerAsync();
        if (all != null)
        {
            return new BaseResponse<IEnumerable<ManagerDto>>
            {
                Message = "Successful",
                Status = true,
                Data = all.Select(x => new ManagerDto
                {
                    UserDto = new UserDto
                    {
                        UserName = $"{x.User.FirstName} {x.User.LastName}",
                        Email = x.User.Email,
                        Password = x.User.Password,
                        PhoneNumber = x.User.PhoneNumber,
                        ProfilePicture = x.User.ProfilePicture,
                        Gender = x.User.Gender,
                        Role = x.Role.RoleName,
                        RoleDescription = x.Role.RoleDescription

                    },


                }).ToList()
            };
        }
        return new BaseResponse<IEnumerable<ManagerDto>>
        {
            Message = "Failed",
            Status = false,
        };
    }

    public async Task<BaseResponse<ManagerDto>> UpdateManagerAsync(string id, UpdateManagerRequestModel model)
    {
        var update =  await _managerRepository.GetManagerAsync(id);
        if (update != null)
        {
            var image = _image.UploadFiles(model.ProfilePicture);
            var manager = new Managers
            {
                User = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    ProfilePicture = image,
                    UserName = $"{model.FirstName} {model.LastName}"

                }
            };
            await _managerRepository.UpdateAsync(manager);
            await _managerRepository.SaveAsync();
            return new BaseResponse<ManagerDto>
            {
                Message = "Updated Successfully",
                Status = true,
                Data = new ManagerDto
                {
                    UserDto = new UserDto
                    {
                        FirstName = manager.User.FirstName,
                        LastName = manager.User.LastName,
                        Email = manager.User.Email,
                        PhoneNumber = manager.User.PhoneNumber,
                        ProfilePicture = manager.User.ProfilePicture,
                        UserName = $"{manager.User.FirstName} {manager.User.LastName}"
                    }
                }
            };
        }
        return new BaseResponse<ManagerDto>{
            Message = "Update Failed",
                Status = false,
        };
    }
}
