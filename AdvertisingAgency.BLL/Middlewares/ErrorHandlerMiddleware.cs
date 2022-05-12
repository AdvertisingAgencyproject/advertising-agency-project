using System.Net;
using AdvertisingAgency.BLL.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

namespace AdvertisingAgency.BLL.Middlewares;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (HttpException httpException)
        {
            context.Response.StatusCode = (int)httpException.StatusCode;
            var responseFeature = context.Features.Get<IHttpResponseFeature>();
            responseFeature.ReasonPhrase = httpException.Message;
        }
        catch (Exception exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var responseFeature = context.Features.Get<IHttpResponseFeature>();
            responseFeature.ReasonPhrase = exception.Message;
        }
    }
}