namespace AdvertisingAgency.DAL.Entities;

public class ProductOrder : BaseEntity
{
    public string UserId { get; set; }
    public string ProductId { get; set; }
    public int Count { get; set; }
    public int TotalPrice { get; set; }
    public string Text { get; set; }
    public DateTime Created { get; set; }
    public virtual User User { get; set; }
    public virtual Product Product { get; set; }
}