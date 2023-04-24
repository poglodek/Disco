using System.Net;
using Disco.Service.Discounts.Application.Exceptions;
using Disco.Service.Discounts.Core.Exceptions;

namespace Disco.Service.Discounts.Infrastructure.Exceptions;


public record ExceptionResponse(HttpStatusCode Code, object Response);

public static class ExceptionMapperToResponse
{
    public static ExceptionResponse Map(Exception exception)
        => exception switch
        {
            AppException ex => new ExceptionResponse(HttpStatusCode.BadRequest,
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
            AppException appException when !string.IsNullOrWhiteSpace(appException.Code) => appException.Code,
            InfrastructureException appException when !string.IsNullOrWhiteSpace(appException.Code) => appException.Code,
            _ => "There was an error."
        };
    }
}