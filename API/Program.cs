
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();

//SKRI
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<API.Interface.IMovieRepository, API.Data.MovieRepository>();
builder.Services.AddScoped<API.Interface.ISalonRepository, API.Data.SalonRepository>();
builder.Services.AddScoped<API.Interface.IMovieViewRepository, API.Data.MovieViewRepository>();
builder.Services.AddScoped<API.Interface.ISeatRepository, API.Data.SeatRepository>();
builder.Services.AddScoped<API.Interface.IReservationRepository, API.Data.ReservationRepository>();
builder.Services.AddDbContext<API.Data.MyDbContext>
(o => o.UseSqlite("DataBase"));
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
