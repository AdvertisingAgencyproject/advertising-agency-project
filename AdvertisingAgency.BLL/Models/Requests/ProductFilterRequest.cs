namespace AdvertisingAgency.BLL.Models.Requests;

public class ProductFilterRequest
{
    public string? SearchQuery { get; set; }
    public int? MinPrice { get; set; } = 0;
    public int? MaxPrice { get; set; } = 9999;
}