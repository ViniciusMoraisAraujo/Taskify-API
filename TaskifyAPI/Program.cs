using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TaskifyAPI.Data;
using TaskifyAPI.Services;
using TaskifyAPI.Services.Interfaces;
using TaskifyAPI.Services.TokenService;
using TaskifyAPI.Validators;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder);
ConfigureAuth(builder);

var app = builder.Build();


app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();

void ConfigureServices(WebApplicationBuilder applicationBuilder)
{
    var connectionString = applicationBuilder.Configuration.GetConnectionString("DefaultConnection");
    applicationBuilder.Services.AddDbContext<TaskyfyDataContext>(options => options.UseSqlServer(connectionString));//add db context
    applicationBuilder.Services.AddControllers();//add controllers
    applicationBuilder.Services.AddControllers().ConfigureApiBehaviorOptions(options => { options.SuppressModelStateInvalidFilter = false; });
    
    applicationBuilder.Services.AddFluentValidationAutoValidation();//add fluent validation
    applicationBuilder.Services.AddFluentValidationClientsideAdapters();
    applicationBuilder.Services.AddValidatorsFromAssemblyContaining<UserRegisterDtoValidator>();//add validators

    applicationBuilder.Services.AddEndpointsApiExplorer();
    applicationBuilder.Services.AddSwaggerGen();//add Swagger
    
    applicationBuilder.Services.AddScoped<IPasswordHasher, PasswordHasher>();//add hasher for password
}

void ConfigureAuth(WebApplicationBuilder applicationBuilder)
{
    applicationBuilder.Services.AddAuthentication(options =>
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
                ValidIssuer = applicationBuilder.Configuration["Jwt:Issuer"],
                ValidAudience = applicationBuilder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(applicationBuilder.Configuration["Jwt:Key"]))
            };
        });
    applicationBuilder.Services.AddScoped<ITokenService, TokenService>();
}