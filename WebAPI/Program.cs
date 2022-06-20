using AspNetCore.Authentication.Basic;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebAPI.BL.Models;
using WebAPI.BL.Services;

var builder = WebApplication.CreateBuilder(args);
var jwtIss = builder.Configuration["Jwt:Issuer"];
var jwtKey = builder.Configuration["Jwt:Key"];

// Add services to the container.
AddBasicAuth(builder);
//AddJwtAuth(builder, jwtIss, jwtKey);

builder.Services.AddAuthentication(BasicDefaults.AuthenticationScheme)
    .AddBasic<BasicUserValidationService>(options => { options.Realm = "CentralRepository"; });

builder.Services.AddControllers();
builder.Services.AddDbContext<ModelContext>(options =>
    options.UseOracle("name=ConnectionStrings:DefaultConnection", 
    config => 
        config.UseOracleSQLCompatibility("11")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IKorisnikService, KorisnikService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

static void AddBasicAuth(WebApplicationBuilder builder)
{
    builder.Services
        .AddAuthentication(BasicDefaults.AuthenticationScheme)
        .AddBasic<BasicUserValidationService>(options =>
        {
            options.Realm = "CentralRepository";
        });
}

static void AddJwtAuth(WebApplicationBuilder builder, string jwtIss, string jwtKey)
{
    builder.Services
        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtIss,
                ValidAudience = jwtIss,
                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            jwtKey))
            };
        });
}