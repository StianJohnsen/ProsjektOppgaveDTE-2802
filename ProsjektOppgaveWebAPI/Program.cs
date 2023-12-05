using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProsjektOppgaveWebAPI.Database.Entities;
using ProsjektOppgaveWebAPI.EntityFramework;
using ProsjektOppgaveWebAPI.EntityFramework.Repository;
using ProsjektOppgaveWebAPI.Services.BlogServices;
using ProsjektOppgaveWebAPI.Services.JwtServices;
using ProsjektOppgaveWebAPI.Services.JwtServices.Models;
using ProsjektOppgaveWebAPI.Services.UserServices;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(builder.Configuration["ConnectionStrings:SqliteConnection"]));

services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;

        byte[] byteKey = Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JwtOptions:Key").Value);

        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            IssuerSigningKey = new SymmetricSecurityKey(byteKey),
            ValidateIssuerSigningKey = true
        };
    });

services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));

services.AddTransient<IJwtService, JwtService>();
services.AddScoped<IPasswordHasher<UserEntity>, PasswordHasher<UserEntity>>();
services.AddTransient<IUserService, UserService>();

services.AddTransient<IBlogService, BlogService>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
