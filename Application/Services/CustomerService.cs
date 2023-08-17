using Application.Abstractions;
using Application.Abstractions.IRepositories;
using Application.Abstractions.IServices;
using Application.Dtos;
using Domain.Entities;

namespace Application.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IVerificationRepository _verificationRepository;
    private readonly IEmailService _emailService;
    private readonly IShopezyImage _imageRepository;


    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<BaseResponse<CustomerDto>> CreateCustomerAsync(CreateCustomerRequestModel model)
    {
        int code = new Random().Next(100000, 999999);
        var get = await _customerRepository.CheckIfEmailExistAsync(model.Email);
        if (get != false)
        {
            var image = _imageRepository.UploadFiles(model.ProfilePicture);
            var customer = new Customer
            {
                User = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = model.Password,
                    PhoneNumber = model.PhoneNumber,
                    ProfilePicture = image,
                    Gender = model.Gender,
                    UserName = $"{model.FirstName} {model.LastName}"

                }
            };
            var role = await _roleRepository.GetRoleAsync(x => x.RoleName == "customer");
            if (role == null)
            {
                return new BaseResponse<CustomerDto>
                {
                    Message = "Role not found",
                    Status = false
                };
            }
            var create = await _customerRepository.CreateAsync(customer);
            await _customerRepository.SaveAsync();
            var userRole = new UserRole
            {
                UserId = create.User.Id,
                RoleId = role.Id
            };
            create.User.UserRoles.Add(userRole);
            await _customerRepository.UpdateAsync(create);
            var verification = new Verification
            {
                Code = code,
                CustomerId = create.Id

            };
            await _verificationRepository.CreateAsync(verification);
            await _verificationRepository.SaveAsync();
            var mailRequest = new MailRequest
            {
                Subject = "Confirmation Code",
                ToEmail = customer.User.Email,
                ToName = $"{customer.User.FirstName} {customer.User.LastName}",
                HtmlContent = $"<html><body><h1>Hello {customer.User.FirstName} {customer.User.LastName}, Welcome to Shopezy...</h1><h4>Your confirmation code is {verification.Code} to continue with the registration</h4></body></html>",
            };
            _emailService.SendEMailAsync(mailRequest);

            return new BaseResponse<CustomerDto>
            {
                Message = "Added Successfully",
                Status = true,
                Data = new CustomerDto
                {
                    Users = new UserDto
                    {
                        FirstName = customer.User.FirstName,
                        LastName = customer.User.LastName,
                        Email = customer.User.Email,
                        Password = customer.User.Password,
                        PhoneNumber = customer.User.PhoneNumber,
                        ProfilePicture = customer.User.ProfilePicture,
                        Gender = customer.User.Gender,
                        UserName = $"{customer.User.FirstName} {customer.User.LastName}"
                    }
                }
            };
        }
        return new BaseResponse<CustomerDto>
        {
            Message = "Already exist",
            Status = false
        };
    }

    public async Task<BaseResponse<IEnumerable<CustomerDto>>> GetAllAsync()
    {
        var get = await _customerRepository.GetAllCustomerAsync();
        if (get != null)
        {
            return new BaseResponse<IEnumerable<CustomerDto>>
            {
                Message = "Successful",
                Status = true,
                Data = get.Select(x => new CustomerDto
                {
                    Users = new UserDto
                    {
                        UserName = $"{x.User.FirstName} {x.User.LastName}",
                        Email = x.User.Email,
                        Password = x.User.Password,
                        PhoneNumber = x.User.PhoneNumber,
                        ProfilePicture = x.User.ProfilePicture,
                        Gender = x.User.Gender
                    }
                }).ToList()
            };
        }
        return new BaseResponse<IEnumerable<CustomerDto>>
        {
            Message = "Failed",
            Status = false,

        };
    }

    public async Task<BaseResponse<CustomerDto>> GetByEmailAsync(string email)
    {
        var get = await _customerRepository.GetAsync(x => x.User.Email == email);
        if (get != null)
        {
            return new BaseResponse<CustomerDto>
            {
                Message = "Successful",
                Status = true,
                Data = new CustomerDto
                {
                    Users = new UserDto
                    {
                        UserName = $"{get.User.FirstName} {get.User.LastName}",
                        Email = get.User.Email,
                        Password = get.User.Password,
                        PhoneNumber = get.User.PhoneNumber,
                        ProfilePicture = get.User.ProfilePicture,
                        Gender = get.User.Gender
                    }
                }
            };
        }
        return new BaseResponse<CustomerDto>
        {
            Message = "Failed",
            Status = false,
        };
    }

    public async Task<BaseResponse<CustomerDto>> GetByIdAsync(string id)
    {
        var get = await _customerRepository.GetAsync(x => x.User.Id == id);
        if (get != null)
        {
            return new BaseResponse<CustomerDto>
            {
                Message = "Successful",
                Status = true,
                Data = new CustomerDto
                {
                    Users = new UserDto
                    {
                        UserName = $"{get.User.FirstName} {get.User.LastName}",
                        Email = get.User.Email,
                        Password = get.User.Password,
                        PhoneNumber = get.User.PhoneNumber,
                        ProfilePicture = get.User.ProfilePicture,
                        Gender = get.User.Gender
                    }
                }
            };
        }
        return new BaseResponse<CustomerDto>
        {
            Message = "Failed",
            Status = false,
        };
    }

    public Task<BaseResponse<IEnumerable<CustomerDto>>> GetSelectedAsync()
    {

        throw new NotImplementedException();
    }

    public async Task<BaseResponse<CustomerDto>> UpdateCustomerAsync(string id, UpdateCustomerRequestModel model)
    {
        var get = await _customerRepository.GetAsync(x => x.Id == id);
        if (get != null)
        {
            var image = _imageRepository.UploadFiles(model.ProfilePicture);
            var customer = new Customer
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
            await _customerRepository.UpdateAsync(customer);
            await _customerRepository.SaveAsync();
            return new BaseResponse<CustomerDto>
            {
                Message = "Updated Successfully",
                Status = true,
                Data = new CustomerDto
                {
                    Users = new UserDto
                    {
                        FirstName = customer.User.FirstName,
                        LastName = customer.User.LastName,
                        Email = customer.User.Email,
                        PhoneNumber = customer.User.PhoneNumber,
                        ProfilePicture = customer.User.ProfilePicture,
                        UserName = $"{customer.User.FirstName} {customer.User.LastName}"
                    }
                }
            };
        }
        return new BaseResponse<CustomerDto>
        {
            Message = "Failed",
            Status = false,
        };
    }
}






































































































