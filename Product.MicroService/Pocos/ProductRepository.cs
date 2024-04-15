using Products.MicroService.Interfaces;

namespace Product.MicroService.Pocos
{
    public class ProductRepository : IProductRepository
    {
        public readonly List<StoreItem> _items = new List<StoreItem>();

        public ProductRepository()
        {
            _items.Add(new StoreItem()
           {
               Id = Guid.NewGuid(), 
               Name ="",
               Code = "",
               QuantityIn_tock = 200,
               UnitPrice    = 50

           });
            _items.Add(new StoreItem()
            {
                Id = Guid.NewGuid(),
                Name = "",
                Code = "",
                QuantityIn_tock = 200,
                UnitPrice = 50

            });
        }
        public Task<List<StoreItem>> GetAllProducts()
        {
            return Task.FromResult(_items);
        }
    }
}
