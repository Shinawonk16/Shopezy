using Application.Dtos;

namespace Application.Abstractions.IServices;

public interface IVerificationService
{
    Task<BaseResponse<VerificationDto>> UpdateVeryficationCodeAsync(string id);
    Task<BaseResponse<VerificationDto>> VerifyCode(string id, string verificationcode);
    Task<BaseResponse<VerificationDto>> SendForgetPasswordVerificationCode(ResetPasswordRequestModel model);

}
