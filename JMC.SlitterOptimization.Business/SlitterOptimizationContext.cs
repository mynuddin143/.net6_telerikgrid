using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace JMC.SlitterOptimization.Business
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

        public virtual DbSet<M_APO_Rolling_Schedule_Import> MApoRollingScheduleImports { get; set; } = null!;
        public virtual DbSet<M_BandDeliveries> MBandDeliveries { get; set; } = null!;
        public virtual DbSet<M_BandInventory> MBandInventories { get; set; } = null!;
        public virtual DbSet<M_BandMaster> MBandMasters { get; set; } = null!;
        public virtual DbSet<M_BandWarehouse> MBandWarehouses { get; set; } = null!;
        public virtual DbSet<M_CoilInventory> MCoilInventories { get; set; } = null!;
        public virtual DbSet<M_CoilMaster> MCoilMasters { get; set; } = null!;
        public virtual DbSet<M_CoilOrders> MCoilOrders { get; set; } = null!;
        public virtual DbSet<M_DefinedDrops> MDefinedDrops { get; set; } = null!;
        public virtual DbSet<M_FamilySchedule> MFamilySchedules { get; set; } = null!;
        public virtual DbSet<M_ItemMaster> MItemMasters { get; set; } = null!;
        public virtual DbSet<M_ItemMasterOld> MItemMasterOlds { get; set; } = null!;
        public virtual DbSet<M_MillWarehouse> MMillWarehouses { get; set; } = null!;
        public virtual DbSet<M_Planning_Product> MPlanningProducts { get; set; } = null!;
        public virtual DbSet<M_SlitOrders> MSlitOrders { get; set; } = null!;
        public virtual DbSet<M_Slit_Pattern_Priority_Extract> MSlitPatternPriorityExtracts { get; set; } = null!;
        public virtual DbSet<M_SteelOrders> MSteelOrders { get; set; } = null!;
        public virtual DbSet<M_TandemSchedule> MTandemSchedules { get; set; } = null!;
        public virtual DbSet<M_TubeGradeGaugeXRef> MTubeGradeGaugeXrefs { get; set; } = null!;
        public virtual DbSet<M_TubeMaster> MTubeMasters { get; set; } = null!;
        public virtual DbSet<M_TubeSubGrade> MTubeSubGrades { get; set; } = null!;

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
            modelBuilder.Entity<M_BandDeliveries>(entity =>
            {
                entity.Property(e => e.DeliveryNo).ValueGeneratedNever();

                entity.Property(e => e.DestWarehouse).IsFixedLength();

                entity.Property(e => e.Vendor).IsFixedLength();
            });

            modelBuilder.Entity<M_BandInventory>(entity =>
            {
                entity.HasKey(e => e.TrackID)
                    .IsClustered(false);

                entity.Property(e => e.BandSequence).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<M_BandMaster>(entity =>
            {
                entity.HasKey(e => e.BandItem)
                    .HasName("PK_M_BandMaster_1");

                entity.Property(e => e.MinNom).IsFixedLength();
            });

            modelBuilder.Entity<M_BandWarehouse>(entity =>
            {
                entity.HasKey(e => new { e.Plant, e.Warehouse });

                entity.Property(e => e.Plant).IsFixedLength();

                entity.Property(e => e.Warehouse).IsFixedLength();
            });

            modelBuilder.Entity<M_CoilInventory>(entity =>
            {
                entity.HasKey(e => e.TrackID)
                    .IsClustered(false);

                entity.Property(e => e.CoilSequence).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<M_CoilMaster>(entity =>
            {
                entity.HasKey(e => new { e.CoilItem, e.TubeGrade, e.TubeFamily, e.CoilGaugeCode });
            });

            modelBuilder.Entity<M_CoilOrders>(entity =>
            {
                entity.HasKey(e => new { e.CoilItem, e.PONum, e.CurReqDate });
            });

            modelBuilder.Entity<M_ItemMaster>(entity =>
            {
                entity.HasKey(e => new { e.Plant, e.Routing, e.TubeItem, e.BOMCoilItem })
                    .IsClustered(false);

                entity.Property(e => e.TubeSurface).IsFixedLength();
            });

            modelBuilder.Entity<M_MillWarehouse>(entity =>
            {
                entity.HasKey(e => new { e.Plant, e.Dept, e.Warehouse });
            });

            modelBuilder.Entity<M_Planning_Product>(entity =>
            {
                entity.HasKey(e => new { e.Plant, e.ResourceName, e.PlanningMaterial });

                entity.Property(e => e.LastBlockAllowed).IsFixedLength();
            });

            modelBuilder.Entity<M_SlitOrders>(entity =>
            {
                entity.HasKey(e => new { e.BandItem, e.CoilItem, e.MOrder })
                    .IsClustered(false);

                entity.Property(e => e.MOrderNo).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<M_Slit_Pattern_Priority_Extract>(entity =>
            {
                entity.HasKey(e => new { e.PrimSlitCoil, e.Plant, e.Priority });
            });

            modelBuilder.Entity<M_SteelOrders>(entity =>
            {
                entity.HasKey(e => new { e.BandItem, e.PONum, e.CurReqDate });
            });

            modelBuilder.Entity<M_TandemSchedule>(entity =>
            {
                entity.HasKey(e => new { e.Plant, e.Dept, e.TubeItem, e.FirstStartDate });
            });

            modelBuilder.Entity<M_TubeMaster>(entity =>
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
