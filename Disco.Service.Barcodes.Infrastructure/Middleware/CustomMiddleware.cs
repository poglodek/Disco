using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Disco.Service.Barcodes.Infrastructure.Middleware;

public class CustomMiddleware : IMiddleware
{
    private readonly ILogger<CustomMiddleware> _logger;

    public CustomMiddleware(ILogger<CustomMiddleware> logger)
    {
        _logger = logger;
    }
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            _logger.LogTrace($"Exception: {e.GetType().Name}, {e.Message}");
            
            var response =  ExceptionMapperToResponse.Map(e);
            
            context.Response.StatusCode = (int) response.Code;
            await context.Response.WriteAsync(JsonConvert.SerializeObject(response.Response));
        }
    }
}