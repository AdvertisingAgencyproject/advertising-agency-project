namespace AdvertisingAgency.BLL.Models.Requests;

public class ProductOrderRequest
{
    public string UserId { get; set; }
    public string ProductId { get; set; }
    public int Count { get; set; }
    public string Text { get; set; }
}