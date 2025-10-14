
using Microsoft.EntityFrameworkCore;
using MVC_Project.Models;

namespace MVC_Project.Data
{
    public class ApplicationDbContext:DbContext
    {
        public DbSet<Category> Categories {  get; set; }

        public DbSet<Product> Products { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("server=localhost;Database=MVC-Project;Trusted_Connection=true;TrustServerCertificate=true");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
                new Category { Id=1 , Name="Mobiles"},
                new Category { Id=2 , Name="Taplets"},
                new Category { Id=3 , Name="Labtops"}
                ); 
        }
    }
}
