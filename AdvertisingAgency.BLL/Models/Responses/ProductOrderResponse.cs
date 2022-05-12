namespace AdvertisingAgency.BLL.Models.Responses;

public class ProductOrderResponse
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public string ProductId { get; set; }
    public int Count { get; set; }
    public int TotalPrice { get; set; }
    public string Text { get; set; }
    public DateTime Created { get; set; }
}