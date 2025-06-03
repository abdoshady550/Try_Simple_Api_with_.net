using System.Threading.RateLimiting;
using Asp.net_Web_Api.Data;
using Asp.net_Web_Api.Meddlewares;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();


var configuration = new ConfigurationBuilder()
     .AddJsonFile("appsettings.json")
     .Build();
var connStr = configuration.GetSection("constr").Value;

builder.Services.AddDbContext<AppDbContext>(option => option
.UseSqlServer(connStr));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<RateLimiterMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
