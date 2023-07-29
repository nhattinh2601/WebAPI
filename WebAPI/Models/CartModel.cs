namespace WebAPI.Models
{
    public class CartModel
    {
        public Guid product_id { get; set; }
        public string product_name { get; set; }        
        public double price { get; set; }
        public int amount { get; set; }
        public double total_cost => amount * price;
    }
}
