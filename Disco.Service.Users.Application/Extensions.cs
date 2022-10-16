using Disco.Service.Users.Application.Commands;
using Disco.Service.Users.Application.Commands.CommandValidators;
using Disco.Service.Users.Core.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Disco.Service.Users.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
        => services
            .AddMediatR(AppDomain.CurrentDomain.GetAssemblies())
            .AddScoped<IPasswordHasher<User>, PasswordHasher<User>>()
            .AddValidators();



    private static IServiceCollection AddValidators(this IServiceCollection services)
        => services
            .AddScoped<IValidator<AddUser>,AddUserHandlerValidator>();
    
    
   
}