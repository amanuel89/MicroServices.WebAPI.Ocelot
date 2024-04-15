namespace Product.MicroService.Pocos
{
    public class StoreItem
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int QuantityIn_tock { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
