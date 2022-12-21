using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace JMC.Portal.Business.SmartGlanceModel
{
    public partial class SmartGlanceDataEntities : DbContext
    {
        public SmartGlanceDataEntities()
        {
        }

        public SmartGlanceDataEntities(DbContextOptions<SmartGlanceDataEntities> options)
            : base(options)
        {
        }

        public virtual DbSet<AtlasMillsCurrentShiftSummary> AtlasMillsCurrentShiftSummaries { get; set; } = null!;
        public virtual DbSet<AtlasMillsSalesOverview> AtlasMillsSalesOverviews { get; set; } = null!;
        public virtual DbSet<CurrentShiftOee> CurrentShiftOees { get; set; } = null!;
        public virtual DbSet<EntStat> EntStats { get; set; } = null!;
        public virtual DbSet<PlantStat> PlantStats { get; set; } = null!;
        public virtual DbSet<ShiftHistory> ShiftHistories { get; set; } = null!;
        public virtual DbSet<VwZekDashCat> VwZekDashCats { get; set; } = null!;
        public virtual DbSet<VwZekDashCat1> VwZekDashCats1 { get; set; } = null!;
        public virtual DbSet<VwZekDashInfo> VwZekDashInfos { get; set; } = null!;
        public virtual DbSet<VwZekDashInfoCat> VwZekDashInfoCats { get; set; } = null!;
        public virtual DbSet<VwZekDashInfoRpt> VwZekDashInfoRpts { get; set; } = null!;
        public virtual DbSet<VwZekDashInfoRptsCfg> VwZekDashInfoRptsCfgs { get; set; } = null!;
        public virtual DbSet<VwZekDashRpt> VwZekDashRpts { get; set; } = null!;
        public virtual DbSet<VwZekDashRpt1> VwZekDashRpts1 { get; set; } = null!;
        public virtual DbSet<VwZekDashSvr> VwZekDashSvrs { get; set; } = null!;
        public virtual DbSet<VwZekDashSvr1> VwZekDashSvrs1 { get; set; } = null!;
        public virtual DbSet<VwZekOvwIncWeb> VwZekOvwIncWebs { get; set; } = null!;
        public virtual DbSet<VwZekOvwWeb> VwZekOvwWebs { get; set; } = null!;
        public virtual DbSet<WtOverviewWeb> WtOverviewWebs { get; set; } = null!;
        public virtual DbSet<ZekAdminMap> ZekAdminMaps { get; set; } = null!;
        public virtual DbSet<ZekAtlasEquipmentDashboard> ZekAtlasEquipmentDashboards { get; set; } = null!;
        public virtual DbSet<ZekAtlasIncentiveDashboard> ZekAtlasIncentiveDashboards { get; set; } = null!;
        public virtual DbSet<ZekDashAtr> ZekDashAtrs { get; set; } = null!;
        public virtual DbSet<ZekDashCat> ZekDashCats { get; set; } = null!;
        public virtual DbSet<ZekDashObjChk> ZekDashObjChks { get; set; } = null!;
        public virtual DbSet<ZekDashRpt> ZekDashRpts { get; set; } = null!;
        public virtual DbSet<ZekDashSvr> ZekDashSvrs { get; set; } = null!;
        public virtual DbSet<ZekDashUsr> ZekDashUsrs { get; set; } = null!;
        public virtual DbSet<ZekThinManagerCfg> ZekThinManagerCfgs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=JMAD-SMRTGL-P;Initial Catalog=SmartGlanceData;User ID=PortalUser;Password=JMCp@ssword;MultipleActiveResultSets=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AtlasMillsCurrentShiftSummary>(entity =>
            {
                entity.HasKey(e => new { e.Division, e.Plant, e.Equipment });
            });

            modelBuilder.Entity<AtlasMillsSalesOverview>(entity =>
            {
                entity.HasKey(e => new { e.Plant, e.Equipment });
            });

            modelBuilder.Entity<CurrentShiftOee>(entity =>
            {
                entity.HasKey(e => new { e.ServerName, e.LastUpdatedUtc, e.EntName });
            });

            modelBuilder.Entity<EntStat>(entity =>
            {
                entity.ToView("ent_stats");
            });

            modelBuilder.Entity<PlantStat>(entity =>
            {
                entity.ToView("plant_stats");
            });

            modelBuilder.Entity<ShiftHistory>(entity =>
            {
                entity.HasKey(e => new { e.ServerName, e.LastUpdatedUtc, e.EntName, e.ShiftStartLocal });
            });

            modelBuilder.Entity<VwZekDashCat>(entity =>
            {
                entity.ToView("vw_ZEK_DashCat");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<VwZekDashCat1>(entity =>
            {
                entity.ToView("vw_ZEK_DashCats");

                entity.Property(e => e.Cid).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<VwZekDashInfo>(entity =>
            {
                entity.ToView("vw_ZEK_Dash_Info");
            });

            modelBuilder.Entity<VwZekDashInfoCat>(entity =>
            {
                entity.ToView("vw_ZEK_Dash_Info_Cats");
            });

            modelBuilder.Entity<VwZekDashInfoRpt>(entity =>
            {
                entity.ToView("vw_ZEK_Dash_Info_Rpts");
            });

            modelBuilder.Entity<VwZekDashInfoRptsCfg>(entity =>
            {
                entity.ToView("vw_ZEK_Dash_Info_Rpts_Cfg");
            });

            modelBuilder.Entity<VwZekDashRpt>(entity =>
            {
                entity.ToView("vw_ZEK_DashRpt");
            });

            modelBuilder.Entity<VwZekDashRpt1>(entity =>
            {
                entity.ToView("vw_ZEK_DashRpts");
            });

            modelBuilder.Entity<VwZekDashSvr>(entity =>
            {
                entity.ToView("vw_ZEK_DashSvr");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<VwZekDashSvr1>(entity =>
            {
                entity.ToView("vw_ZEK_DashSvrs");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<VwZekOvwIncWeb>(entity =>
            {
                entity.ToView("vw_ZEK_Ovw_Inc_Web");

                entity.Property(e => e.RowId).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<VwZekOvwWeb>(entity =>
            {
                entity.ToView("vw_ZEK_Ovw_Web");

                entity.Property(e => e.RowId).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<WtOverviewWeb>(entity =>
            {
                entity.HasKey(e => new { e.Svr, e.EntId });
            });

            modelBuilder.Entity<ZekAdminMap>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<ZekThinManagerCfg>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
