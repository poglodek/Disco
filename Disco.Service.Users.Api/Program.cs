using Disco.Service.Users.Application;
using Disco.Service.Users.Application.Commands;
using Disco.Service.Users.Application.Queries;
using Disco.Service.Users.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddAuthorization()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

MapEndpoints(app);

app.UseInfrastructure();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();


void MapEndpoints(WebApplication webApplication)
{
    webApplication.MapPost("AddUser", async (AddUser request, IMediator mediator) => { await mediator.Send(request); });
    webApplication.MapPut("VerifyUser", async (VerifyUser request, IMediator mediator) => { await mediator.Send(request); });
    webApplication.MapGet("User/{id:guid}", async (Guid id, IMediator mediator) =>
    {
        var user = await mediator.Send(new GetUserInformation(id));
        if (user is not null)
            return Results.Ok(user);
        
        return Results.NotFound();
    });
}