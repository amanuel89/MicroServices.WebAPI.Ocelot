using Customer.MicroService.Interfaces;

namespace Customer.MicroService.Pocos
{
    public class CustomerRepository : ICustomerRepository
    {
        public readonly List<StoreCustomer> _customers = new List<StoreCustomer>();
        public CustomerRepository()
        {
            _customers.Add(new StoreCustomer()
            {
                Id = new Guid(),
                Address = "Oba 1 ",
                Email = "felix@gmail.com",
                FirstName = "Felix",
                LastName = "Obakeree"
            });
            _customers.Add(new StoreCustomer()
            {
                Id = new Guid(),
                Address = "Ijoye 3",
                Email = "idris@yahoo.com",
                FirstName = "isris",
                LastName = "Obanla"
            });

        }

   
         public Task<List<StoreCustomer>> GetAllCustomers()
        {
            return Task.FromResult(_customers);
        }
    }
}
