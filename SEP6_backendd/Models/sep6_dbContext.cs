
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SEP6_backendd.Models
{
    public partial class sep6_dbContext : DbContext
    {
        public sep6_dbContext()
        {
        }

        public sep6_dbContext(DbContextOptions<sep6_dbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Airlines> Airlines { get; set; }
        public virtual DbSet<Flights> Flights { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=sep6.cr8rrqpu4nwe.eu-west-1.rds.amazonaws.com;database=sep6_db;user=admin;password=Admin123", x => x.ServerVersion("8.0.20-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Airlines>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("airlines");

                entity.Property(e => e.Carrier)
                    .HasColumnName("carrier")
                    .HasColumnType("varchar(2)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Flights>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("flights");

                entity.Property(e => e.AirTime).HasColumnName("air_time");

                entity.Property(e => e.ArrDelay).HasColumnName("arr_delay");

                entity.Property(e => e.ArrTime).HasColumnName("arr_time");

                entity.Property(e => e.Carrier)
                    .HasColumnName("carrier")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Day).HasColumnName("DAY");

                entity.Property(e => e.DepDelay).HasColumnName("dep_delay");

                entity.Property(e => e.DepTime).HasColumnName("dep_time");

                entity.Property(e => e.Dest)
                    .HasColumnName("dest")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Distance).HasColumnName("distance");

                entity.Property(e => e.Flight).HasColumnName("flight");

                entity.Property(e => e.Hour).HasColumnName("HOUR");

                entity.Property(e => e.Minute).HasColumnName("MINUTE");

                entity.Property(e => e.Month).HasColumnName("MONTH");

                entity.Property(e => e.Origin)
                    .HasColumnName("origin")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Tailnum)
                    .HasColumnName("tailnum")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Year).HasColumnName("YEAR");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
