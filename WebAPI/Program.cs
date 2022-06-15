using AspNetCore.Authentication.Basic;
using Microsoft.EntityFrameworkCore;
using WebAPI.BL.Models;
using WebAPI.BL.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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
