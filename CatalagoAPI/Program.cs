using CatalagoAPI.Data.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

string mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("A connection string 'DefaultConnectionString' não foi encontrada");

builder.Services.AddDbContext<CatalagoApiDbContext>(options => 
                options.UseMySql(mySqlConnection, 
                ServerVersion.AutoDetect(mySqlConnection)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "Catalago API"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
