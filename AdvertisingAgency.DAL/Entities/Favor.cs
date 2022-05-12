namespace AdvertisingAgency.DAL.Entities;

public class Favor : BaseEntity
{
    public string Title { get; set; }
    public string Type { get; set; }
    public string ImagePath { get; set; }
    public int Price { get; set; }
    public virtual ICollection<FavorOrder> FavorOrders { get; set; }
}