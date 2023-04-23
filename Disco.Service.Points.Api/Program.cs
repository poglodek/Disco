using Disco.Service.Points.Application;
using Disco.Service.Points.Application.Commands;
using Disco.Service.Points.Infrastructure;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication(builder.Configuration)
    .AddInfrastructure(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseInfrastructure();


app.UseAuthorization();

app.MapGet("GetPointsByUserId/{id:Guid}", async (Guid id, IMediator mediator) =>
{
    //TODO: napisać Testy do tego!!!!
    var points = await mediator.Send(new GetPointsByUserId(id));
    return Results.Ok(points);
    
}).RequireAuthorization();

app.MapPut("AddPoints", async (AddPoints request, IMediator mediator) =>
{
    //TODO: Testy
    await mediator.Send(request);
    return Results.Ok();
    
}).RequireAuthorization(x=>x.RequireRole("Admin","App"));

app.MapPut("SubtractPoints", async (SubtractPoints request, IMediator mediator) =>
{
    //TODO: Testy
    await mediator.Send(request);
    return Results.Ok();
    
}).RequireAuthorization(x=>x.RequireRole("Admin","App"));


await app.RunAsync();
public partial class Program {}