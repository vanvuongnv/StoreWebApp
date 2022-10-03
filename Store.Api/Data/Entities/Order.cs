namespace Store.Api.Data.Entities
{
    public class Order
    {
        // primary key
        public int OrderId { get; set; }
        // foreign key
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public int OrderStatus { get; set; }
    }
}
