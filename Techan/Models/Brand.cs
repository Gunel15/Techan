namespace Techan.Models
{
    public class Brand:BaseEntity
    {
        public string Name { get; set; }
        public string ImageUrl {  get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
