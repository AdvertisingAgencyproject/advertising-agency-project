namespace AdvertisingAgency.DAL.Entities;

public class Discount : BaseEntity
{
    public int Percents { get; set; }
    public string FavorId { get; set; }
    public virtual Favor Favor { get; set; }
}