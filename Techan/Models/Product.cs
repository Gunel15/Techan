using System.ComponentModel.DataAnnotations;

namespace Techan.Models
{
    public class Product:BaseEntity
    {
        public string Name {  get; set; }
        public string Description { get; set; }
        [Range(0,100)]
        public byte Discount {  get; set; }
        public decimal CostPrice {  get; set; }
        public decimal SellPrice {  get; set; }
        public string ImageUrl {  get; set; }
        public int BrandId {  get; set; }
        public int CategoryId {  get; set; }
        public Category? Category { get; set; }
        public Brand? Brand {  get; set; }
        public ICollection<ProductImage>? Images {  get; set; }
    }
}
