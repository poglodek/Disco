using FluentValidation;

namespace Disco.Service.Users.Application.Commands.CommandValidators;

public class AddUserHandlerValidator : AbstractValidator<AddUser>
{
    public AddUserHandlerValidator()
    {
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Password).MinimumLength(7).MaximumLength(30);
    }
}