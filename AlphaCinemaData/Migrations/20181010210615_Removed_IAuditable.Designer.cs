﻿// <auto-generated />
using System;
using AlphaCinemaData.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AlphaCinemaData.Migrations
{
    [DbContext(typeof(AlphaCinemaContext))]
    [Migration("20181010210615_Removed_IAuditable")]
    partial class Removed_IAuditable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AlphaCinemaData.Models.Associative.MovieGenre", b =>
                {
                    b.Property<Guid>("MovieId");

                    b.Property<Guid>("GenreId");

                    b.Property<bool>("IsDeleted");

                    b.HasKey("MovieId", "GenreId");

                    b.HasIndex("GenreId");

                    b.ToTable("MoviesGenres");
                });

            modelBuilder.Entity("AlphaCinemaData.Models.Associative.Projection", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CityId");

                    b.Property<DateTime>("Date");

                    b.Property<bool>("IsDeleted");

                    b.Property<Guid>("MovieId");

                    b.Property<Guid>("OpenHourId");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("OpenHourId");

                    b.HasIndex("MovieId", "CityId", "OpenHourId")
                        .IsUnique();

                    b.ToTable("Projections");
                });

            modelBuilder.Entity("AlphaCinemaData.Models.Associative.WatchedMovie", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("ProjectionId");

                    b.Property<bool>("IsDeleted");

                    b.HasKey("UserId", "ProjectionId");

                    b.HasIndex("ProjectionId");

                    b.ToTable("WatchedMovies");
                });

            modelBuilder.Entity("AlphaCinemaData.Models.City", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("AlphaCinemaData.Models.Genre", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("AlphaCinemaData.Models.Movie", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(150);

                    b.Property<int>("Duration");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.Property<int>("ReleaseYear");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("AlphaCinemaData.Models.OpenHour", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("StartHour")
                        .HasMaxLength(6);

                    b.HasKey("Id");

                    b.HasIndex("StartHour")
                        .IsUnique()
                        .HasFilter("[StartHour] IS NOT NULL");

                    b.ToTable("OpenHours");
                });

            modelBuilder.Entity("AlphaCinemaData.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Age");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AlphaCinemaData.Models.Associative.MovieGenre", b =>
                {
                    b.HasOne("AlphaCinemaData.Models.Genre", "Genre")
                        .WithMany("MoviesGenres")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AlphaCinemaData.Models.Movie", "Movie")
                        .WithMany("MovieGenres")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AlphaCinemaData.Models.Associative.Projection", b =>
                {
                    b.HasOne("AlphaCinemaData.Models.City", "City")
                        .WithMany("Projections")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AlphaCinemaData.Models.Movie", "Movie")
                        .WithMany("Projections")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AlphaCinemaData.Models.OpenHour", "OpenHour")
                        .WithMany("Projections")
                        .HasForeignKey("OpenHourId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AlphaCinemaData.Models.Associative.WatchedMovie", b =>
                {
                    b.HasOne("AlphaCinemaData.Models.Associative.Projection", "Projection")
                        .WithMany("WatchedMovies")
                        .HasForeignKey("ProjectionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AlphaCinemaData.Models.User", "User")
                        .WithMany("WatchedMovies")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
