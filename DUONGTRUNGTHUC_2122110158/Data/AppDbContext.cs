using Microsoft.EntityFrameworkCore;
using DUONGTRUNGTHUC_2122110158.Model;

namespace DUONGTRUNGTHUC_2122110158.Data
{
    
    public class AppDbContext : DbContext
    {
     
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

       
        public DbSet<Product> Products { get; set; } 
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
