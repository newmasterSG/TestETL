using Microsoft.EntityFrameworkCore;
using TestETL.Domain.Models;

namespace TestETL.Infrastructure.Db
{
    public class EtldbContext : DbContext
    {
        public EtldbContext()
        {
        }

        public EtldbContext(DbContextOptions<EtldbContext> options)
            : base(options)
        {
        }

        public DbSet<Csv> Csvs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
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
        }
    }
}
