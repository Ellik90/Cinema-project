using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.Data;

public class MyDbContext : DbContext
{
    // public DbSet<Customer> customer { get; set; } //kommer åt personen i databasen
    public DbSet<Movie> movies { get; set; }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options) { }
    // protected override void OnConfiguring(DbContextOptionsBuilder options)
    // {
    //     options.UseInMemoryDatabase("MinDatabas");
    // }
    //  protected override void OnConfiguring(DbContextOptionsBuilder options)
    // {
    //     options.UseSqlite($"Data Source = {"database.db"}");
    // }
    protected override void OnConfiguring
     (DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(databaseName: "DataBase");
    }

}