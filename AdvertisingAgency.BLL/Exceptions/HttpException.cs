using System.Net;

namespace AdvertisingAgency.BLL.Exceptions;

public class HttpException : Exception
{
    public HttpStatusCode StatusCode { get; private set; }

    public HttpException(HttpStatusCode statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }
}