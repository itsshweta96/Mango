using Mango.CouponAPI.Data;
using Mango.CouponAPI.Models;
using Mango.CouponAPI.Models.Dto;
using Mango.Services.Shared.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddAutoMapper(o =>
{
    o.CreateMap<Coupon, CouponDto>().ReverseMap();
});

//var jwtOptions = builder.Configuration.GetSection("ApiSettings:JwtOptions"); // using Microsoft.AspNetCore.Authentication.JwtBearer;
//var secret = jwtOptions.GetValue<string>("Secret");
//var issuer = jwtOptions.GetValue<string>("Issuer");
//var audience = jwtOptions.GetValue<string>("Audience");

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,
//            ValidIssuer = issuer,
//            ValidAudience = audience,
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret))
//        };
//    });

builder.AddJwtAuthentication(); // using Mango.Services.Shared.Extensions;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();

}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
