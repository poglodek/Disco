using System.Net;
using System.Net.Mime;
using Disco.Service.Points.Application.Exceptions;
using Disco.Service.Points.Core.Exceptions;
using Disco.Service.Users.Infrastructure.Exceptions;

namespace Disco.Service.Points.Infrastructure.Exceptions;


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