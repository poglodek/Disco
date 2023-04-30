using Disco.Service.Discounts.Application;
using Disco.Service.Discounts.Application.Commands;
using Disco.Service.Discounts.Infrastructure;
using Disco.Service.Discounts.Infrastructure.Query;
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

//TODO:TESTS
app.MapPost("AddDiscount", async (AddDiscount add,IMediator mediator)=>
{
    await mediator.Send(add);
    
    return Results.Ok();
    
}).RequireAuthorization(x=>x.RequireRole("Company"));

//TODO: TESTS
app.MapGet("GetDiscounts", async (IMediator mediator) => Results.Ok( await mediator.Send(new GetDiscounts())));

//TODO: TESTS
app.MapPost("UseDiscounts/", async (UseDiscounts discount, IMediator mediator) =>
{
    await mediator.Send(discount);
    
    return Results.Ok();
}).RequireAuthorization(x=>x.RequireRole("User","Admin"));



//TODO: TESTS
app.MapDelete("DeleteDiscount/{id:Guid}", async (Guid id, IMediator mediator) =>
{
    await mediator.Send(new RemoveDiscount(id));
    
    return Results.Ok();
}).RequireAuthorization(x=>x.RequireRole("Company"));


await app.RunAsync();
public partial class Program {}