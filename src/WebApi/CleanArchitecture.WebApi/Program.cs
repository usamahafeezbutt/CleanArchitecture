using CleanArchitecture.WebApi.Configurations;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddWebApi(builder.Configuration);
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
app.UseHealthChecks("/health");
await app.UseWebApiMiddlewares();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
