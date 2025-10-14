using Microsoft.EntityFrameworkCore;
using vacation_backend.Application;
using vacation_backend.Infraestructure;
using vacation_backend.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Application (servicios)
builder.Services.AddApplication();

// Infrastructure (repositorios, DbContext)
builder.Services.AddInfrastructure();

//Context
builder.Services.AddDbContext<VacationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.

builder.Services.AddControllers();
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
