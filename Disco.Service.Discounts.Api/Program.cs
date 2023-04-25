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
    
}).RequireAuthorization("Company");;

//TODO: TESTS
app.MapGet("GetDiscounts", async (IMediator mediator) => Results.Ok( await mediator.Send(new GetDiscounts())));

//TODO: Removing points from account  and return a code with discount 
app.MapPost("UseDiscounts/", async (UseDiscounts discount, IMediator mediator) =>
{
    await mediator.Publish(discount);
    
    return Results.Ok();
}).RequireAuthorization("User","Admin");



//TODO:removing discount
app.MapDelete("DeleteDiscount/{id:Guid}", () =>
{
    return Results.Ok();
}).RequireAuthorization("Company");


await app.RunAsync();
public partial class Program {}