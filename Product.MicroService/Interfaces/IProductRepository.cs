
using Product.MicroService;
using Product.MicroService.Pocos;

namespace Products.MicroService.Interfaces
{
    public interface IProductRepository
    {
        public Task<List<StoreItem>> GetAllProducts();
    }
}
