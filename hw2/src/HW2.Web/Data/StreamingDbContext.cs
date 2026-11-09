using System;
using System.Collections.Generic;
using HW2.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace HW2.Web.Data;

public partial class StreamingDbContext : DbContext
{
    public StreamingDbContext(DbContextOptions<StreamingDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AgeCertification> AgeCertifications { get; set; }

    public virtual DbSet<Credit> Credits { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<GenreAssignment> GenreAssignments { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<ProductionCountry> ProductionCountries { get; set; }

    public virtual DbSet<ProductionCountryAssignment> ProductionCountryAssignments { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Show> Shows { get; set; }

    public virtual DbSet<ShowType> ShowTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AgeCertification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AgeCerti__3214EC27FDF0B11E");

            entity.ToTable("AgeCertification");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CertificationIdentifier).HasMaxLength(20);
        });

        modelBuilder.Entity<Credit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Credit__3214EC270EE09D8B");

            entity.ToTable("Credit");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CharacterName).HasMaxLength(128);
            entity.Property(e => e.PersonId).HasColumnName("PersonID");
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.ShowId).HasColumnName("ShowID");

            entity.HasOne(d => d.Person).WithMany(p => p.Credits)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Credit_Fk_Person");

            entity.HasOne(d => d.Role).WithMany(p => p.Credits)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Credit_Fk_Role");

            entity.HasOne(d => d.Show).WithMany(p => p.Credits)
                .HasForeignKey(d => d.ShowId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Credit_Fk_Show");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Genre__3214EC27AA4BFD6E");

            entity.ToTable("Genre");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.GenreString).HasMaxLength(32);
        });

        modelBuilder.Entity<GenreAssignment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GenreAss__3214EC2772576844");

            entity.ToTable("GenreAssignment");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.GenreId).HasColumnName("GenreID");
            entity.Property(e => e.ShowId).HasColumnName("ShowID");

            entity.HasOne(d => d.Genre).WithMany(p => p.GenreAssignments)
                .HasForeignKey(d => d.GenreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("GenreAssignment_Fk_Genre");

            entity.HasOne(d => d.Show).WithMany(p => p.GenreAssignments)
                .HasForeignKey(d => d.ShowId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("GenreAssignment_Fk_Show");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Person__3214EC27F3863D2D");

            entity.ToTable("Person");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FullName).HasMaxLength(50);
            entity.Property(e => e.JustWatchPersonId).HasColumnName("JustWatchPersonID");
        });

        modelBuilder.Entity<ProductionCountry>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Producti__3214EC2749B74AB0");

            entity.ToTable("ProductionCountry");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CountryIdentifier).HasMaxLength(64);
        });

        modelBuilder.Entity<ProductionCountryAssignment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Producti__3214EC2760D920D0");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ProductionCountryId).HasColumnName("ProductionCountryID");
            entity.Property(e => e.ShowId).HasColumnName("ShowID");

            entity.HasOne(d => d.ProductionCountry).WithMany(p => p.ProductionCountryAssignments)
                .HasForeignKey(d => d.ProductionCountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ProdCountryAssign_Fk_ProductionCountry");

            entity.HasOne(d => d.Show).WithMany(p => p.ProductionCountryAssignments)
                .HasForeignKey(d => d.ShowId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ProdCountryAssign_Fk_Show");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3214EC27236501FA");

            entity.ToTable("Role");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.RoleName).HasMaxLength(32);
        });

        modelBuilder.Entity<Show>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Show__3214EC27F5A16F28");

            entity.ToTable("Show");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AgeCertificationId).HasColumnName("AgeCertificationID");
            entity.Property(e => e.ImdbId)
                .HasMaxLength(12)
                .HasColumnName("ImdbID");
            entity.Property(e => e.JustWatchShowId)
                .HasMaxLength(12)
                .HasColumnName("JustWatchShowID");
            entity.Property(e => e.ShowTypeId).HasColumnName("ShowTypeID");
            entity.Property(e => e.Title).HasMaxLength(128);

            entity.HasOne(d => d.AgeCertification).WithMany(p => p.Shows)
                .HasForeignKey(d => d.AgeCertificationId)
                .HasConstraintName("Show_Fk_AgeCertification");

            entity.HasOne(d => d.ShowType).WithMany(p => p.Shows)
                .HasForeignKey(d => d.ShowTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Show_Fk_ShowType");
        });

        modelBuilder.Entity<ShowType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ShowType__3214EC271F15211D");

            entity.ToTable("ShowType");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ShowTypeIdentifier).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
