namespace WebAPI.dtos
{
   
    // vm = viewmodel, cai the hien thi ra ngoai

    public class ProductDto 
    {        
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

        public Byte Discount { get; set; }

        public int CategoryId { get; set; }
    }

    public class ProductVM
    {
        // id dung khi hien thi list san pham can co id truy cap vao product detail
        public Guid Id { get; set; }
        public string Name { get; set; }  
        public string Description { get; set; } 
        public double Price { get; set; }
        public Byte Discount { get; set; }
        public string CategoryName { get; set; }
    }
    
}
