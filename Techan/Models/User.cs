using Microsoft.AspNetCore.Identity;

namespace Techan.Models
{
    public class User:IdentityUser<Guid>
    {
        public string Fullname { get; set; } = null!;
        public string ImageUrl { get; set; } = "default.jpg";
    }
}
