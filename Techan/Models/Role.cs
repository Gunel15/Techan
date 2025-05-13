using Microsoft.AspNetCore.Identity;

namespace Techan.Models
{
    public class Role:IdentityRole<Guid>
    {
        public Role()
        {
            
        }

        public Role(string name):base(name) 
        {
            
        }
    }
}
