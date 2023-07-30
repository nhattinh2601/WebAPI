namespace WebAPI.dtos
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

    public class ProductModel
    {
        public Guid id { get; set; }
        public string name { get; set; }  
        public double price { get; set; }

        public string category_name { get; set; }
    }
}
