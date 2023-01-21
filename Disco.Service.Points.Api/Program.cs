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
app.UseHttpsRedirection();


app.UseAuthorization();

app.MapGet("GetPointsByUserId/{id:Guid}", async (Guid id, IMediator mediator) =>
{
    var points = await mediator.Send(new GetPointsByUserId(id));
    return Results.Ok(points);
    
}).RequireAuthorization();

app.MapPut("AddPoints", async (Guid id, IMediator mediator) =>
{
    var points = await mediator.Send(new GetPointsByUserId(id));
    return Results.Ok(points);
    
}).RequireAuthorization(x=>x.RequireRole("Admin","App"));


await app.RunAsync();
public partial class Program {}