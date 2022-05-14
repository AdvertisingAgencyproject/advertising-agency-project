namespace AdvertisingAgency.BLL.Models.Responses;

public class FavorResponse
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Type { get; set; }
    public string ImagePath { get; set; }
    public int? DiscountPercents { get; set; }
    public int Price { get; set; }
}