namespace Store.Api.Data.Entities
{
    public class Product
    {
        // primary key
        public int ProductId { get; set; }
        // foreign key
        public int CategoryId { get; set; }
        public string? ProductName { get; set; }
        public double Price { get; set; }
        public int UnitsInStock { get; set; }

    }
}
