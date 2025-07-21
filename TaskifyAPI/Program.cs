using Microsoft.EntityFrameworkCore;
using TaskifyAPI.Data;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder);


var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.MapOpenApi();


app.UseHttpsRedirection();


app.Run();

void ConfigureServices(WebApplicationBuilder builder)
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<TaskyfyDataContext>(options => options.UseSqlServer(connectionString));
}