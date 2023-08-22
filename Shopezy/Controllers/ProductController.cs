using Application.Abstractions.IServices;

namespace Shopezy.Controllers
{
    public class ProductController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
    }
}