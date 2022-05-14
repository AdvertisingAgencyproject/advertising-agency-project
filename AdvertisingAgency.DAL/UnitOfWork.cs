using AdvertisingAgency.DAL.Entities;
using AdvertisingAgency.DAL.Interfaces;
using AdvertisingAgency.DAL.Repositories;

namespace AdvertisingAgency.DAL;

public class UnitOfWork : IUnitOfWork
{
    public IRepository<User> UserRepository { get; private set; }
    public IRepository<Product> ProductRepository { get; private set; }
    public IRepository<Favor> FavorRepository { get; private set; }
    public IRepository<ProductOrder> ProductOrderRepository { get; private set; }
    public IRepository<FavorOrder> FavorOrderRepository { get; private set; }
    public IRepository<Discount> DiscountRepository { get; private set; }

    public UnitOfWork(DataContext context)
    {
        UserRepository = new GenericRepository<User>(context);
        ProductRepository = new GenericRepository<Product>(context);
        FavorRepository = new GenericRepository<Favor>(context);
        ProductOrderRepository = new GenericRepository<ProductOrder>(context);
        FavorOrderRepository = new GenericRepository<FavorOrder>(context);
        DiscountRepository = new GenericRepository<Discount>(context);
    }
}