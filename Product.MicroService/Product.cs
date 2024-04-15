namespace Product.MicroService
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int QuantityIn_tock { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
