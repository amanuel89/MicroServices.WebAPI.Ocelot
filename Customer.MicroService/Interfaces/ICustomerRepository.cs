using Customer.MicroService.Pocos;

namespace Customer.MicroService.Interfaces
{
    
    public interface ICustomerRepository
    {
        public Task<List<StoreCustomer>> GetAllCustomers();
    }
}
