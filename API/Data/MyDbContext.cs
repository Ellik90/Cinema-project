namespace Microsoft.EntityFrameworkCore;

using API.Models;

public class MyDbContext : DbContext
{
     public DbSet<Customer> customer {get;set;} //kommer åt personen i databasen

    // protected override void OnConfiguring(DbContextOptionsBuilder options)
    // {
    //     options.UseInMemoryDatabase("MinDatabas");
    // }
     protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite($"Data Source = {"database.db"}");
    }
}