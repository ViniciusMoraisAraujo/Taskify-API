using System.Security.Claims;
using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TaskifyAPI.Data;
using TaskifyAPI.Middlewares;
using TaskifyAPI.Repository.TaskItemRepository;
using TaskifyAPI.Repository.UserRepository;
using TaskifyAPI.Services;
using TaskifyAPI.Services.PasswordHasher;
using TaskifyAPI.Services.TaskItemService;
using TaskifyAPI.Services.TokenService;
using TaskifyAPI.Services.UserService;
using TaskifyAPI.Validators;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder);
ConfigureAuth(builder);

var app = builder.Build();


app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionMiddleware>();
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
    
    applicationBuilder.Services.AddScoped<IPasswordHasherService, PasswordHasherService>();//add dependency for password hash
    applicationBuilder.Services.AddScoped<ITokenService, TokenService>();//add dependency for token service
    applicationBuilder.Services.AddScoped<IUserRepository, UserRepository>();//add dependency for user repository
    applicationBuilder.Services.AddScoped<IUserService, UserService>();//add dependency for user service
    applicationBuilder.Services.AddScoped<ITaskItemRepository,TaskItemRepository>();//add dependency for taskItem repository
    applicationBuilder.Services.AddScoped<ITaskItemService,TaskItemService>();//add dependency for taskItem service
    applicationBuilder.Services.AddHttpContextAccessor();
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
            options.Events = new JwtBearerEvents
            {
                OnTokenValidated = context =>
                {
                    var identity = context.Principal.Identity as ClaimsIdentity;
                    var userRoleClaim = identity.FindFirst("user_role");
                   
                    if (userRoleClaim != null)
                        identity.AddClaim(new Claim(ClaimTypes.Role, userRoleClaim.Value));
                    
                    return Task.CompletedTask;
                }
            };
        });
    applicationBuilder.Services.AddScoped<ITokenService, TokenService>();
}