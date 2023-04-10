﻿// <auto-generated />
using System;
using API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace API.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20230410143211_actoranddirector")]
    partial class actoranddirector
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.4");

            modelBuilder.Entity("API.Models.Movie", b =>
                {
                    b.Property<int>("MovieId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Actors")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Directors")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("TEXT");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("MaxViews")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MovieLength")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MovieViewsShown")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("MovieId");

                    b.ToTable("movies");
                });

            modelBuilder.Entity("API.Models.MovieView", b =>
                {
                    b.Property<int>("MovieViewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<int>("MovieId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("MovieTitle")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("SalonId")
                        .HasColumnType("INTEGER");

                    b.HasKey("MovieViewId");

                    b.HasIndex("MovieId");

                    b.HasIndex("SalonId");

                    b.ToTable("movieViews");
                });

            modelBuilder.Entity("API.Models.Reservation", b =>
                {
                    b.Property<int>("ReservationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateForReservation")
                        .HasColumnType("TEXT");

                    b.Property<int>("MovieViewId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("NumberOfSeats")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ReservationId");

                    b.HasIndex("MovieViewId");

                    b.ToTable("reservations");
                });

            modelBuilder.Entity("API.Models.Salon", b =>
                {
                    b.Property<int>("SalonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("NumberOfRows")
                        .HasColumnType("INTEGER");

                    b.Property<int>("NumberOfSeats")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SalonName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("SalonId");

                    b.ToTable("salons");
                });

            modelBuilder.Entity("API.Models.Seat", b =>
                {
                    b.Property<int>("SeatId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("SalonId")
                        .HasColumnType("INTEGER");

                    b.HasKey("SeatId");

                    b.HasIndex("SalonId");

                    b.ToTable("seats");
                });

            modelBuilder.Entity("API.Models.MovieView", b =>
                {
                    b.HasOne("API.Models.Movie", "Movie")
                        .WithMany("movieViews")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Models.Salon", "Salon")
                        .WithMany("Views")
                        .HasForeignKey("SalonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");

                    b.Navigation("Salon");
                });

            modelBuilder.Entity("API.Models.Reservation", b =>
                {
                    b.HasOne("API.Models.MovieView", "MovieView")
                        .WithMany("Reservations")
                        .HasForeignKey("MovieViewId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MovieView");
                });

            modelBuilder.Entity("API.Models.Seat", b =>
                {
                    b.HasOne("API.Models.Salon", "Salon")
                        .WithMany("Seats")
                        .HasForeignKey("SalonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Salon");
                });

            modelBuilder.Entity("API.Models.Movie", b =>
                {
                    b.Navigation("movieViews");
                });

            modelBuilder.Entity("API.Models.MovieView", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("API.Models.Salon", b =>
                {
                    b.Navigation("Seats");

                    b.Navigation("Views");
                });
#pragma warning restore 612, 618
        }
    }
}