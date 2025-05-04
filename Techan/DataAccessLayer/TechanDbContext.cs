using Microsoft.EntityFrameworkCore;
using Techan.Models;
namespace Techan.DataAccessLayer;

    public class TechanDbContext:DbContext
    {
        public DbSet<Slider> Sliders {  get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-2A1S4DT\\SQLEXPRESS;Database=Techan;Trusted_Connection=true;TrustServerCertificate=true");
            base.OnConfiguring(optionsBuilder);
        }
    }

