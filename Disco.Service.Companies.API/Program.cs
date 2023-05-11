using Disco.Service.Companies.Application.Commands;
using Disco.Service.Companies.Application.Query;
using Disco.Service.Companies.Infrastructure;
using Disco.Service.Discounts.Application;
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

app.UseAuthorization();

app.MapGet("{id:Guid}", async (Guid id, IMediator mediator) =>
{
    var company = await mediator.Send(new GetCompanyById(id));
    return Results.Ok(company);
    
}).RequireAuthorization();

app.MapPost("create", async (CreateCompany request, IMediator mediator) =>
{
    var company = await mediator.Send(request);
    return Results.Ok(company);
});


await app.RunAsync();
public partial class Program {}