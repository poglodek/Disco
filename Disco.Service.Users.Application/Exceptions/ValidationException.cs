using FluentValidation.Results;

namespace Disco.Service.Users.Application.Exceptions;

public class ValidationException : ApplicationException
{
    public List<ValidationFailure> Errors { get; }

    public ValidationException(object obj, List<ValidationFailure> errors) : base($"Validation error on {nameof(obj)} with errors: {string.Join(", ", errors)}")
    {
        Errors = errors;
    }
    
    public override string Code 
        => "validation_error";
}