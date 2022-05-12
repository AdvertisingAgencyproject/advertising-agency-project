namespace AdvertisingAgency.DAL.Entities;

public class User : BaseEntity
{
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string FullName { get; set; }
    public string Role { get; set; }
    public virtual ICollection<ProductOrder> ProductOrders { get; set; }
    public virtual ICollection<FavorOrder> FavorOrders { get; set; }
}