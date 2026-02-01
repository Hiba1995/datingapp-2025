using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<API.Data.AppDbContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddCors();


var app = builder.Build();

// Configure the HTTP request pipeline.
// to resolve the error of cors (Access-Control-Allow-Origin =>in the response header)
//I connect the backend with the frontend (with.origins("frontendUrl"))
app.UseCors(options =>options.AllowAnyHeader().AllowAnyMethod()
.WithOrigins("http://localhost:4200", "https://localhost:4200")); 
app.MapControllers(); // configure the middleware to use controllers


app.Run();
