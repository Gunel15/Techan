using System.ComponentModel.DataAnnotations;

namespace Techan.Models
{
    public class Category:BaseEntity
    {
        [MinLength(3),MaxLength(64)]
        public string Name {  get; set; }
        public IEnumerable<Product>? Products { get; set; }
    }
}
