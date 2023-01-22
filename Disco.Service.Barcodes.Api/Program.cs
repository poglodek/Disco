using Disco.Service.Barcodes.Application;
using Disco.Service.Barcodes.Application.Events;
using Disco.Service.Barcodes.Infrastructure;
using Disco.Shared.Auth;
using MediatR;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
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

app.MapGet("GetUserIdByBarcode/{id:long}", async (long id, IMediator mediator) =>
{
    var user = await mediator.Send(new GetUserIdByBarCode(id));
    return Results.Ok(user);
}).RequireAuthorization();

app.MapGet("GetUsersBarcode/{id:Guid}", async (Guid id, IMediator mediator) =>
{
    var user = await mediator.Send(new GetUsersBarcode(id));
    return Results.Ok(user);
}).RequireAuthorization(x =>
{
    x.RequireRole(Roles.Company.ToString());
});

await app.RunAsync();
public partial class Program {}