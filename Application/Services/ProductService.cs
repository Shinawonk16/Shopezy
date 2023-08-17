using Application.Abstractions;
using Application.Abstractions.IRepositories;
using Application.Abstractions.IServices;
using Application.Dtos;
using Domain.Entities;

namespace Application.Services;
public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IBrandRepository _brandRepository;
    private readonly IShopezyImage _image;

    public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository, IBrandRepository brandRepository, IShopezyImage image = null)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _brandRepository = brandRepository;
        _image = image;
    }



    public async Task<BaseResponse<ProductDto>> CreateProductAsync(AddProductRequestModel model)
    {
        var check = await _productRepository.GetAsync(x => x.ProductName == model.ProductName.ToLower());
        if (check != null)
        {
            var category = await _categoryRepository.GetCategoryAsync(x => x.CategoryName == model.CategoryName.ToLower());
            if (category == null)
            {
                return new BaseResponse<ProductDto>
                {
                    Message = $"{model.CategoryName} not found",
                    Status = false,
                };
            }
            var brand = await _brandRepository.GetBrand(x => x.BrandName == model.BrandName.ToLower());
            if (brand == null)
            {
                return new BaseResponse<ProductDto>
                {
                    Message = $"{model.BrandName} not found",
                    Status = false,
                };
            }
            var images = _image.UploadFiles(model.Image);
            var product = new Product
            {
                ProductName = model.ProductName.ToLower(),
                Description = model.Description,
                Price = model.Price,
                Quantity = model.Quantity,
                Image = images,
                CategoryId = category.Id,
                BrandId = brand.Id,

            };
            await _productRepository.CreateAsync(product);
            await _productRepository.SaveAsync();
            return new BaseResponse<ProductDto>
            {
                Message = "Successful added a product",
                Status = true,
                Data = new ProductDto
                {
                    ProductName = product.ProductName,
                    Description = product.Description,
                    Price = product.Price,
                    Image = product.Image,
                    Quantity = product.Quantity,
                    CategoryDto = new CategoryDto
                    {
                        CategoryName = category.CategoryName,
                        CategoryDescription = category.CategoryDescription
                    },
                    BrandsDto = new BrandsDto
                    {
                        BrandDescription = brand.BrandDescription,
                        BrandName = brand.BrandName
                    }
                }
            };
        }
        return new BaseResponse<ProductDto>
        {
            Message = $"{model.ProductName} not added",
            Status = false,

        };
    }

    public async Task<BaseResponse<IEnumerable<ProductDto>>> GetAllAsync()
    {
        var all = await _productRepository.GetAllProductAsync();
        if (all != null)
        {
            return new BaseResponse<IEnumerable<ProductDto>>
            {
                Message = "Product list found successfully",
                Status = true,
                Data = all.Select(x => new ProductDto
                {
                    ProductName = x.ProductName,
                    Description = x.Description,
                    Price = x.Price,
                    Image = x.Image,
                    CategoryDto = new CategoryDto
                    {
                        CategoryName = x.Category.CategoryName
                    },
                    BrandsDto = new BrandsDto
                    {
                        BrandName = x.Brand.BrandName
                    }

                })
            };
        }
        return new BaseResponse<IEnumerable<ProductDto>>
        {
            Message = "operation failed",
            Status = false,

        };
    }

    public async Task<BaseResponse<ProductDto>> GetAsync(string id)
    {
        var get = await _productRepository.GetAsync(x => x.Id == id);
        if (get == null)
        {
            return new BaseResponse<ProductDto>
            {
                Message = "product not found",
                Status = false,
            };
        }
        return new BaseResponse<ProductDto>
        {
            Message = $"{get.ProductName} found successfully",
            Status = true,
            Data = new ProductDto
            {
                ProductName = get.ProductName,
                Price = get.Price,
                Description = get.Description,
                Image = get.Image,
                CategoryDto = new CategoryDto
                {
                    CategoryName = get.Category.CategoryName,
                },
                BrandsDto = new BrandsDto
                {
                    BrandName = get.Brand.BrandName
                }


            }
        };
    }

    public Task<BaseResponse<ProductDto>> GetAvailableProductsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse<IEnumerable<ProductDto>>> GetByProductCategoryAsync(string categoryId)
    {
        var get = await _productRepository.GetProductByCategoryAsync(categoryId);
        if (get == null)
        {
            return new BaseResponse<IEnumerable<ProductDto>>
            {

            };
        }
        return new BaseResponse<IEnumerable<ProductDto>>
        {
            Message = "Product list found successfully",
            Status = true,
            Data = get.Select(x => new ProductDto
            {
                ProductName = x.ProductName,
                Description = x.Description,
                Price = x.Price,
                Image = x.Image,
                CategoryDto = new CategoryDto
                {
                    CategoryName = x.Category.CategoryName
                },
                BrandsDto = new BrandsDto
                {
                    BrandName = x.Brand.BrandName
                }

            })
        };
    }

    public async Task<BaseResponse<IEnumerable<ProductDto>>> GetProductsByPriceAsync(decimal price)
    {
        var get = await _productRepository.GetByPriceAsync(price);
        if (get == null)
        {
            return new BaseResponse<IEnumerable<ProductDto>>
            {

            };
        }
        return new BaseResponse<IEnumerable<ProductDto>>
        {
            Message = "Product list found successfully",
            Status = true,
            Data = get.Select(x => new ProductDto
            {
                ProductName = x.ProductName,
                Description = x.Description,
                Price = x.Price,
                Image = x.Image,
                CategoryDto = new CategoryDto
                {
                    CategoryName = x.Category.CategoryName
                },
                BrandsDto = new BrandsDto
                {
                    BrandName = x.Brand.BrandName
                }

            }).ToList()
        };
    }

    public async Task<BaseResponse<ProductDto>> UpdateProductAsync(string id, UpdateProductRequestModel model)
    {
        var update = await _productRepository.GetAsync(x => x.Id == id);
        if (update == null)
        {
            return new BaseResponse<ProductDto>
            {
                Message = "not found",
                Status = false
            };
        }

        var images = _image.UploadFiles(model.Image);
        var product = new Product
        {
            ProductName = model.ProductName.ToLower(),
            Description = model.Description,
            Price = model.Price,
            Quantity = model.Quantity,
            Image = images,

        };
        await _productRepository.UpdateAsync(product);
        await _productRepository.SaveAsync();
        return new BaseResponse<ProductDto>
        {
            Message = "Successful added a product",
            Status = true,
            Data = new ProductDto
            {
                ProductName = product.ProductName,
                Description = product.Description,
                Price = product.Price,
                Image = product.Image,
                Quantity = product.Quantity,

            }
        };

    }
}
