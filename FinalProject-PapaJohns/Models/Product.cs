namespace FinalProject_PapaJohns.Models
{
    public class Product:BaseEntity
    {
        //one
        public string Name { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
        public string ImageUrl { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }


        public List<SaleProduct> SaleProducts { get; set; }

    }


}
