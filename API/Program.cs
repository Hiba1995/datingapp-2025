using System.Text;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<API.Data.AppDbContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddCors();
builder.Services.AddScoped<ITokenService,TokenService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options=>
{
    var tokenKey = builder.Configuration["TokenKey"] 
    ?? throw new Exception("Token Key not found -Program.cs");

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
        ValidateIssuer = false,
        ValidateAudience = false

    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
// to resolve the error of cors (Access-Control-Allow-Origin =>in the response header)
//I connect the backend with the frontend (with.origins("frontendUrl"))
app.UseCors(options =>options.AllowAnyHeader().AllowAnyMethod()
.WithOrigins("http://localhost:4200", "https://localhost:4200")); 
app.MapControllers(); // configure the middleware to use controllers

app.UseAuthentication();
app.UseAuthorization();


app.Run();
