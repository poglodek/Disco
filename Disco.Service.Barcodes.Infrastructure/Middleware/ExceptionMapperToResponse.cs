using System.Net;
using Disco.Service.Barcodes.Core.Expcetions;
using Disco.Service.Barcodes.Infrastructure.Expcetions;

namespace Disco.Service.Barcodes.Infrastructure.Middleware;


public record ExceptionResponse(HttpStatusCode Code, object Response);

public static class ExceptionMapperToResponse
{
    public static ExceptionResponse Map(Exception exception)
        => exception switch
        {
            Application.Exception.ApplicationException ex => new ExceptionResponse(HttpStatusCode.BadRequest,
                GetCode(ex)),
            DomainException ex => new ExceptionResponse(HttpStatusCode.BadRequest, GetCode(ex)),
            InfrastructureException ex => new ExceptionResponse(HttpStatusCode.BadRequest, GetCode(ex)),
            _ => new ExceptionResponse(HttpStatusCode.BadRequest, "There was an error."),
        };

    private static string GetCode(Exception ex)
    {
        return  ex switch
        {
            DomainException domainException when !string.IsNullOrWhiteSpace(domainException.Code) => domainException
                .Code,
            Application.Exception.ApplicationException appException when !string.IsNullOrWhiteSpace(appException.Code) => appException.Code,
             InfrastructureException appException when !string.IsNullOrWhiteSpace(appException.Code) => appException.Code,
             _ => "There was an error."
        };
    }
}