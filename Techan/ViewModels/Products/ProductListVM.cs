using System.ComponentModel.DataAnnotations;

namespace Techan.ViewModels.Products
{
    public class ProductListVM
    {
        public int Id { get; set; }
        public string BrandName {  get; set; }
        public string ImageUrl {  get; set; }
        public string Name { get; set; }
        [Range(0, 100)]
        public byte Discount { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellPrice { get; set; }
        public string CategoryName {  get; set; }
        public bool IsDeleted {  get; set; }
    }
}
