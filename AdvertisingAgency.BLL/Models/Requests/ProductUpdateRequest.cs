namespace AdvertisingAgency.BLL.Models.Requests;

public class ProductUpdateRequest
{
    public string Id { get; set; }
    public string ImagePath { get; set; }
    public string Text { get; set; }
    public string Type { get; set; }
    public int Price { get; set; }
}