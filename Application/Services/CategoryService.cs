using Application.Abstractions.IServices;
using Application.Dtos;

namespace Application.Services;

public class CategoryService : ICategoryService
{
    public Task<BaseResponse<CategoryDto>> AddCategoryAsync(CreateCategoryRequestModel model)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse<CategoryDto>> UpdateCategoryAsync(string id, UpdateCategoryRequestModel model)
    {
        throw new NotImplementedException();
    }
}
