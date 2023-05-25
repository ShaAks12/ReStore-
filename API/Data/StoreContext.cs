using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class StoreContext : DbContext 
    {
        public StoreContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Product> Products{get;set;} //Dbset for creating a table
        public DbSet<Basket> Baskets{get;set;} //Dbset for creating a table
        
    }
}