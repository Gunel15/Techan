using System.ComponentModel.DataAnnotations;

namespace Techan.ViewModels.Sliders
{
    public class SliderUpdateVM
    {
        public int Id {  get; set; }
        public IFormFile? ImageFile { get; set; }
        public string? ImageUrl {  get; set; }
        [MinLength(5)]
        public string LittleTitle { get; set; }
        [MinLength(5)]
        public string Title { get; set; }
        [MinLength(5,ErrorMessage ="Basliq 5-den uzun olmalidir"),MaxLength(50)]
        public string BigTitle { get; set; }
        [MinLength(10)]
        public string Offer { get; set; }
        public string Link { get; set; }
    }
}
