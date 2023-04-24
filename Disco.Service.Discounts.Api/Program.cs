using Disco.Service.Discounts.Application;
using Disco.Service.Discounts.Infrastructure;

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

//TODO:Creating new 
app.MapPost("AddDiscount",()=>
{

    return Results.Ok();
}).RequireAuthorization("Company");;

//TODO:Get discounts
app.MapGet("GetDiscounts", () =>
{
    return Results.Ok();
});

//TODO:Removing points from account  and return a code with discount 
app.MapPost("UseDiscounts/", () =>
{
    return Results.Ok();
});

//TODO:removing discount
app.MapDelete("DeleteDiscount/{id:Guid}", () =>
{
    return Results.Ok();
}).RequireAuthorization("Company");


await app.RunAsync();
public partial class Program {}