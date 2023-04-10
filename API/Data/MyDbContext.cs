using Microsoft.EntityFrameworkCore;
using API.Models;
using API.DTO;
using Microsoft.Data.Sqlite;


namespace API.Data;

public class MyDbContext : DbContext
{
    // public DbSet<Customer> customer { get; set; } //kommer åt personen i databasen
    public DbSet<Movie> movies { get; set; }
    public DbSet<Salon> salons { get; set; }
    public DbSet<MovieView> movieViews { get; set; }
    public DbSet<Seat> seats { get; set; }
    public DbSet<Reservation> reservations { get; set; }



    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options) { }
    // protected override void OnConfiguring(DbContextOptionsBuilder options)
    // {
    //     options.UseInMemoryDatabase("MinDatabas");
    // }
     protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite($"Data Source = {"database.db"}");
        // options.UseSqlite("Data Source=biotranan.db");

    }
    // protected override void OnConfiguring
    //  (DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder.UseInMemoryDatabase(databaseName: "DataBase");

    // }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Salon>().HasMany(s => s.Seats).WithOne(se => se.Salon);

        modelBuilder.Entity<Movie>()
       .HasMany(m => m.movieViews)
       .WithOne(v => v.Movie);

       modelBuilder.Entity<MovieView>().HasOne(mv => mv.Movie)
       .WithMany(m => m.movieViews);

       modelBuilder.Entity<MovieView>().HasOne(mv => mv.Salon)
       .WithMany(s => s.Views);

       modelBuilder.Entity<Reservation>().HasOne(r => r.MovieView)
       .WithMany(mv => mv.Reservations);

         modelBuilder.Entity<Movie>()
        .Property(m => m.Actors)
        .HasMaxLength(1000) // ange maxlängden på strängen
        .IsRequired(); // gör kolumnen obligatorisk

modelBuilder.Entity<Movie>()
        .Property(m => m.Directors)
        .HasMaxLength(1000) // ange maxlängden på strängen
        .IsRequired(); // gör kolumnen obligatorisk
    }

}