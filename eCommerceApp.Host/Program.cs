using eCommerceApp.Application.DependencyInjection;
using eCommerceApp.Domain.Entities.Identity;
using eCommerceApp.Domain.Interfaces;
using eCommerceApp.Infrastructure.DependencyInjection;
using eCommerceApp.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("log/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();
Log.Logger.Information("Application is building.......");

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructureService(builder.Configuration);
builder.Services.AddApplicationService();

IServiceCollection serviceCollection = builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddCors(builder =>
{
    builder.AddDefaultPolicy(options =>
    {
        options.AllowAnyHeader()
        .AllowAnyMethod()
        .WithOrigins("https://localhost:7025")
        .AllowCredentials();
    });
});

builder.Services.AddSwaggerGen(c =>
{
c.SwaggerDoc("v1", new OpenApiInfo { Title = "eCommerce API", Version = "v1" });

c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
{
    In = ParameterLocation.Header,
    Description = "Masukkan JWT dengan format: Bearer {token}",
    Name = "Authorization",
    Type = SecuritySchemeType.Http,
    Scheme = "Bearer",
    BearerFormat = "JWT"
});

c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Configuration.AddUserSecrets<Program>();

try
{
    var app = builder.Build();
    app.UseCors();
    app.UseSerilogRequestLogging();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseInfrastructureService();

    app.UseHttpsRedirection();

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();

    // ---- SEEDING ROLE ----
    //using (var scope = app.Services.CreateScope())
    //{
    //    var services = scope.ServiceProvider;
    //    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    //    string[] roleNames = { "Admin", "User" };
    //    foreach (var roleName in roleNames)
    //    {
    //        var roleExists = await roleManager.RoleExistsAsync(roleName);
    //        if (!roleExists)
    //        {
    //            await roleManager.CreateAsync(new IdentityRole(roleName));
    //        }
    //    }
    //}
    // ----------------------

    Log.Logger.Information("Application is running.......");
    app.Run();
}
catch (Exception ex)
{
    Log.Logger.Error(ex, "Application failed to start....");
}
finally
{
    Log.CloseAndFlush();
}

namespace eCommerceApp.Host
{
    public partial class Program { } // <- ini penting untuk test
}