using System.ComponentModel.DataAnnotations;
using Techan.Models;

namespace Techan.ViewModels.Products
{
    public class ProductCreateVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [Range(0, 100)]
        public byte Discount { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellPrice { get; set; }
        public IFormFile ImageFile { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public IEnumerable<IFormFile>? ImageFiles { get; set; }
    }
}
