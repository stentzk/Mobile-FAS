using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MobileConfigServices.Entities
{
    public partial class RoutebookPTContext : DbContext
    {
        public RoutebookPTContext()
        {
        }

        public RoutebookPTContext(DbContextOptions<RoutebookPTContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CfgApplication> CfgApplication { get; set; }
        public virtual DbSet<CfgLocation> CfgLocation { get; set; }
        public virtual DbSet<CfgNotification> CfgNotification { get; set; }
        public virtual DbSet<CfgParameters> CfgParameters { get; set; }
        public virtual DbSet<CfgRoute> CfgRoute { get; set; }


        // Unable to generate entity type for table 'dbo.tblClassTemp'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.tblClassImportTemp'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.cfgAudit'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=cdmassql51;Initial Catalog=RoutebookPT;Persist Security Info=True;User ID=RoutebookRw;Password=Spring13");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CfgApplication>(entity =>
            {
                entity.HasKey(e => e.AppId);

                entity.ToTable("cfgApplication");

                entity.Property(e => e.AppId).HasColumnName("appId");

                entity.Property(e => e.AppDescription)
                    .HasColumnName("appDescription")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AppName)
                    .IsRequired()
                    .HasColumnName("appName")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("updatedDate")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<CfgLocation>(entity =>
            {
                entity.HasKey(e => e.LocationId);

                entity.ToTable("cfgLocation");

                entity.HasIndex(e => e.LocationNumber)
                    .HasName("IX_cfgLocation")
                    .IsUnique();

                entity.Property(e => e.LocationId).HasColumnName("locationId");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.AppId).HasColumnName("appId");

                entity.Property(e => e.LocationNumber)
                    .IsRequired()
                    .HasColumnName("locationNumber")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("updatedDate")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.App)
                    .WithMany(p => p.CfgLocation)
                    .HasForeignKey(d => d.AppId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_cfgLocation_cfgApplication");
            });

            modelBuilder.Entity<CfgNotification>(entity =>
            {
                entity.HasKey(e => e.NotificationId);

                entity.ToTable("cfgNotification");

                entity.Property(e => e.NotificationId).HasColumnName("notificationId");

                entity.Property(e => e.AppId).HasColumnName("appId");

                entity.Property(e => e.LocationId).HasColumnName("locationId");

                entity.Property(e => e.NotificationData)
                    .HasColumnName("notificationData")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NotificationEndTime)
                    .HasColumnName("notificationEndTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.NotificationStartTime)
                    .HasColumnName("notificationStartTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.RouteId).HasColumnName("routeId");

                entity.Property(e => e.UpdateDate)
                    .HasColumnName("updateDate")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.App)
                    .WithMany(p => p.CfgNotification)
                    .HasForeignKey(d => d.AppId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_cfgNotification_cfgApplication");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.CfgNotification)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_cfgNotification_cfgLocation");

                entity.HasOne(d => d.Route)
                    .WithMany(p => p.CfgNotification)
                    .HasForeignKey(d => d.RouteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_cfgNotification_cfgRoute");
            });

            modelBuilder.Entity<CfgParameters>(entity =>
            {
                entity.HasKey(e => e.ParameterId);

                entity.ToTable("cfgParameters");

                entity.Property(e => e.ParameterId).HasColumnName("parameterId");

                entity.Property(e => e.AppId).HasColumnName("appId");

                entity.Property(e => e.ForceFullDownload).HasColumnName("forceFullDownload");

                entity.Property(e => e.FullDownloadEndTime)
                    .HasColumnName("fullDownloadEndTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.FullDownloadStartTime)
                    .HasColumnName("fullDownloadStartTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.LocationId).HasColumnName("locationId");

                entity.Property(e => e.LoggingEndTime)
                    .HasColumnName("loggingEndTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.LoggingLevel).HasColumnName("loggingLevel");

                entity.Property(e => e.LoggingStartTime)
                    .HasColumnName("loggingStartTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.RefreshConfigTableFreq).HasColumnName("refreshConfigTableFreq");

                entity.Property(e => e.RouteId).HasColumnName("routeId");

                entity.Property(e => e.ShouldFlushAfter).HasColumnName("shouldFlushAfter");

                entity.Property(e => e.ShouldRefreshAfter).HasColumnName("shouldRefreshAfter");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("updatedDate")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.App)
                    .WithMany(p => p.CfgParameters)
                    .HasForeignKey(d => d.AppId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_cfgParameters_cfgApplication");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.CfgParameters)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_cfgParameters_cfgLocation");

                entity.HasOne(d => d.Route)
                    .WithMany(p => p.CfgParameters)
                    .HasForeignKey(d => d.RouteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_cfgParameters_cfgRoute");
            });

            modelBuilder.Entity<CfgRoute>(entity =>
            {
                entity.HasKey(e => e.RouteId);

                entity.ToTable("cfgRoute");

                entity.Property(e => e.RouteId).HasColumnName("routeId");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.LocationId).HasColumnName("locationId");

                entity.Property(e => e.RouteNumber)
                    .IsRequired()
                    .HasColumnName("routeNumber")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("updatedDate")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.CfgRoute)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_cfgRoute_cfgLocation");
            });

        }
    }
}
