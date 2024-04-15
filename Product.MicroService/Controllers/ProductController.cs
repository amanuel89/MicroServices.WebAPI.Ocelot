using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.MicroService.Pocos;
using Products.MicroService.Interfaces;

namespace Product.MicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepo;

        public ProductController(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<StoreItem>>> GetAllProducts()
        {
            return await _productRepo.GetAllProducts();
        }
    }
}
