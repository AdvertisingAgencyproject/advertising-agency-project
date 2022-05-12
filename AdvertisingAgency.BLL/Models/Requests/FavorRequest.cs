namespace AdvertisingAgency.BLL.Models.Requests;

public class FavorRequest
{
    public string Title { get; set; }
    public string Type { get; set; }
    public string Base64 { get; set; }
    public int Price { get; set; }
}