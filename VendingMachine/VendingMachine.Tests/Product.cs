namespace VendingMachine.Tests
{
    public class Product
    {
        public Product()
        {
            ProductId = 1;
        }

        public int ProductId { get; set; }
        public decimal Price { get; set; }
    }
}