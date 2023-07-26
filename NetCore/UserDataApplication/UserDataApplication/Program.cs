using Microsoft.EntityFrameworkCore;
using UserDataApplication.Models;
using UserDataApplication.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<sdirectdbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("con")));
builder.Services.AddScoped<InterfaceUserData, UserDataClass>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(option => option.AddPolicy("cors", builder =>
    builder
    .AllowAnyHeader()
    .AllowAnyOrigin()
    .AllowAnyMethod()

));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseAuthorization();

app.MapControllers();
app.UseCors("cors");
app.Run();
