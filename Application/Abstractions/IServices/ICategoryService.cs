using Application.Dtos;

namespace Application.Abstractions.IServices;

public interface ICategoryService
{
    Task<BaseResponse<CategoryDto>> AddCategoryAsync(CreateCategoryRequestModel model);
    Task<BaseResponse<CategoryDto>> UpdateCategoryAsync(string id, UpdateCategoryRequestModel model);


}
