using BackendTZ.Application.Services;
using BackendTZ.Core.Abstractions;
using BackendTZ.Core.Models;
using BackendTZ.DataAccess;
using BackendTZ.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(DataContext)));
    });

builder.Services. AddScoped<IService<Service>, ServService>();
builder.Services.AddScoped<IService<ConferenceHall>, HallService>();
builder.Services.AddScoped<IService<Booking>, BookingService>();
builder.Services.AddScoped<IRepository<Service>, ServiceRepository>();
builder.Services.AddScoped<IRepository<Booking>, BookingRepository>();
builder.Services.AddScoped<IRepository<ConferenceHall>, ConferenceHallRepository>();

var app = builder.Build();
app.UseCors(builder => builder.AllowAnyOrigin()
                              .AllowAnyHeader()
                              .AllowAnyMethod());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
