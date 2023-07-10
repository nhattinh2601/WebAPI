namespace WebAPI.Models
{

    public class ProductVM
    {
        public String name { get; set; }
        public double price { get; set; }

    }
    // vm = viewmodel, cai the hien thi ra ngoai

    public class Product : ProductVM
    {
        public Guid id { get; set; }
    }
}
