using CleanArchitecture.Application;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Emails;
using CleanArchitecture.Infrastructure.Files;
using CleanArchitecture.Infrastructure.Identity;
using CleanArchitecture.Infrastructure.Persistence;
using CleanArchitecture.Infrastructure.Persistence.DatabaseContext;
using CleanArchitecture.Infrastructure.Persistence.DataSeedings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddFileServices();
builder.Services.AddEmailServices(builder.Configuration);

builder.Host.UseSerilog((context, configurations) => configurations.ReadFrom.Configuration(context.Configuration));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});



using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        await MigrateDatabaseAsync(services);

        await InitializeDatabase(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB. {exceptionMessage}", ex.Message);
    }
}

static async Task InitializeDatabase(IServiceProvider services)
{
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    await ApplicationDataSeed.SeedApplicationUsers(userManager, roleManager);
}

static async Task MigrateDatabaseAsync(IServiceProvider services)
{
    var context = services.GetRequiredService<ApplicationDbContext>();
    if (context.Database.EnsureCreated())
    {
        await context.Database.MigrateAsync();

    }
}

app.Run();
