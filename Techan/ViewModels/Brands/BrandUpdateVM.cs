using System.ComponentModel.DataAnnotations;

namespace Techan.ViewModels.Brands
{
    public class BrandUpdateVM
    {
        [MinLength(3)]
        public string Name {  get; set; }
        public IFormFile? ImageFile {  get; set; }
        public string? ImageUrl {  get; set; }
    }
}
