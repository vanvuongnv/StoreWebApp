namespace Store.Api.Data.Entities
{
    public class OrderDetail
    {
        // foreign key
        public int OrderId { get; set; }
        // foreign key
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
    }
}
