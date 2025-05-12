using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using Techan.Models;
namespace Techan.DataAccessLayer;

    public class TechanDbContext:IdentityDbContext<User,IdentityRole<Guid>,Guid>
    {
    public TechanDbContext(DbContextOptions opt):base(opt)
    {
        
    }
    public DbSet<Slider> Sliders {  get; set; }
    public DbSet<ProductImage> ProductImages {  get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Brand> Brands { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=DESKTOP-2A1S4DT\\SQLEXPRESS;Database=Techan;Trusted_Connection=true;TrustServerCertificate=true");
        //    base.OnConfiguring(optionsBuilder);
        //}
    }

