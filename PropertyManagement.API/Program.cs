using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.IdentityModel.Tokens;
using PropertyManagement.Application;
using PropertyManagement.Domain.Abstractions;
using PropertyManagement.Domain.Abstractions.Repositories;
using PropertyManagement.Domain.Entities;
using PropertyManagement.Infrastructure;
using PropertyManagement.Infrastructure.Persistence;
using PropertyManagement.Infrastructure.Persistence.Repositories;
using PropertyManagement.Infrastructure.ThirdParty;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CustomDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
)
);

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IHostServices, HostServicesImpl>();
builder.Services.AddScoped<IHostRepository, HostRepositoryImpl>();
builder.Services.AddScoped<IPropertyServices, PropertyServicesImpl>();
builder.Services.AddScoped<IPropertyRepository, PropertyRepositoryImpl>();
builder.Services.AddScoped<IOTAManager, OTAManagerMock>();
builder.Services.AddScoped<ISecurityServices, SecurityServiceImpl>();
builder.Services.AddScoped<IUserRepository, UserRepositoryImpl>();


var jwtConfig = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtConfig["Key"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtConfig["Issuer"],
        ValidAudience = jwtConfig["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c=>
    {
        c.EnableAnnotations();
    });


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddAuthorization();

var app = builder.Build();



if (args.Length > 0 && args[0].ToLower() == "seed")
{
    //Seed logic for test
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<CustomDbContext>();
    var hostServices = scope.ServiceProvider.GetRequiredService<IHostServices>();
    var propertyServices = scope.ServiceProvider.GetRequiredService<IPropertyServices>();
    var securityServices = scope.ServiceProvider.GetRequiredService<ISecurityServices>();

    var seed = new SeedGenerator(dbContext, securityServices, hostServices, propertyServices);
    await seed.GenerateAsync();
    return;
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();             
    app.UseSwaggerUI(options =>   
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        options.RoutePrefix = ""; 
    });

    app.MapOpenApi();
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<EFMiddleware>();

app.MapControllers();

app.Run();
