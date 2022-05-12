namespace AdvertisingAgency.BLL.Models.Responses;

public class AuthResponse
{
    public string Token { get; set; }

    public AuthResponse(string token)
    {
        Token = token;
    }

    public AuthResponse() { }
}