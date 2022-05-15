namespace AdvertisingAgency.BLL.Models.Responses;

public class FavorOrderResponse
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public string FavorId { get; set; }
    public int TotalPrice { get; set; }
    public bool? IsFastOrder { get; set; }
    public DateTime Created { get; set; }
}