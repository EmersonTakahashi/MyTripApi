

using Microsoft.EntityFrameworkCore;
using MyTripApi.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile($"appsettings.json");
builder.Configuration.AddJsonFile($"appsettings.Development.json");

// Add services to the container.
builder.Services.AddDbContext<MyTripDbContext>(option =>
{
    option.UseNpgsql(builder.Configuration.GetConnectionString("DefaultPostgreSQLConnection"));
});

builder.Services.AddControllers(option => { 
    //option.ReturnHttpNotAcceptable=true;
}).AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
