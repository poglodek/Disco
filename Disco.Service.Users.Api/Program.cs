using Disco.Service.Users.Application;
using Disco.Service.Users.Application.Commands;
using Disco.Service.Users.Infrastructure;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
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

app.UseAuthorization();

app.MapControllers();

app.Run();


void MapEndpoints(WebApplication webApplication)
{
    webApplication.MapPost("AddUser", async (AddUser request, IMediator mediator) => { await mediator.Send(request); });
}