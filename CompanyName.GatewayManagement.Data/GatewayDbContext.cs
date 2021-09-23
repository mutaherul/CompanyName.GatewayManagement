using Microsoft.EntityFrameworkCore;
using CompanyName.GatewayManagement.Data.Entities;

#nullable disable

namespace CompanyName.GatewayManagement.Data
{
    public partial class GatewayDbContext : DbContext
    {
        public GatewayDbContext()
        {
        }

        public GatewayDbContext(DbContextOptions<GatewayDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Gateway> Gateways { get; set; }
        public virtual DbSet<PeripheralDevice> PeripheralDevices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Gateway>(entity =>
            {
                entity.ToTable("Gateway");

                entity.Property(e => e.AddressIpv4)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.GatewayName)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("('FALSE')");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.SerialNumber).HasDefaultValueSql("(newsequentialid())");
            });

            modelBuilder.Entity<PeripheralDevice>(entity =>
            {
                entity.HasKey(e => e.Uid);

                entity.ToTable("PeripheralDevice");

                entity.Property(e => e.Uid).HasColumnName("UID");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("('FALSE')");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.VendorName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.HasOne(d => d.Gateway)
                    .WithMany(p => p.PeripheralDevices)
                    .HasForeignKey(d => d.GatewayId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PeripheralDevice_Gateway");
            });

            base.OnModelCreating(modelBuilder);
        }


    }
}
