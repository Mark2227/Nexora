using Microsoft.EntityFrameworkCore;
using Nexora.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<NexoraDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("NexoraDatabase"));
});

var app = builder.Build();

var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<NexoraDbContext>();
context.Database.EnsureDeleted();
context.Database.EnsureCreated();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.Run();
