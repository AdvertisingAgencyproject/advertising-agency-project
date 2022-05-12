namespace AdvertisingAgency.DAL.Entities;

public class FavorOrder : BaseEntity
{
    public string UserId { get; set; }
    public string FavorId { get; set; }
    public int TotalPrice { get; set; }
    public DateTime Created { get; set; }
    public virtual User User { get; set; }
    public virtual Favor Favor { get; set; }
}