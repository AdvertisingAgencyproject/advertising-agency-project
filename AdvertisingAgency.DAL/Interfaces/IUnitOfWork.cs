using AdvertisingAgency.DAL.Entities;

namespace AdvertisingAgency.DAL.Interfaces;

public interface IUnitOfWork
{
    IRepository<User> UserRepository { get; }
    IRepository<Product> ProductRepository { get; }
    IRepository<Favor> FavorRepository { get; }
    IRepository<ProductOrder> ProductOrderRepository { get; }
    IRepository<FavorOrder> FavorOrderRepository { get; }
}