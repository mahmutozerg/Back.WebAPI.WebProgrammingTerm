using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Models;
using WebProgrammingTerm.Repository.Configurations;

namespace WebProgrammingTerm.Repository;

public class AppDbContext:DbContext
{
    public DbSet<Company> Companies { get; set; }
    public DbSet<Depot> Depots { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductDetail> ProductDetails { get; set; }
    public DbSet<AppUser>Users{ get; set; }
    public DbSet<UserComments>UserComments{ get; set; }
    public DbSet<UserFavorites>UserFavorites{ get; set; }
    public DbSet<CompanyUser>CompanyUser{ get; set; }
    

    public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
    {   
        //fill it
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());
        modelBuilder.ApplyConfiguration(new ProductDetailConfiguration());
        modelBuilder.ApplyConfiguration(new CompanyUserConfiguration());
        modelBuilder.ApplyConfiguration(new UserCommentsConfiguration());
        modelBuilder.ApplyConfiguration(new UserFavoritesConfiguration());



        
        base.OnModelCreating(modelBuilder);
    }

    
}