using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using TaskifyAPI.Data;
using TaskifyAPI.Services;
using TaskifyAPI.Services.Interfaces;
using TaskifyAPI.Validators;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder);


var app = builder.Build();




app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();

void ConfigureServices(WebApplicationBuilder applicationBuilder)
{
    var connectionString = applicationBuilder.Configuration.GetConnectionString("DefaultConnection");
    applicationBuilder.Services.AddDbContext<TaskyfyDataContext>(options => options.UseSqlServer(connectionString));
    applicationBuilder.Services.AddControllers();
    applicationBuilder.Services.AddControllers().ConfigureApiBehaviorOptions(options => { options.SuppressModelStateInvalidFilter = false; });
    
    applicationBuilder.Services.AddFluentValidationAutoValidation();//add fluent validation
    applicationBuilder.Services.AddFluentValidationClientsideAdapters();
    applicationBuilder.Services.AddValidatorsFromAssemblyContaining<UserRegisterDtoValidator>();//add validators

    applicationBuilder.Services.AddEndpointsApiExplorer();
    applicationBuilder.Services.AddSwaggerGen();
    
    applicationBuilder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
}