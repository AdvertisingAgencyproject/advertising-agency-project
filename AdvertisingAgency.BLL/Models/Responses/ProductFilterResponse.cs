namespace AdvertisingAgency.BLL.Models.Responses;

public class ProductFilterResponse
{
    public int MinPrice { get; set; }
    public int MaxPrice { get; set; }
    public List<string> Types { get; set; }
}