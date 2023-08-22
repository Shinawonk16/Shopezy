using Application.Abstractions.IRepositories;
using Application.Abstractions.IServices;
using Application.Dtos;
using Domain.Entities;

namespace Application.Services;

public class BrandService : IBrandService
{
    private readonly IBrandRepository _brandRepository;

    public BrandService(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }

    public async Task<BaseResponse<BrandsDto>> AddBrandAsync(CreateBrandsRequestModel model)
    {
        var check = await _brandRepository.GetBrand(x => x.BrandName == model.BrandName.ToLower());
        if (check != null)
        {
            var brand = new Brand
            {
                BrandDescription = model.BrandDescription,
                BrandName = model.BrandName,
                CreatedAt = model.CreatedAt,

            };
            await _brandRepository.CreateAsync(brand);
            await _brandRepository.SaveAsync();
            return new BaseResponse<BrandsDto>
            {
                Message = "Successful",
                Status = true,
                Data = new BrandsDto
                {
                    BrandDescription = brand.BrandDescription,
                    BrandName = brand.BrandName,
                    CreatedAt = brand.CreatedAt

                }
            };
        }
        return new BaseResponse<BrandsDto>
        {
            Message = "Failed",
            Status = false,

        };

    }

    public async Task<BaseResponse<IList<BrandsDto>>> GetAllBrand()
    {
        var check = await _brandRepository.GetBrandsAsync();
        if (check != null)
        {

            return new BaseResponse<IList<BrandsDto>>
            {
                Message = "Successful",
                Status = true,
                Data = check.Select(x => new BrandsDto
                {
                    BrandDescription = x.BrandDescription,
                    BrandName = x.BrandName,
                    CreatedAt = x.CreatedAt

                }).ToList()

            };
        }
        return new BaseResponse<IList<BrandsDto>>
        {
            Message = "Failed",
            Status = false,
        };

    }

    public async Task<BaseResponse<BrandsDto>> GetById(string id)
    {
        var check = await _brandRepository.GetBrand(x => x.Id == id);
        if (check != null)
        {

            return new BaseResponse<BrandsDto>
            {
                Message = "Successful",
                Status = true,
                Data = new BrandsDto
                {
                    BrandDescription = check.BrandDescription,
                    BrandName = check.BrandName,
                    CreatedAt = check.CreatedAt

                }
            };
        }
        return new BaseResponse<BrandsDto>
        {
            Message = "Failed",
            Status = false,
        };

    }

    public async Task<BaseResponse<BrandsDto>> UpdateBrandAsync(string id, UpdateBrandsRequestModel model)
    {
        var check = await _brandRepository.GetBrand(x => x.Id == id);
        if (check != null)
        {
            var brand = new Brand
            {
                BrandDescription = model.BrandDescription,
                BrandName = model.BrandName,
                CreatedAt = model.CreatedAt,

            };
            await _brandRepository.UpdateAsync(brand);
            await _brandRepository.SaveAsync();
            return new BaseResponse<BrandsDto>
            {
                Message = "Successful",
                Status = true,
                Data = new BrandsDto
                {
                    BrandDescription = brand.BrandDescription,
                    BrandName = brand.BrandName,
                    CreatedAt = brand.CreatedAt

                }
            };
        }
        return new BaseResponse<BrandsDto>
        {
            Message = "Failed",
            Status = false,

        };
    }
}
