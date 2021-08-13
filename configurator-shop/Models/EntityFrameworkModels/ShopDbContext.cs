using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace configurator_shop.Models.EntityFrameworkModels
{
    public partial class ShopDbContext : DbContext
    {
        public ShopDbContext()
        {
        }

        public ShopDbContext(DbContextOptions<ShopDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Case> Cases { get; set; }
        public virtual DbSet<CaseFan> CaseFans { get; set; }
        public virtual DbSet<Configuration> Configurations { get; set; }
        public virtual DbSet<ConfigurationList> ConfigurationLists { get; set; }
        public virtual DbSet<Cpu> Cpus { get; set; }
        public virtual DbSet<CpuCooler> CpuCoolers { get; set; }
        public virtual DbSet<Gpu> Gpus { get; set; }
        public virtual DbSet<Hdd> Hdds { get; set; }
        public virtual DbSet<Motherboard> Motherboards { get; set; }
        public virtual DbSet<OrderCart> OrderCarts { get; set; }
        public virtual DbSet<OrderInfo> OrderInfos { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductType> ProductTypes { get; set; }
        public virtual DbSet<Psu> Psus { get; set; }
        public virtual DbSet<Ram> Rams { get; set; }
        public virtual DbSet<Ssd> Ssds { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ConnectionStrings:ShopDatabase");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Case>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.ESata).HasColumnName("eSATA");
            });

            modelBuilder.Entity<CaseFan>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CaseFan");
            });

            modelBuilder.Entity<Configuration>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Summary).IsUnicode(false);

                entity.Property(e => e.UserId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Configurations)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Configurations_Users");
            });

            modelBuilder.Entity<ConfigurationList>(entity =>
            {
                entity.ToTable("ConfigurationList");

                entity.HasOne(d => d.Conf)
                    .WithMany(p => p.ConfigurationLists)
                    .HasForeignKey(d => d.ConfId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ConfigurationList_Configurations");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ConfigurationLists)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ConfigurationList_Products");
            });

            modelBuilder.Entity<Cpu>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CPU");

                entity.Property(e => e.Gpu).HasColumnName("GPU");

                entity.Property(e => e.Tdp).HasColumnName("TDP");
            });

            modelBuilder.Entity<CpuCooler>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CpuCooler");

                entity.Property(e => e.Tdp).HasColumnName("TDP");
            });

            modelBuilder.Entity<Gpu>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("GPU");

                entity.Property(e => e.Gpu1).HasColumnName("GPU");

                entity.Property(e => e.Hdmi).HasColumnName("HDMI");

                entity.Property(e => e.Vga).HasColumnName("VGA");
            });

            modelBuilder.Entity<Hdd>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("HDD");
            });

            modelBuilder.Entity<Motherboard>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Pci).HasColumnName("PCI");

                entity.Property(e => e.Pciex1).HasColumnName("PCIex1");

                entity.Property(e => e.Pciex16).HasColumnName("PCIex16");

                entity.Property(e => e.Pciex16version).HasColumnName("PCIex16version");

                entity.Property(e => e.Pciex4).HasColumnName("PCIex4");

                entity.Property(e => e.Sata2).HasColumnName("SATA2");

                entity.Property(e => e.Sata2raid).HasColumnName("SATA2RAID");

                entity.Property(e => e.Sata3).HasColumnName("SATA3");

                entity.Property(e => e.Sata3raid).HasColumnName("SATA3RAID");

                entity.Property(e => e.Sli).HasColumnName("SLI");
            });

            modelBuilder.Entity<OrderCart>(entity =>
            {
                entity.ToTable("OrderCart");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderCarts)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderCart_OrderInfo");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderCarts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderCart_Products");
            });

            modelBuilder.Entity<OrderInfo>(entity =>
            {
                entity.ToTable("OrderInfo");

                entity.Property(e => e.OrderDate).HasColumnType("date");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.OrderInfos)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderInfo_Users");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(e => e.Sku, "UK_Products_SKU")
                    .IsUnique();

                entity.Property(e => e.Description)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sku)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SKU");

                entity.Property(e => e.Summary)
                    .IsUnicode(false)
                    .HasColumnName("summary");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Products_ProductTypes");
            });

            modelBuilder.Entity<ProductType>(entity =>
            {
                entity.HasIndex(e => e.Type, "UK_ProductTypes_Type")
                    .IsUnique();

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Psu>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("PSU");

                entity.Property(e => e.Pciex6).HasColumnName("PCIex6");

                entity.Property(e => e.Pciex8).HasColumnName("PCIex8");

                entity.Property(e => e.Pfc).HasColumnName("PFC");

                entity.Property(e => e.Sata).HasColumnName("SATA");
            });

            modelBuilder.Entity<Ram>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("RAM");
            });

            modelBuilder.Entity<Ssd>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("SSD");

                entity.Property(e => e.Nvme).HasColumnName("NVMe");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email, "UK_Users_Email")
                    .IsUnique();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Tel)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Token)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_UserRoles");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasIndex(e => e.RoleName, "UK_UserRoles_Role")
                    .IsUnique();

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
