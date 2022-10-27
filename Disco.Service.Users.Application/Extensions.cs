using Disco.Service.Users.Application.Commands;
using Disco.Service.Users.Application.Commands.CommandValidators;
using Disco.Service.Users.Core.Entities;
using Disco.Shared.Auth;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Disco.Service.Users.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        => services
            .AddMediatR(AppDomain.CurrentDomain.GetAssemblies())
            .AddScoped<IPasswordHasher<User>, PasswordHasher<User>>()
            .AddAuth(configuration)
            .AddValidators();



    private static IServiceCollection AddValidators(this IServiceCollection services)
        => services
            .AddScoped<IValidator<AddUser>,AddUserHandlerValidator>();
    
    
   
}