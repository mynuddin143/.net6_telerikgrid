using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace JMC.SlitterOptimization.Business.SlitterOptimizationEntities
{
    public partial class SlitterOptimizationContext : DbContext
    {
        public SlitterOptimizationContext()
        {
        }

        public SlitterOptimizationContext(DbContextOptions<SlitterOptimizationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MApoRollingScheduleImport> MApoRollingScheduleImports { get; set; } = null!;
        public virtual DbSet<MBandDelivery> MBandDeliveries { get; set; } = null!;
        public virtual DbSet<MBandInventory> MBandInventories { get; set; } = null!;
        public virtual DbSet<MBandMaster> MBandMasters { get; set; } = null!;
        public virtual DbSet<MBandWarehouse> MBandWarehouses { get; set; } = null!;
        public virtual DbSet<MCoilInventory> MCoilInventories { get; set; } = null!;
        public virtual DbSet<MCoilMaster> MCoilMasters { get; set; } = null!;
        public virtual DbSet<MCoilOrder> MCoilOrders { get; set; } = null!;
        public virtual DbSet<MDefinedDrop> MDefinedDrops { get; set; } = null!;
        public virtual DbSet<MFamilySchedule> MFamilySchedules { get; set; } = null!;
        public virtual DbSet<MItemMaster> MItemMasters { get; set; } = null!;
        public virtual DbSet<MItemMasterOld> MItemMasterOlds { get; set; } = null!;
        public virtual DbSet<MMillWarehouse> MMillWarehouses { get; set; } = null!;
        public virtual DbSet<MPlanningProduct> MPlanningProducts { get; set; } = null!;
        public virtual DbSet<MSlitOrder> MSlitOrders { get; set; } = null!;
        public virtual DbSet<MSlitPatternPriorityExtract> MSlitPatternPriorityExtracts { get; set; } = null!;
        public virtual DbSet<MSteelOrder> MSteelOrders { get; set; } = null!;
        public virtual DbSet<MTandemSchedule> MTandemSchedules { get; set; } = null!;
        public virtual DbSet<MTubeGradeGaugeXref> MTubeGradeGaugeXrefs { get; set; } = null!;
        public virtual DbSet<MTubeMaster> MTubeMasters { get; set; } = null!;
        public virtual DbSet<MTubeSubGrade> MTubeSubGrades { get; set; } = null!;

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Data Source=JMCWEBDEV\\DEV\n;Initial Catalog=SlitterOptimization;User ID=SOUser;Password=p@ssw0rd\n;MultipleActiveResultSets=True;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MBandDelivery>(entity =>
            {
                entity.Property(e => e.DeliveryNo).ValueGeneratedNever();

                entity.Property(e => e.DestWarehouse).IsFixedLength();

                entity.Property(e => e.Vendor).IsFixedLength();
            });

            modelBuilder.Entity<MBandInventory>(entity =>
            {
                entity.HasKey(e => e.TrackId)
                    .IsClustered(false);

                entity.Property(e => e.BandSequence).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<MBandMaster>(entity =>
            {
                entity.HasKey(e => e.BandItem)
                    .HasName("PK_M_BandMaster_1");

                entity.Property(e => e.MinNom).IsFixedLength();
            });

            modelBuilder.Entity<MBandWarehouse>(entity =>
            {
                entity.HasKey(e => new { e.Plant, e.Warehouse });

                entity.Property(e => e.Plant).IsFixedLength();

                entity.Property(e => e.Warehouse).IsFixedLength();
            });

            modelBuilder.Entity<MCoilInventory>(entity =>
            {
                entity.HasKey(e => e.TrackId)
                    .IsClustered(false);

                entity.Property(e => e.CoilSequence).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<MCoilMaster>(entity =>
            {
                entity.HasKey(e => new { e.CoilItem, e.TubeGrade, e.TubeFamily, e.CoilGaugeCode });
            });

            modelBuilder.Entity<MCoilOrder>(entity =>
            {
                entity.HasKey(e => new { e.CoilItem, e.Ponum, e.CurReqDate });
            });

            modelBuilder.Entity<MItemMaster>(entity =>
            {
                entity.HasKey(e => new { e.Plant, e.Routing, e.TubeItem, e.BomcoilItem })
                    .IsClustered(false);

                entity.Property(e => e.TubeSurface).IsFixedLength();
            });

            modelBuilder.Entity<MMillWarehouse>(entity =>
            {
                entity.HasKey(e => new { e.Plant, e.Dept, e.Warehouse });
            });

            modelBuilder.Entity<MPlanningProduct>(entity =>
            {
                entity.HasKey(e => new { e.Plant, e.ResourceName, e.PlanningMaterial });

                entity.Property(e => e.LastBlockAllowed).IsFixedLength();
            });

            modelBuilder.Entity<MSlitOrder>(entity =>
            {
                entity.HasKey(e => new { e.BandItem, e.CoilItem, e.MOrder })
                    .IsClustered(false);

                entity.Property(e => e.MOrderNo).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<MSlitPatternPriorityExtract>(entity =>
            {
                entity.HasKey(e => new { e.PrimSlitCoil, e.Plant, e.Priority });
            });

            modelBuilder.Entity<MSteelOrder>(entity =>
            {
                entity.HasKey(e => new { e.BandItem, e.Ponum, e.CurReqDate });
            });

            modelBuilder.Entity<MTandemSchedule>(entity =>
            {
                entity.HasKey(e => new { e.Plant, e.Dept, e.TubeItem, e.FirstStartDate });
            });

            modelBuilder.Entity<MTubeMaster>(entity =>
            {
                entity.HasKey(e => e.TubeItem)
                    .IsClustered(false);

                entity.Property(e => e.TubeDesc).IsFixedLength();

                entity.Property(e => e.TubeFamily).IsFixedLength();

                entity.Property(e => e.TubeGaugeCode).IsFixedLength();

                entity.Property(e => e.TubeSurface).IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
