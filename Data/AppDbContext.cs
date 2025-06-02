using Microsoft.EntityFrameworkCore;

namespace Asp.net_Web_Api.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                        .Property(p => p.Id)
                        .ValueGeneratedOnAdd();
            modelBuilder.Entity<Product>().ToTable("Products")
                        .HasKey(x => x.Id);
            modelBuilder.Entity<Product>()
                        .Property(p => p.sku)
                        .HasColumnName("Sku");

        }
    }
}
