using Microsoft.EntityFrameworkCore;
using ProductMicroservice.Models;

namespace ProductMicroservice.DbContexts
{
    public class ProductMicroserviceDbContext : DbContext
    {
        public ProductMicroserviceDbContext(DbContextOptions<ProductMicroserviceDbContext> options) : base(options)
        {

        }


        public DbSet<Product> Products { get; set; }
    }

    
}
