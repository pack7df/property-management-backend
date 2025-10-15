using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using PropertyManagement.Application;
using PropertyManagement.Domain.Abstractions;
using PropertyManagement.Domain.Abstractions.Repositories;
using PropertyManagement.Infrastructure.Persistence;
using PropertyManagement.Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CustomDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
)
);

builder.Services.AddScoped<IHostServices, HostServicesImpl>();
builder.Services.AddScoped<IHostRepository, HostRepositoryImpl>();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<EFMiddleware>();

app.MapControllers();

app.Run();
