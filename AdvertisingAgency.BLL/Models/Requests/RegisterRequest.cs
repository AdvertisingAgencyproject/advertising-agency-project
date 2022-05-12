namespace AdvertisingAgency.BLL.Models.Requests;

public class RegisterRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string Fullname { get; set; }

    public RegisterRequest(string email, string password, string fullname)
    {
        Email = email;
        Password = password;
        Fullname = fullname;
    }

    public RegisterRequest() { }
}