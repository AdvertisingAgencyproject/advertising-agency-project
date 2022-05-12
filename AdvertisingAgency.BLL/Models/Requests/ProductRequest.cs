namespace AdvertisingAgency.BLL.Models.Requests;

public class ProductRequest
{
    public string Text { get; set; }
    public string Type { get; set; }
    public string Base64 { get; set; }
    public int Price { get; set; }
}