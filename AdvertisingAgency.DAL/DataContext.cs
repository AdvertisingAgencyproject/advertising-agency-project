using AdvertisingAgency.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdvertisingAgency.DAL;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<Favor> Favors { get; set; }
    public virtual DbSet<ProductOrder> ProductOrders { get; set; }
    public virtual DbSet<FavorOrder> FavorOrders { get; set; }
    public virtual DbSet<Discount> Discounts { get; set; }
}