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

        public virtual DbSet<CategoryCase> CategoryCases { get; set; }
        public virtual DbSet<CategoryCaseFan> CategoryCaseFans { get; set; }
        public virtual DbSet<CategoryCpu> CategoryCpus { get; set; }
        public virtual DbSet<CategoryCpuCooler> CategoryCpuCoolers { get; set; }
        public virtual DbSet<CategoryGpu> CategoryGpus { get; set; }
        public virtual DbSet<CategoryHdd> CategoryHdds { get; set; }
        public virtual DbSet<CategoryMotherboard> CategoryMotherboards { get; set; }
        public virtual DbSet<CategoryPsu> CategoryPsus { get; set; }
        public virtual DbSet<CategoryRam> CategoryRams { get; set; }
        public virtual DbSet<CategorySsd> CategorySsds { get; set; }
        public virtual DbSet<Configuration> Configurations { get; set; }
        public virtual DbSet<ConfigurationList> ConfigurationLists { get; set; }
        public virtual DbSet<OrderCart> OrderCarts { get; set; }
        public virtual DbSet<OrderInfo> OrderInfos { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductType> ProductTypes { get; set; }
        public virtual DbSet<SpecCaseFormFactor> SpecCaseFormFactors { get; set; }
        public virtual DbSet<SpecCaseMaterial> SpecCaseMaterials { get; set; }
        public virtual DbSet<SpecColor> SpecColors { get; set; }
        public virtual DbSet<SpecCoolerType> SpecCoolerTypes { get; set; }
        public virtual DbSet<SpecCpuFamily> SpecCpuFamilies { get; set; }
        public virtual DbSet<SpecCpuPackaging> SpecCpuPackagings { get; set; }
        public virtual DbSet<SpecCpuPinPowering> SpecCpuPinPowerings { get; set; }
        public virtual DbSet<SpecCpuSeries> SpecCpuSeries { get; set; }
        public virtual DbSet<SpecDirectxType> SpecDirectxTypes { get; set; }
        public virtual DbSet<SpecFanSize> SpecFanSizes { get; set; }
        public virtual DbSet<SpecGpuChipset> SpecGpuChipsets { get; set; }
        public virtual DbSet<SpecGpuConnectorType> SpecGpuConnectorTypes { get; set; }
        public virtual DbSet<SpecGpuPinPowering> SpecGpuPinPowerings { get; set; }
        public virtual DbSet<SpecHddFormFactor> SpecHddFormFactors { get; set; }
        public virtual DbSet<SpecHddInterface> SpecHddInterfaces { get; set; }
        public virtual DbSet<SpecManufacturer> SpecManufacturers { get; set; }
        public virtual DbSet<SpecMotherboardChipset> SpecMotherboardChipsets { get; set; }
        public virtual DbSet<SpecMotherboardFormFactor> SpecMotherboardFormFactors { get; set; }
        public virtual DbSet<SpecPciex16version> SpecPciex16versions { get; set; }
        public virtual DbSet<SpecPsuFormFactor> SpecPsuFormFactors { get; set; }
        public virtual DbSet<SpecPsuPlusType> SpecPsuPlusTypes { get; set; }
        public virtual DbSet<SpecRamTechnology> SpecRamTechnologies { get; set; }
        public virtual DbSet<SpecRamType> SpecRamTypes { get; set; }
        public virtual DbSet<SpecSddFormFactor> SpecSddFormFactors { get; set; }
        public virtual DbSet<SpecSocket> SpecSockets { get; set; }
        public virtual DbSet<SpecSsdInterface> SpecSsdInterfaces { get; set; }
        public virtual DbSet<SpecSsdTechnology> SpecSsdTechnologies { get; set; }
        public virtual DbSet<SpecVramType> SpecVramTypes { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<ViewCase> ViewCases { get; set; }
        public virtual DbSet<ViewCaseFan> ViewCaseFans { get; set; }
        public virtual DbSet<ViewCpu> ViewCpus { get; set; }
        public virtual DbSet<ViewCpuCooler> ViewCpuCoolers { get; set; }
        public virtual DbSet<ViewGpu> ViewGpus { get; set; }
        public virtual DbSet<ViewHdd> ViewHdds { get; set; }
        public virtual DbSet<ViewMotherboard> ViewMotherboards { get; set; }
        public virtual DbSet<ViewPsu> ViewPsus { get; set; }
        public virtual DbSet<ViewRam> ViewRams { get; set; }
        public virtual DbSet<ViewSsd> ViewSsds { get; set; }

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

            modelBuilder.Entity<CategoryCase>(entity =>
            {
                entity.ToTable("categoryCases");

                entity.Property(e => e.ESata).HasColumnName("eSATA");

                entity.HasOne(d => d.ColorNavigation)
                    .WithMany(p => p.CategoryCases)
                    .HasForeignKey(d => d.Color)
                    .HasConstraintName("FK_categoryCases_specColors");

                entity.HasOne(d => d.FormFactorNavigation)
                    .WithMany(p => p.CategoryCases)
                    .HasForeignKey(d => d.FormFactor)
                    .HasConstraintName("FK_categoryCases_specCaseFormFactors");

                entity.HasOne(d => d.ManufacturerNavigation)
                    .WithMany(p => p.CategoryCases)
                    .HasForeignKey(d => d.Manufacturer)
                    .HasConstraintName("FK_categoryCases_specManufacturers");

                entity.HasOne(d => d.MaterialNavigation)
                    .WithMany(p => p.CategoryCases)
                    .HasForeignKey(d => d.Material)
                    .HasConstraintName("FK_categoryCases_specCaseMaterials");

                entity.HasOne(d => d.MotherboardFormFactorNavigation)
                    .WithMany(p => p.CategoryCases)
                    .HasForeignKey(d => d.MotherboardFormFactor)
                    .HasConstraintName("FK_categoryCases_specMotherboardFormFactors");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.CategoryCases)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cases_Products");
            });

            modelBuilder.Entity<CategoryCaseFan>(entity =>
            {
                entity.ToTable("categoryCaseFan");

                entity.HasOne(d => d.FanSizeNavigation)
                    .WithMany(p => p.CategoryCaseFans)
                    .HasForeignKey(d => d.FanSize)
                    .HasConstraintName("FK_categoryCaseFan_specFanSizes");

                entity.HasOne(d => d.ManufacturerNavigation)
                    .WithMany(p => p.CategoryCaseFans)
                    .HasForeignKey(d => d.Manufacturer)
                    .HasConstraintName("FK_categoryCaseFan_specManufacturers");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.CategoryCaseFans)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CaseFan_Products");
            });

            modelBuilder.Entity<CategoryCpu>(entity =>
            {
                entity.ToTable("categoryCPU");

                entity.Property(e => e.Tdp).HasColumnName("TDP");

                entity.HasOne(d => d.FamilyNavigation)
                    .WithMany(p => p.CategoryCpus)
                    .HasForeignKey(d => d.Family)
                    .HasConstraintName("FK_categoryCPU_specCpuFamily");

                entity.HasOne(d => d.GpuChipsetNavigation)
                    .WithMany(p => p.CategoryCpus)
                    .HasForeignKey(d => d.GpuChipset)
                    .HasConstraintName("FK_categoryCPU_specGpuChipsets");

                entity.HasOne(d => d.ManufacturerNavigation)
                    .WithMany(p => p.CategoryCpus)
                    .HasForeignKey(d => d.Manufacturer)
                    .HasConstraintName("FK_categoryCPU_specManufacturers");

                entity.HasOne(d => d.PackagingNavigation)
                    .WithMany(p => p.CategoryCpus)
                    .HasForeignKey(d => d.Packaging)
                    .HasConstraintName("FK_categoryCPU_specCpuPackaging");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.CategoryCpus)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CPU_Products");

                entity.HasOne(d => d.RamTechnologyNavigation)
                    .WithMany(p => p.CategoryCpus)
                    .HasForeignKey(d => d.RamTechnology)
                    .HasConstraintName("FK_categoryCPU_specRamTechnology");

                entity.HasOne(d => d.SeriesNavigation)
                    .WithMany(p => p.CategoryCpus)
                    .HasForeignKey(d => d.Series)
                    .HasConstraintName("FK_categoryCPU_specCpuSeries");

                entity.HasOne(d => d.SocketNavigation)
                    .WithMany(p => p.CategoryCpus)
                    .HasForeignKey(d => d.Socket)
                    .HasConstraintName("FK_categoryCPU_specSockets");
            });

            modelBuilder.Entity<CategoryCpuCooler>(entity =>
            {
                entity.ToTable("categoryCpuCooler");

                entity.Property(e => e.Tdp).HasColumnName("TDP");

                entity.HasOne(d => d.CoolerTypeNavigation)
                    .WithMany(p => p.CategoryCpuCoolers)
                    .HasForeignKey(d => d.CoolerType)
                    .HasConstraintName("FK_categoryCpuCooler_specCoolerTypes");

                entity.HasOne(d => d.ManufacturerNavigation)
                    .WithMany(p => p.CategoryCpuCoolers)
                    .HasForeignKey(d => d.Manufacturer)
                    .HasConstraintName("FK_categoryCpuCooler_specManufacturers");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.CategoryCpuCoolers)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CpuCooler_Products");

                entity.HasOne(d => d.SocketNavigation)
                    .WithMany(p => p.CategoryCpuCoolers)
                    .HasForeignKey(d => d.Socket)
                    .HasConstraintName("FK_categoryCpuCooler_specSockets");
            });

            modelBuilder.Entity<CategoryGpu>(entity =>
            {
                entity.ToTable("categoryGPU");

                entity.Property(e => e.Hdmi).HasColumnName("HDMI");

                entity.Property(e => e.Vga).HasColumnName("VGA");

                entity.HasOne(d => d.ConnectorNavigation)
                    .WithMany(p => p.CategoryGpus)
                    .HasForeignKey(d => d.Connector)
                    .HasConstraintName("FK_categoryGPU_specGpuConnectorTypes");

                entity.HasOne(d => d.DirectXNavigation)
                    .WithMany(p => p.CategoryGpus)
                    .HasForeignKey(d => d.DirectX)
                    .HasConstraintName("FK_categoryGPU_specDirectxTypes");

                entity.HasOne(d => d.GpuChipsetNavigation)
                    .WithMany(p => p.CategoryGpus)
                    .HasForeignKey(d => d.GpuChipset)
                    .HasConstraintName("FK_categoryGPU_specGpuChipsets");

                entity.HasOne(d => d.GpuManufacturerNavigation)
                    .WithMany(p => p.CategoryGpuGpuManufacturerNavigations)
                    .HasForeignKey(d => d.GpuManufacturer)
                    .HasConstraintName("FK_categoryGPU_specManufacturers1");

                entity.HasOne(d => d.GpuPinPoweringNavigation)
                    .WithMany(p => p.CategoryGpus)
                    .HasForeignKey(d => d.GpuPinPowering)
                    .HasConstraintName("FK_categoryGPU_specGpuPinPowering");

                entity.HasOne(d => d.ManufacturerNavigation)
                    .WithMany(p => p.CategoryGpuManufacturerNavigations)
                    .HasForeignKey(d => d.Manufacturer)
                    .HasConstraintName("FK_categoryGPU_specManufacturers");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.CategoryGpus)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GPU_Products");

                entity.HasOne(d => d.VramTypeNavigation)
                    .WithMany(p => p.CategoryGpus)
                    .HasForeignKey(d => d.VramType)
                    .HasConstraintName("FK_categoryGPU_specVramType");
            });

            modelBuilder.Entity<CategoryHdd>(entity =>
            {
                entity.ToTable("categoryHDD");

                entity.HasOne(d => d.FormFactorNavigation)
                    .WithMany(p => p.CategoryHdds)
                    .HasForeignKey(d => d.FormFactor)
                    .HasConstraintName("FK_categoryHDD_specHddFormFactor");

                entity.HasOne(d => d.InterfaceNavigation)
                    .WithMany(p => p.CategoryHdds)
                    .HasForeignKey(d => d.Interface)
                    .HasConstraintName("FK_categoryHDD_specHddInterface");

                entity.HasOne(d => d.ManufacturerNavigation)
                    .WithMany(p => p.CategoryHdds)
                    .HasForeignKey(d => d.Manufacturer)
                    .HasConstraintName("FK_categoryHDD_specManufacturers");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.CategoryHdds)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HDD_Products");
            });

            modelBuilder.Entity<CategoryMotherboard>(entity =>
            {
                entity.ToTable("categoryMotherboards");

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

                entity.HasOne(d => d.ChipsetNavigation)
                    .WithMany(p => p.CategoryMotherboards)
                    .HasForeignKey(d => d.Chipset)
                    .HasConstraintName("FK_categoryMotherboards_specMotherboardChipset");

                entity.HasOne(d => d.CpuPinPoweringNavigation)
                    .WithMany(p => p.CategoryMotherboards)
                    .HasForeignKey(d => d.CpuPinPowering)
                    .HasConstraintName("FK_categoryMotherboards_specCpuPinPowering");

                entity.HasOne(d => d.FormFactorNavigation)
                    .WithMany(p => p.CategoryMotherboards)
                    .HasForeignKey(d => d.FormFactor)
                    .HasConstraintName("FK_categoryMotherboards_specMotherboardFormFactors");

                entity.HasOne(d => d.ManufacturerNavigation)
                    .WithMany(p => p.CategoryMotherboards)
                    .HasForeignKey(d => d.Manufacturer)
                    .HasConstraintName("FK_categoryMotherboards_specManufacturers");

                entity.HasOne(d => d.Pciex16versionNavigation)
                    .WithMany(p => p.CategoryMotherboards)
                    .HasForeignKey(d => d.Pciex16version)
                    .HasConstraintName("FK_categoryMotherboards_specPCIex16version");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.CategoryMotherboards)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Motherboards_Products");

                entity.HasOne(d => d.RamTechnologyNavigation)
                    .WithMany(p => p.CategoryMotherboards)
                    .HasForeignKey(d => d.RamTechnology)
                    .HasConstraintName("FK_categoryMotherboards_specRamTechnology");

                entity.HasOne(d => d.RamTypeNavigation)
                    .WithMany(p => p.CategoryMotherboards)
                    .HasForeignKey(d => d.RamType)
                    .HasConstraintName("FK_categoryMotherboards_specRamTypes");

                entity.HasOne(d => d.SocketNavigation)
                    .WithMany(p => p.CategoryMotherboards)
                    .HasForeignKey(d => d.Socket)
                    .HasConstraintName("FK_categoryMotherboards_specSockets");
            });

            modelBuilder.Entity<CategoryPsu>(entity =>
            {
                entity.ToTable("categoryPSU");

                entity.Property(e => e.Pciex2).HasColumnName("PCIex2");

                entity.Property(e => e.Pciex24).HasColumnName("PCIex24");

                entity.Property(e => e.Pciex4).HasColumnName("PCIex4");

                entity.Property(e => e.Pciex6).HasColumnName("PCIex6");

                entity.Property(e => e.Pciex8).HasColumnName("PCIex8");

                entity.Property(e => e.Pfc).HasColumnName("PFC");

                entity.Property(e => e.Sata).HasColumnName("SATA");

                entity.HasOne(d => d.FormFactorNavigation)
                    .WithMany(p => p.CategoryPsus)
                    .HasForeignKey(d => d.FormFactor)
                    .HasConstraintName("FK_categoryPSU_specPsuFormFactor");

                entity.HasOne(d => d.ManufacturerNavigation)
                    .WithMany(p => p.CategoryPsus)
                    .HasForeignKey(d => d.Manufacturer)
                    .HasConstraintName("FK_categoryPSU_specManufacturers");

                entity.HasOne(d => d.PlusNavigation)
                    .WithMany(p => p.CategoryPsus)
                    .HasForeignKey(d => d.Plus)
                    .HasConstraintName("FK_categoryPSU_specPsuPlusType");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.CategoryPsus)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PSU_Products");
            });

            modelBuilder.Entity<CategoryRam>(entity =>
            {
                entity.ToTable("categoryRAM");

                entity.HasOne(d => d.ManufacturerNavigation)
                    .WithMany(p => p.CategoryRams)
                    .HasForeignKey(d => d.Manufacturer)
                    .HasConstraintName("FK_categoryRAM_specManufacturers");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.CategoryRams)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RAM_Products");

                entity.HasOne(d => d.RamTechnologyNavigation)
                    .WithMany(p => p.CategoryRams)
                    .HasForeignKey(d => d.RamTechnology)
                    .HasConstraintName("FK_categoryRAM_specRamTechnology");

                entity.HasOne(d => d.RamTypeNavigation)
                    .WithMany(p => p.CategoryRams)
                    .HasForeignKey(d => d.RamType)
                    .HasConstraintName("FK_categoryRAM_specRamTypes");
            });

            modelBuilder.Entity<CategorySsd>(entity =>
            {
                entity.ToTable("categorySSD");

                entity.Property(e => e.Nvme).HasColumnName("NVMe");

                entity.HasOne(d => d.FormFactorNavigation)
                    .WithMany(p => p.CategorySsds)
                    .HasForeignKey(d => d.FormFactor)
                    .HasConstraintName("FK_categorySSD_specSddFormFactor");

                entity.HasOne(d => d.InterfaceNavigation)
                    .WithMany(p => p.CategorySsds)
                    .HasForeignKey(d => d.Interface)
                    .HasConstraintName("FK_categorySSD_specSsdInterface");

                entity.HasOne(d => d.ManufacturerNavigation)
                    .WithMany(p => p.CategorySsds)
                    .HasForeignKey(d => d.Manufacturer)
                    .HasConstraintName("FK_categorySSD_specManufacturers");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.CategorySsds)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_categorySSD_Products");

                entity.HasOne(d => d.TechnologyNavigation)
                    .WithMany(p => p.CategorySsds)
                    .HasForeignKey(d => d.Technology)
                    .HasConstraintName("FK_categorySSD_specSsdTechnology");
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

            modelBuilder.Entity<OrderCart>(entity =>
            {
                entity.ToTable("OrderCart");

                entity.Property(e => e.Amount).HasDefaultValueSql("((1))");

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

                entity.Property(e => e.Address).IsUnicode(false);

                entity.Property(e => e.OrderDate).HasColumnType("date");

                entity.Property(e => e.Token)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.OrderInfos)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderInfo_Users");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Image)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Summary).IsUnicode(false);

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

            modelBuilder.Entity<SpecCaseFormFactor>(entity =>
            {
                entity.ToTable("specCaseFormFactors");

                entity.Property(e => e.Spec)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SpecCaseMaterial>(entity =>
            {
                entity.ToTable("specCaseMaterials");

                entity.Property(e => e.Spec)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SpecColor>(entity =>
            {
                entity.ToTable("specColors");

                entity.Property(e => e.Spec)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SpecCoolerType>(entity =>
            {
                entity.ToTable("specCoolerTypes");

                entity.Property(e => e.Spec)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SpecCpuFamily>(entity =>
            {
                entity.ToTable("specCpuFamily");

                entity.Property(e => e.Spec)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SpecCpuPackaging>(entity =>
            {
                entity.ToTable("specCpuPackaging");

                entity.Property(e => e.Spec)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SpecCpuPinPowering>(entity =>
            {
                entity.ToTable("specCpuPinPowering");

                entity.Property(e => e.Spec)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SpecCpuSeries>(entity =>
            {
                entity.ToTable("specCpuSeries");

                entity.Property(e => e.Spec)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SpecDirectxType>(entity =>
            {
                entity.ToTable("specDirectxTypes");

                entity.Property(e => e.Spec)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SpecFanSize>(entity =>
            {
                entity.ToTable("specFanSizes");

                entity.Property(e => e.Spec)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SpecGpuChipset>(entity =>
            {
                entity.ToTable("specGpuChipsets");

                entity.Property(e => e.Spec)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SpecGpuConnectorType>(entity =>
            {
                entity.ToTable("specGpuConnectorTypes");

                entity.Property(e => e.Spec)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SpecGpuPinPowering>(entity =>
            {
                entity.ToTable("specGpuPinPowering");

                entity.Property(e => e.Spec)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SpecHddFormFactor>(entity =>
            {
                entity.ToTable("specHddFormFactor");

                entity.Property(e => e.Spec)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SpecHddInterface>(entity =>
            {
                entity.ToTable("specHddInterface");

                entity.Property(e => e.Spec)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SpecManufacturer>(entity =>
            {
                entity.ToTable("specManufacturers");

                entity.Property(e => e.Spec)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SpecMotherboardChipset>(entity =>
            {
                entity.ToTable("specMotherboardChipset");

                entity.Property(e => e.Spec)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SpecMotherboardFormFactor>(entity =>
            {
                entity.ToTable("specMotherboardFormFactors");

                entity.Property(e => e.Spec)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SpecPciex16version>(entity =>
            {
                entity.ToTable("specPCIex16version");

                entity.Property(e => e.Spec)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SpecPsuFormFactor>(entity =>
            {
                entity.ToTable("specPsuFormFactor");

                entity.Property(e => e.Spec)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SpecPsuPlusType>(entity =>
            {
                entity.ToTable("specPsuPlusType");

                entity.Property(e => e.Spec)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SpecRamTechnology>(entity =>
            {
                entity.ToTable("specRamTechnology");

                entity.Property(e => e.Spec)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SpecRamType>(entity =>
            {
                entity.ToTable("specRamTypes");

                entity.Property(e => e.Spec)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SpecSddFormFactor>(entity =>
            {
                entity.ToTable("specSddFormFactor");

                entity.Property(e => e.Spec)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SpecSocket>(entity =>
            {
                entity.ToTable("specSockets");

                entity.Property(e => e.Spec)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SpecSsdInterface>(entity =>
            {
                entity.ToTable("specSsdInterface");

                entity.Property(e => e.Spec)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SpecSsdTechnology>(entity =>
            {
                entity.ToTable("specSsdTechnology");

                entity.Property(e => e.Spec)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SpecVramType>(entity =>
            {
                entity.ToTable("specVramType");

                entity.Property(e => e.Spec)
                    .HasMaxLength(100)
                    .IsUnicode(false);
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
                    .HasMaxLength(100)
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
                entity.HasIndex(e => e.RoleName, "UK_UserRoles_RoleName")
                    .IsUnique();

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ViewCase>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewCases");

                entity.Property(e => e.Color)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.ESata).HasColumnName("eSATA");

                entity.Property(e => e.FormFactor)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Image)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Manufacturer)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Material)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.MotherboardFormFactor)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Summary).IsUnicode(false);
            });

            modelBuilder.Entity<ViewCaseFan>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewCaseFan");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.FanSize)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Image)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Manufacturer)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Summary).IsUnicode(false);
            });

            modelBuilder.Entity<ViewCpu>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewCPU");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Family)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.GpuChipset)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Image)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Manufacturer)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Packaging)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RamTechnology)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Series)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Socket)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Summary).IsUnicode(false);

                entity.Property(e => e.Tdp).HasColumnName("TDP");
            });

            modelBuilder.Entity<ViewCpuCooler>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewCpuCooler");

                entity.Property(e => e.CoolerType)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Image)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Manufacturer)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Socket)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Summary).IsUnicode(false);

                entity.Property(e => e.Tdp).HasColumnName("TDP");
            });

            modelBuilder.Entity<ViewGpu>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewGPU");

                entity.Property(e => e.Connector)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DirectX)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.GpuChipset)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.GpuManufacturer)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.GpuPinPowering)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Hdmi).HasColumnName("HDMI");

                entity.Property(e => e.Image)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Manufacturer)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Summary).IsUnicode(false);

                entity.Property(e => e.Vga).HasColumnName("VGA");

                entity.Property(e => e.VramType)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ViewHdd>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewHDD");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.FormFactor)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Image)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Interface)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Manufacturer)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Summary).IsUnicode(false);
            });

            modelBuilder.Entity<ViewMotherboard>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewMotherboards");

                entity.Property(e => e.Chipset)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CpuPinPowering)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.FormFactor)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Image)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Manufacturer)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Pci).HasColumnName("PCI");

                entity.Property(e => e.Pciex1).HasColumnName("PCIex1");

                entity.Property(e => e.Pciex16).HasColumnName("PCIex16");

                entity.Property(e => e.Pciex16version)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("PCIex16version");

                entity.Property(e => e.Pciex4).HasColumnName("PCIex4");

                entity.Property(e => e.RamTechnology)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RamTypes)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Sata2).HasColumnName("SATA2");

                entity.Property(e => e.Sata2raid).HasColumnName("SATA2RAID");

                entity.Property(e => e.Sata3).HasColumnName("SATA3");

                entity.Property(e => e.Sata3raid).HasColumnName("SATA3RAID");

                entity.Property(e => e.Sli).HasColumnName("SLI");

                entity.Property(e => e.Socket)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Summary).IsUnicode(false);
            });

            modelBuilder.Entity<ViewPsu>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewPSU");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.FormFactor)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Image)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Manufacturer)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Pciex2).HasColumnName("PCIex2");

                entity.Property(e => e.Pciex24).HasColumnName("PCIex24");

                entity.Property(e => e.Pciex4).HasColumnName("PCIex4");

                entity.Property(e => e.Pciex6).HasColumnName("PCIex6");

                entity.Property(e => e.Pciex8).HasColumnName("PCIex8");

                entity.Property(e => e.Pfc).HasColumnName("PFC");

                entity.Property(e => e.Plus)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Sata).HasColumnName("SATA");

                entity.Property(e => e.Summary).IsUnicode(false);
            });

            modelBuilder.Entity<ViewRam>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewRAM");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Image)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Manufacturer)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RamTechnology)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RamType)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Summary).IsUnicode(false);
            });

            modelBuilder.Entity<ViewSsd>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewSSD");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Image)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Manufacturer)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nvme).HasColumnName("NVMe");

                entity.Property(e => e.SsdFormFactor)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SsdInterface)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SsdTechnology)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Summary).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
