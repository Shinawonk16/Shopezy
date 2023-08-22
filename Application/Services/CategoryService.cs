using Application.Abstractions.IRepositories;
using Application.Abstractions.IServices;
using Application.Dtos;
using Domain.Entities;

namespace Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<BaseResponse<CategoryDto>> AddCategoryAsync(CreateCategoryRequestModel model)
    {
        var check = await _categoryRepository.GetCategoryAsync(x => x.CategoryName == model.CategoryDescription.ToLower());
        if (check != null)
        {
            var category = new Category
            {
                CategoryDescription = model.CategoryDescription,
                CategoryName = model.CategoryName,
                CreatedAt = model.CreatedAt,

            };
            await _categoryRepository.CreateAsync(category);
            await _categoryRepository.SaveAsync();
            return new BaseResponse<CategoryDto>
            {
                Message = "Successful",
                Status = true,
                Data = new CategoryDto
                {
                    CategoryDescription = category.CategoryDescription,
                    CategoryName = category.CategoryName,
                    CreatedAt = category.CreatedAt

                }
            };
        }
        return new BaseResponse<CategoryDto>
        {
            Message = "Failed",
            Status = false,

        };
    }

    public async Task<BaseResponse<IList<CategoryDto>>> GetAllCategory()
    {
        var check = await _categoryRepository.GetAllCategoryAsync();
        if (check != null)
        {

            return new BaseResponse<IList<CategoryDto>>
            {
                Message = "Successful",
                Status = true,
                Data = check.Select(x => new CategoryDto
                {
                    CategoryDescription = x.CategoryDescription,
                    CategoryName = x.CategoryName,
                    CreatedAt = x.CreatedAt
                }
                ).ToList()

            };
        }
        return new BaseResponse<IList<CategoryDto>>
        {
            Message = "Failed",
            Status = false,

        };
    }

    public async Task<BaseResponse<CategoryDto>> GetById(string id)
    {
        var check = await _categoryRepository.GetCategoryAsync(x => x.Id == id);
        if (check != null)
        {

            return new BaseResponse<CategoryDto>
            {
                Message = "Successful",
                Status = true,
                Data = new CategoryDto
                {
                    CategoryDescription = check.CategoryDescription,
                    CategoryName = check.CategoryName,
                    CreatedAt = check.CreatedAt

                }
            };
        }
        return new BaseResponse<CategoryDto>
        {
            Message = "Failed",
            Status = false,

        };
    }

    public async Task<BaseResponse<CategoryDto>> UpdateCategoryAsync(string id, UpdateCategoryRequestModel model)
    {
        var check = await _categoryRepository.GetCategoryAsync(x => x.Id == id);
        if (check != null)
        {
            var category = new Category
            {
                CategoryDescription = model.CategoryDescription,
                CategoryName = model.CategoryName,
                CreatedAt = model.CreatedAt,

            };
            await _categoryRepository.UpdateAsync(category);
            await _categoryRepository.SaveAsync();
            return new BaseResponse<CategoryDto>
            {
                Message = "Successful",
                Status = true,
                Data = new CategoryDto
                {
                    CategoryDescription = category.CategoryDescription,
                    CategoryName = category.CategoryName,
                    CreatedAt = category.CreatedAt

                }
            };
        }
        return new BaseResponse<CategoryDto>
        {
            Message = "Failed",
            Status = false,

        };
    }
}
