using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FatSecretWebApp.Models.DBContext
{
    public partial class FatSecretDataExporterContext : DbContext
    {
        public FatSecretDataExporterContext()
        {
        }

        public FatSecretDataExporterContext(DbContextOptions<FatSecretDataExporterContext> options)
            : base(options)
        {
        }

        public virtual DbSet<FatSecretLog> FatSecretLog { get; set; }
        public virtual DbSet<FatSecretToken> FatSecretToken { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<FatSecretLog>(entity =>
            {
                entity.Property(e => e.GoogleUserName).HasMaxLength(200);

                entity.Property(e => e.LogTexxt).IsRequired();

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");
            });

            modelBuilder.Entity<FatSecretToken>(entity =>
            {
                entity.HasIndex(e => e.GoogleUserName)
                    .HasName("IX_FatSecretTokenGoogleUserName");

                entity.Property(e => e.FatSecretAuthSecret)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.FatSecretUserToken)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.GoogleUserName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}