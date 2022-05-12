namespace AdvertisingAgency.BLL.Models.Requests;

public class FavorUpdateRequest
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Type { get; set; }
    public string ImagePath { get; set; }
    public int Price { get; set; }
}