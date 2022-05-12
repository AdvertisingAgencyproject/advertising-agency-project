namespace AdvertisingAgency.BLL.Models.Responses;

public class ManagerResponse
{
    public string Email { get; set; }
    public string Password { get; set; }

    public ManagerResponse(string email, string password)
    {
        Email = email;
        Password = password;
    }

    public ManagerResponse()
    {
        
    }
}