namespace AdvertisingAgency.DAL.Entities;

public class Product : BaseEntity
{
    public string Text { get; set; }
    public string Type { get; set; }
    public string ImagePath { get; set; }
    public int Price { get; set; }
    
    public virtual ICollection<ProductOrder> ProductOrders { get; set; }
}