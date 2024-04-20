using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CinemaBackend.Models;

public partial class CinemaDatabaseContext : DbContext
{

    private readonly IConfiguration _configuration;

    public CinemaDatabaseContext(DbContextOptions<CinemaDatabaseContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<Screening> Screenings { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<Worker> Workers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(_configuration.GetConnectionString("CinemaDatabase"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customer", "EONIS");

            entity.HasIndex(e => e.CustomerEmailAdress, "UQ_Customer_CustomerEmailAdress").IsUnique();

            entity.HasIndex(e => e.CustomerPhoneNumber, "UQ_Customer_CustomerPhoneNumber").IsUnique();

            entity.Property(e => e.CustomerId)
                .HasMaxLength(36)
                .IsUnicode(false);
            entity.Property(e => e.CustomerEmailAdress)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.CustomerMembershipLevel)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CustomerName).HasMaxLength(30);
            entity.Property(e => e.CustomerPhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.ToTable("Movie", "EONIS");

            entity.HasIndex(e => e.MovieTitle, "UQ_Movie_MovieTitle").IsUnique();

            entity.Property(e => e.MovieId)
                .HasMaxLength(36)
                .IsUnicode(false);
            entity.Property(e => e.MovieCast).HasMaxLength(100);
            entity.Property(e => e.MovieDirector).HasMaxLength(30);
            entity.Property(e => e.MovieGenre).HasMaxLength(20);
            entity.Property(e => e.MovieTitle).HasMaxLength(30);
            entity.Property(e => e.WorkerId)
                .HasMaxLength(36)
                .IsUnicode(false);

            entity.HasOne(d => d.Worker).WithMany(p => p.Movies)
                .HasForeignKey(d => d.WorkerId)
                .HasConstraintName("FK_Worker_Movie");
        });

        modelBuilder.Entity<Screening>(entity =>
        {
            entity.ToTable("Screening", "EONIS");

            entity.Property(e => e.ScreeningId)
                .HasMaxLength(36)
                .IsUnicode(false);
            entity.Property(e => e.MovieId)
                .HasMaxLength(36)
                .IsUnicode(false);
            entity.Property(e => e.ScreeningRoom)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.Movie).WithMany(p => p.Screenings)
                .HasForeignKey(d => d.MovieId)
                .HasConstraintName("FK_Movie_Screening");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.ToTable("Ticket", "EONIS", tb => tb.HasTrigger("CheckCustomerLevel"));

            entity.Property(e => e.TicketId)
                .HasMaxLength(36)
                .IsUnicode(false);
            entity.Property(e => e.CustomerId)
                .HasMaxLength(36)
                .IsUnicode(false);
            entity.Property(e => e.ScreeningId)
                .HasMaxLength(36)
                .IsUnicode(false);
            entity.Property(e => e.TicketPrice).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.TicketSeat)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.TicketType)
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.HasOne(d => d.Customer).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_Customer_Ticket");

            entity.HasOne(d => d.Screening).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.ScreeningId)
                .HasConstraintName("FK_Screening_Ticket");
        });

        modelBuilder.Entity<Worker>(entity =>
        {
            entity.ToTable("Worker", "EONIS");

            entity.HasIndex(e => e.WorkerEmailAdress, "UQ_Worker_WorkerEmailAdress").IsUnique();

            entity.HasIndex(e => e.WorkerPhoneNumber, "UQ_Worker_WorkerPhoneNumber").IsUnique();

            entity.Property(e => e.WorkerId)
                .HasMaxLength(36)
                .IsUnicode(false);
            entity.Property(e => e.WorkerCity).HasMaxLength(30);
            entity.Property(e => e.WorkerEmailAdress)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.WorkerName).HasMaxLength(30);
            entity.Property(e => e.WorkerPhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.WorkerSalary).HasColumnType("numeric(18, 0)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
