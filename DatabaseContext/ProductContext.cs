using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProductApi.Entities.Products;

namespace ProductApi.DatabaseContext
{
    public class ProductContext : IdentityDbContext<IdentityUser>, IProductContext
    {
        public DbSet<Product> Products { get; set; }
        protected readonly IConfiguration Configuration;

        public ProductContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to mysql with connection string from app settings
            var connectionString = Configuration.GetConnectionString("ProductDatabase");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public async Task<int> SaveChanges()
        {
            return await base.SaveChangesAsync();
        }


    }
}
