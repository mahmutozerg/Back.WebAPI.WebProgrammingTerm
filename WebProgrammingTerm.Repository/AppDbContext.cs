using System.Reflection;
using Microsoft.EntityFrameworkCore;
using WebProgrammingTerm.Core.Models;

namespace WebProgrammingTerm.Repository;

public class AppDbContext:DbContext
{
    public DbSet<Company> Companies { get; set; }

    public DbSet<Depot> Depots { get; set; }
    public DbSet<Images> Images { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductDetail> ProductDetails { get; set; }
    public DbSet<User>Users{ get; set; }
    public DbSet<UserComments>UserComments{ get; set; }
    public DbSet<UserFavorites>UserFavorites{ get; set; }
    

    public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
    {   
        //fill it
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.Entity<User>(
                u =>
                {
                    u.HasOne<Company>(u => u.Company).WithMany(c => c.Users);
                });

        
    }

    
}