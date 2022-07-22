using Microsoft.EntityFrameworkCore;
using ProductApi.Entities.Products;

namespace ProductApi.DatabaseContext
{
    public interface IProductContext
    {
        DbSet<Product> Products { get; set; }

        Task<int> SaveChanges();
    }
}