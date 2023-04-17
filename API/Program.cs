
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();

//SKRI
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<API.Data.IMovieRepository, API.Data.MovieRepository>();
builder.Services.AddScoped<API.Data.ISalonRepository, API.Data.SalonRepository>();
builder.Services.AddScoped<API.Data.IMovieViewRepository, API.Data.MovieViewRepository>();
builder.Services.AddScoped<API.Data.ISeatRepository, API.Data.SeatRepository>();
builder.Services.AddScoped<API.Data.IReservationRepository, API.Data.ReservationRepository>();
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
