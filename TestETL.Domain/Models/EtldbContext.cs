using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TestETL.Domain.Models;

public partial class EtldbContext : DbContext
{
    public EtldbContext()
    {
    }

    public EtldbContext(DbContextOptions<EtldbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Csv> Csvs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=ETLDb;Trusted_Connection=True;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Csv>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_CSVId");

            entity.ToTable("CSV");

            entity.HasIndex(e => e.PulocationId, "IX_PULocationID");

            entity.HasIndex(e => e.TpepDropoffDatetime, "IX_tpep_dropoff_datetime");

            entity.HasIndex(e => e.TripDistance, "IX_trip_distance");

            entity.Property(e => e.DolocationId).HasColumnName("DOLocationID");
            entity.Property(e => e.FareAmount)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("fare_amount");
            entity.Property(e => e.PassengerCount).HasColumnName("passenger_count");
            entity.Property(e => e.PulocationId).HasColumnName("PULocationID");
            entity.Property(e => e.StoreAndFwdFlag)
                .HasMaxLength(4)
                .HasColumnName("store_and_fwd_flag");
            entity.Property(e => e.TipAmount)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("tip_amount");
            entity.Property(e => e.TpepDropoffDatetime)
                .HasColumnType("datetime")
                .HasColumnName("tpep_dropoff_datetime");
            entity.Property(e => e.TpepPickupDatetime)
                .HasColumnType("datetime")
                .HasColumnName("tpep_pickup_datetime");
            entity.Property(e => e.TripDistance)
                .HasColumnType("numeric(5, 3)")
                .HasColumnName("trip_distance");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
