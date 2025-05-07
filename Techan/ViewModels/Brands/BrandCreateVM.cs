using System.ComponentModel.DataAnnotations;

namespace Techan.ViewModels.Brands
{
    public class BrandCreateVM
    {
        [MinLength(3)]
        public string Name { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
