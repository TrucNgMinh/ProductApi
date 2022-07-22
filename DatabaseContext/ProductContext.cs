using Microsoft.EntityFrameworkCore;
using ProductApi.Entities.Products;

namespace ProductApi.DatabaseContext
{
    public class ProductContext : DbContext, IProductContext
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

        public async Task<int> SaveChanges()
        {
            return await base.SaveChangesAsync();
        }


    }
}
