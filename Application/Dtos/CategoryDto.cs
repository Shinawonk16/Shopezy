namespace Application.Dtos;

public class CategoryDto
    {
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public List<ProductDto> ProductDtos { get; set; }
    }
    public class CreateCategoryRequestModel
    {
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
    }

    public class UpdateCategoryRequestModel:CreateCategoryRequestModel
    {
       
    }
