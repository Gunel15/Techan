using System.ComponentModel.DataAnnotations;
using Techan.Models;

namespace Techan.ViewModels.Categories
{
    public class CategoryCreateVM
    {
        [MinLength(3), MaxLength(64)]
        public string Name { get; set; }
        public IEnumerable<Product>? Products { get; set; }
    }
}
