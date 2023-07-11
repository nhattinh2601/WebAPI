namespace WebAPI.Data
{
    public class OrderDetail
    {
        public Guid product_id { get; set; }
        public Guid order_id { get; set; }
        public int count { get; set; }
        public double price { get; set; }
        public byte discount { get; set; }

        //relationship
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
