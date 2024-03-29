using Disco.Service.Users.Application;
using Disco.Service.Users.Application.Commands;
using Disco.Service.Users.Application.Queries;
using Disco.Service.Users.Infrastructure;
using Disco.Shared.Rabbit;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddAuthorization()
    .AddApplication(builder.Configuration)
    .AddInfrastructure(builder.Configuration);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseInfrastructure();

app.UseAuthorization();

MapEndpoints(app);

await app.RunAsync();


void MapEndpoints(WebApplication webApplication)
{
    webApplication.MapPost("AddUser", async (AddUser request, IMediator mediator) => { await mediator.Send(request); });
    webApplication.MapPut("VerifyUser", async (VerifyUser request, IMediator mediator) => { await mediator.Send(request); }).RequireAuthorization();
    webApplication.MapGet("Get/{id:guid}", async (Guid id, IMediator mediator) =>
    {
        var user = await mediator.Send(new GetUserInformation(id));
        return Results.Ok(user);
    }).RequireAuthorization();
    
    webApplication.MapPost("login", async (UserLoginRequest user, IMediator mediator) =>
    {
        var token = await mediator.Send(user);
        return Results.Ok(token);
    });
    
    webApplication.MapDelete("delete/{id:guid}", async (Guid id, IMediator mediator) =>
    {
        await mediator.Send(new DeleteUser(id));
        return Results.Ok();
    }).RequireAuthorization(x =>
    {
        x.RequireRole("Admin");
    });
    
}
// ReSharper disable once UnusedType.Global
public partial class Program{}