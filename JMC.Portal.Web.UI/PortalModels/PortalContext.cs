using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class PortalContext : DbContext
    {
        public PortalContext()
        {
        }

        public PortalContext(DbContextOptions<PortalContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AlternateSoldTo> AlternateSoldTos { get; set; } = null!;
        public virtual DbSet<Application> Applications { get; set; } = null!;
        public virtual DbSet<ApplicationRole> ApplicationRoles { get; set; } = null!;
        public virtual DbSet<AtpemployeePlant> AtpemployeePlants { get; set; } = null!;
        public virtual DbSet<Box> Boxes { get; set; } = null!;
        public virtual DbSet<City> Cities { get; set; } = null!;
        public virtual DbSet<ClaimRequest> ClaimRequests { get; set; } = null!;
        public virtual DbSet<Cmir> Cmirs { get; set; } = null!;
        public virtual DbSet<Country> Countries { get; set; } = null!;
        public virtual DbSet<DataView> DataViews { get; set; } = null!;
        public virtual DbSet<DataViewColumn> DataViewColumns { get; set; } = null!;
        public virtual DbSet<DealsBySoldToShipTo> DealsBySoldToShipTos { get; set; } = null!;
        public virtual DbSet<DealsDetail> DealsDetails { get; set; } = null!;
        public virtual DbSet<DealsMaterialPricingGroup> DealsMaterialPricingGroups { get; set; } = null!;
        public virtual DbSet<DealsPlant> DealsPlants { get; set; } = null!;
        public virtual DbSet<DealsPricingGroup> DealsPricingGroups { get; set; } = null!;
        public virtual DbSet<DefaultTubeStandard> DefaultTubeStandards { get; set; } = null!;
        public virtual DbSet<DeliveryMethod> DeliveryMethods { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Division> Divisions { get; set; } = null!;
        public virtual DbSet<Document> Documents { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<EmployeePosition> EmployeePositions { get; set; } = null!;
        public virtual DbSet<FiscalPeriod> FiscalPeriods { get; set; } = null!;
        public virtual DbSet<FiscalYear> FiscalYears { get; set; } = null!;
        public virtual DbSet<FreightandFsc> FreightandFscs { get; set; } = null!;
        public virtual DbSet<Gauge> Gauges { get; set; } = null!;
        public virtual DbSet<Ipsfile> Ipsfiles { get; set; } = null!;
        public virtual DbSet<Location> Locations { get; set; } = null!;
        public virtual DbSet<LocationDepartment> LocationDepartments { get; set; } = null!;
        public virtual DbSet<LoginHistory> LoginHistories { get; set; } = null!;
        public virtual DbSet<Mill> Mills { get; set; } = null!;
        public virtual DbSet<MillException> MillExceptions { get; set; } = null!;
        public virtual DbSet<PendingShipmentCost> PendingShipmentCosts { get; set; } = null!;
        public virtual DbSet<PersonDataViewColumn> PersonDataViewColumns { get; set; } = null!;
        public virtual DbSet<PersonDataViewColumnTemplate> PersonDataViewColumnTemplates { get; set; } = null!;
        public virtual DbSet<PersonDataViewTemplate> PersonDataViewTemplates { get; set; } = null!;
        public virtual DbSet<PhoneNumber> PhoneNumbers { get; set; } = null!;
        public virtual DbSet<PipeSize> PipeSizes { get; set; } = null!;
        public virtual DbSet<Plant> Plants { get; set; } = null!;
        public virtual DbSet<PlantComputer> PlantComputers { get; set; } = null!;
        public virtual DbSet<PriceChangeRequest> PriceChangeRequests { get; set; } = null!;
        public virtual DbSet<PriceChangeRequestItem> PriceChangeRequestItems { get; set; } = null!;
        public virtual DbSet<PriceChangeSetting> PriceChangeSettings { get; set; } = null!;
        public virtual DbSet<PriceSheetNote> PriceSheetNotes { get; set; } = null!;
        public virtual DbSet<ProductLine> ProductLines { get; set; } = null!;
        public virtual DbSet<QuickSearchCriterion> QuickSearchCriteria { get; set; } = null!;
        public virtual DbSet<QuickSearchTemplate> QuickSearchTemplates { get; set; } = null!;
        public virtual DbSet<Quote> Quotes { get; set; } = null!;
        public virtual DbSet<QuoteMaterial> QuoteMaterials { get; set; } = null!;
        public virtual DbSet<QuoteMaterialSapcondition> QuoteMaterialSapconditions { get; set; } = null!;
        public virtual DbSet<QuoteSapcondition> QuoteSapconditions { get; set; } = null!;
        public virtual DbSet<QuoteStatus> QuoteStatuses { get; set; } = null!;
        public virtual DbSet<QuoteStatusChange> QuoteStatusChanges { get; set; } = null!;
        public virtual DbSet<RandomLengthSapsoldTo> RandomLengthSapsoldTos { get; set; } = null!;
        public virtual DbSet<Rectangle> Rectangles { get; set; } = null!;
        public virtual DbSet<Rolling> Rollings { get; set; } = null!;
        public virtual DbSet<Round> Rounds { get; set; } = null!;
        public virtual DbSet<SapbundlingOption> SapbundlingOptions { get; set; } = null!;
        public virtual DbSet<SapcharacteristicOption> SapcharacteristicOptions { get; set; } = null!;
        public virtual DbSet<SapcharacteristicType> SapcharacteristicTypes { get; set; } = null!;
        public virtual DbSet<Sapcondition> Sapconditions { get; set; } = null!;
        public virtual DbSet<SapconditionGroup> SapconditionGroups { get; set; } = null!;
        public virtual DbSet<SapcustomerGroup> SapcustomerGroups { get; set; } = null!;
        public virtual DbSet<SapcustomerGroupSapsalesOrganization> SapcustomerGroupSapsalesOrganizations { get; set; } = null!;
        public virtual DbSet<SapcustomerGroupUser> SapcustomerGroupUsers { get; set; } = null!;
        public virtual DbSet<SapcustomerServiceRep> SapcustomerServiceReps { get; set; } = null!;
        public virtual DbSet<SapcustomerServiceRepSapsalesOrganization> SapcustomerServiceRepSapsalesOrganizations { get; set; } = null!;
        public virtual DbSet<SapcustomerText> SapcustomerTexts { get; set; } = null!;
        public virtual DbSet<Sapdelivery> Sapdeliveries { get; set; } = null!;
        public virtual DbSet<SapdeliveryItem> SapdeliveryItems { get; set; } = null!;
        public virtual DbSet<SapdeliveryType> SapdeliveryTypes { get; set; } = null!;
        public virtual DbSet<Sapmaterial> Sapmaterials { get; set; } = null!;
        public virtual DbSet<SapmaterialPlant> SapmaterialPlants { get; set; } = null!;
        public virtual DbSet<SapmaterialSapbundlingOption> SapmaterialSapbundlingOptions { get; set; } = null!;
        public virtual DbSet<SapmaterialUnitOfMeasure> SapmaterialUnitOfMeasures { get; set; } = null!;
        public virtual DbSet<Sapregion> Sapregions { get; set; } = null!;
        public virtual DbSet<SapregionSapsalesOrganization> SapregionSapsalesOrganizations { get; set; } = null!;
        public virtual DbSet<SapsalesDelivery> SapsalesDeliveries { get; set; } = null!;
        public virtual DbSet<SapsalesGroup> SapsalesGroups { get; set; } = null!;
        public virtual DbSet<SapsalesGroupSapsalesOrganization> SapsalesGroupSapsalesOrganizations { get; set; } = null!;
        public virtual DbSet<SapsalesOrder> SapsalesOrders { get; set; } = null!;
        public virtual DbSet<SapsalesOrderItem> SapsalesOrderItems { get; set; } = null!;
        public virtual DbSet<SapsalesOrganization> SapsalesOrganizations { get; set; } = null!;
        public virtual DbSet<SapscrapDelivery> SapscrapDeliveries { get; set; } = null!;
        public virtual DbSet<SapscrapOrder> SapscrapOrders { get; set; } = null!;
        public virtual DbSet<SapshipTo> SapshipTos { get; set; } = null!;
        public virtual DbSet<SapshipToSapsalesOrganization> SapshipToSapsalesOrganizations { get; set; } = null!;
        public virtual DbSet<Sapshipment> Sapshipments { get; set; } = null!;
        public virtual DbSet<SapsoldTo> SapsoldTos { get; set; } = null!;
        public virtual DbSet<SapsoldToPlantExclusion> SapsoldToPlantExclusions { get; set; } = null!;
        public virtual DbSet<SapsoldToSapshipTo> SapsoldToSapshipTos { get; set; } = null!;
        public virtual DbSet<Sapstock> Sapstocks { get; set; } = null!;
        public virtual DbSet<SapstorageLocation> SapstorageLocations { get; set; } = null!;
        public virtual DbSet<Saptier> Saptiers { get; set; } = null!;
        public virtual DbSet<SaptierSapsalesOrganization> SaptierSapsalesOrganizations { get; set; } = null!;
        public virtual DbSet<SapunitOfMeasure> SapunitOfMeasures { get; set; } = null!;
        public virtual DbSet<Sapvendor> Sapvendors { get; set; } = null!;
        public virtual DbSet<SapwheatlandDelivery> SapwheatlandDeliveries { get; set; } = null!;
        public virtual DbSet<ScrapSale> ScrapSales { get; set; } = null!;
        public virtual DbSet<ScrapSaleBox> ScrapSaleBoxes { get; set; } = null!;
        public virtual DbSet<ScrapSaleSapsalesOrderItem> ScrapSaleSapsalesOrderItems { get; set; } = null!;
        public virtual DbSet<ScrapSapsoldTo> ScrapSapsoldTos { get; set; } = null!;
        public virtual DbSet<ShipmentCost> ShipmentCosts { get; set; } = null!;
        public virtual DbSet<ShipmentCostType> ShipmentCostTypes { get; set; } = null!;
        public virtual DbSet<ShippingCart> ShippingCarts { get; set; } = null!;
        public virtual DbSet<ShippingCartSapsalesOrderItem> ShippingCartSapsalesOrderItems { get; set; } = null!;
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; } = null!;
        public virtual DbSet<ShoppingCartSaprolling> ShoppingCartSaprollings { get; set; } = null!;
        public virtual DbSet<ShoppingCartSapstock> ShoppingCartSapstocks { get; set; } = null!;
        public virtual DbSet<ShoppingCartWheatland> ShoppingCartWheatlands { get; set; } = null!;
        public virtual DbSet<SoldToPriceSheetDefaultOption> SoldToPriceSheetDefaultOptions { get; set; } = null!;
        public virtual DbSet<Square> Squares { get; set; } = null!;
        public virtual DbSet<State> States { get; set; } = null!;
        public virtual DbSet<Stock> Stocks { get; set; } = null!;
        public virtual DbSet<StockingList> StockingLists { get; set; } = null!;
        public virtual DbSet<Trailer> Trailers { get; set; } = null!;
        public virtual DbSet<Truck> Trucks { get; set; } = null!;
        public virtual DbSet<Tzcountry> Tzcountries { get; set; } = null!;
        public virtual DbSet<Tztimezone> Tztimezones { get; set; } = null!;
        public virtual DbSet<Tzzone> Tzzones { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserApplicationRole> UserApplicationRoles { get; set; } = null!;
        public virtual DbSet<UserProfile> UserProfiles { get; set; } = null!;
        public virtual DbSet<UserSapsoldTo> UserSapsoldTos { get; set; } = null!;
        public virtual DbSet<WebRelasePlantSapsalesOrderItemSapdeliveryItem> WebRelasePlantSapsalesOrderItemSapdeliveryItems { get; set; } = null!;
        public virtual DbSet<WebRelease> WebReleases { get; set; } = null!;
        public virtual DbSet<WebReleasePlant> WebReleasePlants { get; set; } = null!;
        public virtual DbSet<WebReleasePlantSapsalesOrderItem> WebReleasePlantSapsalesOrderItems { get; set; } = null!;
        public virtual DbSet<WtcpipeBnb> WtcpipeBnbs { get; set; } = null!;
        public virtual DbSet<WtcpipeSize> WtcpipeSizes { get; set; } = null!;
        public virtual DbSet<Zep1> Zep1s { get; set; } = null!;
        public virtual DbSet<Zg01> Zg01s { get; set; } = null!;
        public virtual DbSet<Zr00> Zr00s { get; set; } = null!;
        public virtual DbSet<Zr01> Zr01s { get; set; } = null!;
        public virtual DbSet<Zr04> Zr04s { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Portal;Trusted_Connection=True;Timeout=1000;command timeout=1000");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AlternateSoldTo>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("AlternateSoldTo");

                entity.HasIndex(e => new { e.SapshipToId, e.AlternateSapsoldToId }, "IX_AlternateSoldTo")
                    .IsUnique();

                entity.Property(e => e.AlternateSapsoldToId).HasColumnName("AlternateSAPSoldToID");

                entity.Property(e => e.SapshipToId).HasColumnName("SAPShipToID");

                entity.HasOne(d => d.AlternateSapsoldTo)
                    .WithMany()
                    .HasForeignKey(d => d.AlternateSapsoldToId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AlternateSoldTo_SAPSoldTo1");

                entity.HasOne(d => d.SapshipTo)
                    .WithMany()
                    .HasForeignKey(d => d.SapshipToId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AlternateSoldTo_SAPSoldTo");
            });

            modelBuilder.Entity<Application>(entity =>
            {
                entity.ToTable("Application");

                entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LongName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Url)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("URL");
            });

            modelBuilder.Entity<ApplicationRole>(entity =>
            {
                entity.ToTable("ApplicationRole");

                entity.Property(e => e.ApplicationRoleId).HasColumnName("ApplicationRoleID");

                entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.ApplicationRoles)
                    .HasForeignKey(d => d.ApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApplicationRole_Application");
            });

            modelBuilder.Entity<AtpemployeePlant>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ATPEmployeePlant");

                entity.HasIndex(e => new { e.UserId, e.LocationId }, "IX_ATPEmployeePlant")
                    .IsUnique();

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Location)
                    .WithMany()
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ATPEmployeePlant_Plant");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ATPEmployeePlant_Employee");
            });

            modelBuilder.Entity<Box>(entity =>
            {
                entity.ToTable("Box");

                entity.Property(e => e.BoxId).HasColumnName("BoxID");

                entity.Property(e => e.ChagedDate).HasColumnType("datetime");

                entity.Property(e => e.ChangedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PreviousWeight)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("previousWeight");

                entity.Property(e => e.RandomLengthSapsoldToId).HasColumnName("RandomLengthSAPSoldToID");

                entity.Property(e => e.SapvendorId).HasColumnName("SAPVendorID");

                entity.Property(e => e.ScrapSapsoldToId).HasColumnName("ScrapSAPSoldToID");

                entity.Property(e => e.Weight).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.RandomLengthSapsoldTo)
                    .WithMany(p => p.Boxes)
                    .HasForeignKey(d => d.RandomLengthSapsoldToId)
                    .HasConstraintName("FK_Box_RandomLengthSAPSoldTo");

                entity.HasOne(d => d.Sapvendor)
                    .WithMany(p => p.Boxes)
                    .HasForeignKey(d => d.SapvendorId)
                    .HasConstraintName("FK_Box_SAPVendor");

                entity.HasOne(d => d.ScrapSapsoldTo)
                    .WithMany(p => p.Boxes)
                    .HasForeignKey(d => d.ScrapSapsoldToId)
                    .HasConstraintName("FK_Box_ScrapSAPSoldTo");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("City");

                entity.HasIndex(e => new { e.Name, e.StateId }, "IX_City")
                    .IsUnique();

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StateId).HasColumnName("StateID");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_City_State");
            });

            modelBuilder.Entity<ClaimRequest>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ClaimRequest");

                entity.Property(e => e.BatchNos).HasMaxLength(50);

                entity.Property(e => e.BillOfLading).HasMaxLength(50);

                entity.Property(e => e.Contact)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ContactNo).HasMaxLength(10);

                entity.Property(e => e.Isr)
                    .HasMaxLength(50)
                    .HasColumnName("ISR");

                entity.Property(e => e.MaterialGauge).HasMaxLength(10);

                entity.Property(e => e.MaterialLength).HasMaxLength(10);

                entity.Property(e => e.MaterialSize).HasMaxLength(10);

                entity.Property(e => e.MigrationsOptions).HasMaxLength(50);

                entity.Property(e => e.Reason).HasMaxLength(50);

                entity.Property(e => e.RequestDate).HasColumnType("date");

                entity.Property(e => e.ShipTo).HasMaxLength(50);

                entity.Property(e => e.SoldTo).HasMaxLength(50);
            });

            modelBuilder.Entity<Cmir>(entity =>
            {
                entity.ToTable("CMIR");

                entity.Property(e => e.Cmirid).HasColumnName("CMIRID");

                entity.Property(e => e.CustomerPartNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DistributionChannel)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.SapmaterialId).HasColumnName("SAPMaterialID");

                entity.Property(e => e.SapsalesOrganizationId).HasColumnName("SAPSalesOrganizationID");

                entity.Property(e => e.SapshipToId).HasColumnName("SAPShipToID");

                entity.HasOne(d => d.Sapmaterial)
                    .WithMany(p => p.Cmirs)
                    .HasForeignKey(d => d.SapmaterialId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CMIR_SAPMaterial");

                entity.HasOne(d => d.SapsalesOrganization)
                    .WithMany(p => p.Cmirs)
                    .HasForeignKey(d => d.SapsalesOrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CMIR_SAPSalesOrganization");

                entity.HasOne(d => d.SapshipTo)
                    .WithMany(p => p.Cmirs)
                    .HasForeignKey(d => d.SapshipToId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CMIR_SAPSoldTo");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("Country");

                entity.HasIndex(e => new { e.Name, e.Abbreviation }, "IX_Country")
                    .IsUnique();

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.Abbreviation)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DataView>(entity =>
            {
                entity.ToTable("DataView");

                entity.Property(e => e.DataViewId).HasColumnName("DataViewID");

                entity.Property(e => e.Name)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DataViewColumn>(entity =>
            {
                entity.ToTable("DataViewColumn");

                entity.Property(e => e.DataViewColumnId).HasColumnName("DataViewColumnID");

                entity.Property(e => e.DataViewId).HasColumnName("DataViewID");

                entity.Property(e => e.DisplayFieldName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FieldName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.FooterTemplate).IsUnicode(false);

                entity.Property(e => e.Format)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.GroupByDirection)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.GroupFooterTemplate).IsUnicode(false);

                entity.Property(e => e.GroupHeaderTemplate).IsUnicode(false);

                entity.Property(e => e.HeaderTemplate).IsUnicode(false);

                entity.Property(e => e.HtmlAttributeClass).IsUnicode(false);

                entity.Property(e => e.HtmlAttributeStyle).IsUnicode(false);

                entity.Property(e => e.SortDirection)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.DataView)
                    .WithMany(p => p.DataViewColumns)
                    .HasForeignKey(d => d.DataViewId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DataViewColumn_DataView");
            });

            modelBuilder.Entity<DealsBySoldToShipTo>(entity =>
            {
                entity.ToTable("DealsBySoldToShipTo");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DealId).HasColumnName("DealID");

                entity.Property(e => e.SapshipToId).HasColumnName("SAPShipToID");

                entity.Property(e => e.SapsoldToId).HasColumnName("SAPSoldToID");

                entity.HasOne(d => d.Deal)
                    .WithMany(p => p.DealsBySoldToShipTos)
                    .HasForeignKey(d => d.DealId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DealsBySoldToShipTo_DealID");

                entity.HasOne(d => d.SapshipTo)
                    .WithMany(p => p.DealsBySoldToShipTos)
                    .HasForeignKey(d => d.SapshipToId)
                    .HasConstraintName("FK_DealsBySoldToShipTo_SAPShipToID");

                entity.HasOne(d => d.SapsoldTo)
                    .WithMany(p => p.DealsBySoldToShipTos)
                    .HasForeignKey(d => d.SapsoldToId)
                    .HasConstraintName("FK_DealsBySoldToShipTo_SAPSoldToID");
            });

            modelBuilder.Entity<DealsDetail>(entity =>
            {
                entity.HasKey(e => e.DealId)
                    .HasName("PK_DealsDetails_DealID");

                entity.Property(e => e.DealId).HasColumnName("DealID");

                entity.Property(e => e.ApprovedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ApprovedDeniedDate).HasColumnType("datetime");

                entity.Property(e => e.ApprovedUserId).HasColumnName("ApprovedUserID");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedUserId).HasColumnName("CreatedUserID");

                entity.Property(e => e.DealType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Notes)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.OrderFromDate).HasColumnType("datetime");

                entity.Property(e => e.OrderToDate).HasColumnType("datetime");

                entity.Property(e => e.ShipFromDate).HasColumnType("datetime");

                entity.Property(e => e.ShipToDate).HasColumnType("datetime");

                entity.Property(e => e.TonsUsed).HasColumnType("decimal(10, 2)");
            });

            modelBuilder.Entity<DealsMaterialPricingGroup>(entity =>
            {
                entity.HasKey(e => e.DealsMaterialPricingGroupsId)
                    .HasName("PK_DealsMaterialPricingGroups_DealsMaterialPricingGroupsID");

                entity.Property(e => e.DealsMaterialPricingGroupsId).HasColumnName("DealsMaterialPricingGroupsID");

                entity.Property(e => e.DealId).HasColumnName("DealID");

                entity.Property(e => e.Discount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Zr00).HasColumnName("ZR00");

                entity.HasOne(d => d.Deal)
                    .WithMany(p => p.DealsMaterialPricingGroups)
                    .HasForeignKey(d => d.DealId)
                    .HasConstraintName("FK_DealsMaterialPricingGroups_DealID");

                entity.HasOne(d => d.Zr00Navigation)
                    .WithMany(p => p.DealsMaterialPricingGroups)
                    .HasForeignKey(d => d.Zr00)
                    .HasConstraintName("FK_ZR00_SAPCharacteristicOptionID");
            });

            modelBuilder.Entity<DealsPlant>(entity =>
            {
                entity.ToTable("DealsPlant");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DealId).HasColumnName("DealID");

                entity.Property(e => e.PlantId).HasColumnName("PlantID");

                entity.HasOne(d => d.Deal)
                    .WithMany(p => p.DealsPlants)
                    .HasForeignKey(d => d.DealId)
                    .HasConstraintName("FK_DealsPlant_DealID");
            });

            modelBuilder.Entity<DealsPricingGroup>(entity =>
            {
                entity.HasKey(e => e.DealsPricingGroupsId)
                    .HasName("PK_DealsPricingGroups_DealsPricingGroupsID");

                entity.Property(e => e.DealsPricingGroupsId).HasColumnName("DealsPricingGroupsID");

                entity.Property(e => e.DealId).HasColumnName("DealID");

                entity.Property(e => e.Discount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Zr01).HasColumnName("ZR01");

                entity.HasOne(d => d.Deal)
                    .WithMany(p => p.DealsPricingGroups)
                    .HasForeignKey(d => d.DealId)
                    .HasConstraintName("FK_DealsPricingGroups_DealID");

                entity.HasOne(d => d.Zr01Navigation)
                    .WithMany(p => p.DealsPricingGroups)
                    .HasForeignKey(d => d.Zr01)
                    .HasConstraintName("FK_ZR01_SAPCharacteristicOptionID");
            });

            modelBuilder.Entity<DefaultTubeStandard>(entity =>
            {
                entity.HasKey(e => e.DefaultId);

                entity.Property(e => e.DefaultId).HasColumnName("DefaultID");

                entity.Property(e => e.Diameter).HasColumnType("decimal(17, 3)");

                entity.Property(e => e.GradeValue)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PlantId).HasColumnName("PlantID");

                entity.Property(e => e.Size).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Size2).HasColumnType("decimal(18, 4)");
            });

            modelBuilder.Entity<DeliveryMethod>(entity =>
            {
                entity.ToTable("DeliveryMethod");

                entity.Property(e => e.DeliveryMethodId).HasColumnName("DeliveryMethodID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Adname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ADName");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Division>(entity =>
            {
                entity.ToTable("Division");

                entity.Property(e => e.DivisionId).HasColumnName("DivisionID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Document>(entity =>
            {
                entity.ToTable("Document");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.FileName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FileType)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("Employee");

                entity.Property(e => e.UserId)
                    .ValueGeneratedNever()
                    .HasColumnName("UserID");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.DivisionId).HasColumnName("DivisionID");

                entity.Property(e => e.Domain)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeePositionId).HasColumnName("EmployeePositionID");

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.ManagerId).HasColumnName("ManagerID");

                entity.Property(e => e.SamaccountName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SAMAccountName");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK_Employee_Department");

                entity.HasOne(d => d.Division)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DivisionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_Division");

                entity.HasOne(d => d.EmployeePosition)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.EmployeePositionId)
                    .HasConstraintName("FK_Employee_EmployeePosition");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK_Employee_Location");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Employee)
                    .HasForeignKey<Employee>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_User");
            });

            modelBuilder.Entity<EmployeePosition>(entity =>
            {
                entity.ToTable("EmployeePosition");

                entity.Property(e => e.EmployeePositionId).HasColumnName("EmployeePositionID");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FiscalPeriod>(entity =>
            {
                entity.ToTable("FiscalPeriod");

                entity.Property(e => e.FiscalPeriodId).HasColumnName("FiscalPeriodID");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.FiscalYearId).HasColumnName("FiscalYearID");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.FiscalYear)
                    .WithMany(p => p.FiscalPeriods)
                    .HasForeignKey(d => d.FiscalYearId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FiscalPeriod_FiscalYear");
            });

            modelBuilder.Entity<FiscalYear>(entity =>
            {
                entity.ToTable("FiscalYear");

                entity.Property(e => e.FiscalYearId).HasColumnName("FiscalYearID");
            });

            modelBuilder.Entity<FreightandFsc>(entity =>
            {
                entity.HasKey(e => e.FreightId);

                entity.ToTable("FreightandFSC");

                entity.HasIndex(e => new { e.SapshiptoId, e.SapconditionId, e.SapsoldtoId }, "IDX_FSC_SHIPTOSOLDTO")
                    .HasFillFactor(100);

                entity.Property(e => e.FreightId).HasColumnName("freightID");

                entity.Property(e => e.Currency)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Rate).HasColumnType("decimal(13, 2)");

                entity.Property(e => e.SapconditionId).HasColumnName("SAPConditionID");

                entity.Property(e => e.SapshiptoId).HasColumnName("SAPShiptoID");

                entity.Property(e => e.SapsoldtoId).HasColumnName("SAPSoldtoID");

                entity.Property(e => e.Unit)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ValidFrom).HasColumnType("date");

                entity.Property(e => e.ValidTo).HasColumnType("date");

                entity.HasOne(d => d.Sapcondition)
                    .WithMany(p => p.FreightandFscs)
                    .HasForeignKey(d => d.SapconditionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FreightandFSC_SAPCondition");

                entity.HasOne(d => d.Sapshipto)
                    .WithMany(p => p.FreightandFscs)
                    .HasForeignKey(d => d.SapshiptoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FreightandFSC_FreightandFSC");
            });

            modelBuilder.Entity<Gauge>(entity =>
            {
                entity.ToTable("Gauge");

                entity.Property(e => e.GaugeId).HasColumnName("GaugeID");

                entity.Property(e => e.Gauge1)
                    .HasColumnType("decimal(9, 3)")
                    .HasColumnName("Gauge");

                entity.Property(e => e.MaterialGroup)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nps).HasColumnName("NPS");

                entity.Property(e => e.Npsdescription)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NPSDescription");
            });

            modelBuilder.Entity<Ipsfile>(entity =>
            {
                entity.ToTable("IPSFile");

                entity.Property(e => e.IpsfileId).HasColumnName("IPSFileID");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Ipsfiles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IPSFile_User");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("Location");

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Address)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Adname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ADName");

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.DivisionId).HasColumnName("DivisionID");

                entity.Property(e => e.FaxNumber)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TollFreePhoneNumber)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Location_City");

                entity.HasOne(d => d.Division)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.DivisionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Location_Division");
            });

            modelBuilder.Entity<LocationDepartment>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("LocationDepartment");

                entity.HasIndex(e => e.LocationId, "IX_LocationDepartment")
                    .IsClustered();

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.HasOne(d => d.Department)
                    .WithMany()
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LocationDepartment_Department");

                entity.HasOne(d => d.Location)
                    .WithMany()
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LocationDepartment_Location");
            });

            modelBuilder.Entity<LoginHistory>(entity =>
            {
                entity.ToTable("LoginHistory");

                entity.HasIndex(e => e.LoginDate, "ix_LoginDate");

                entity.HasIndex(e => new { e.UserId, e.LoginDate }, "ix_UserID_LoginDate");

                entity.Property(e => e.LoginHistoryId).HasColumnName("LoginHistoryID");

                entity.Property(e => e.BrowserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BrowserVersion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HostName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IpAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LoginDate).HasColumnType("datetime");

                entity.Property(e => e.Platform)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ScreenResolution)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserAgent)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.LoginHistories)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LoginHistory_User");
            });

            modelBuilder.Entity<Mill>(entity =>
            {
                entity.ToTable("Mill");

                entity.Property(e => e.MillId).HasColumnName("MillID");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.MaxGauge).HasColumnType("decimal(9, 3)");

                entity.Property(e => e.MaxLength).HasColumnType("decimal(9, 3)");

                entity.Property(e => e.MaxSize).HasColumnType("decimal(9, 3)");

                entity.Property(e => e.MinGauge).HasColumnType("decimal(9, 3)");

                entity.Property(e => e.MinLength).HasColumnType("decimal(9, 3)");

                entity.Property(e => e.MinSize).HasColumnType("decimal(9, 3)");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PlantId).HasColumnName("PlantID");

                entity.Property(e => e.WorkCenter).HasMaxLength(50);

                entity.HasOne(d => d.Plant)
                    .WithMany(p => p.Mills)
                    .HasForeignKey(d => d.PlantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Mill_Plant");
            });

            modelBuilder.Entity<MillException>(entity =>
            {
                entity.ToTable("MillException");

                entity.Property(e => e.MillExceptionId).HasColumnName("MillExceptionID");

                entity.Property(e => e.MaxGauge).HasColumnType("decimal(9, 3)");

                entity.Property(e => e.MaxLength).HasColumnType("decimal(9, 3)");

                entity.Property(e => e.MaxSize).HasColumnType("decimal(9, 3)");

                entity.Property(e => e.MillId).HasColumnName("MillID");

                entity.Property(e => e.MinGauge).HasColumnType("decimal(9, 3)");

                entity.Property(e => e.MinLength).HasColumnType("decimal(9, 3)");

                entity.Property(e => e.MinSize).HasColumnType("decimal(9, 3)");
            });

            modelBuilder.Entity<PendingShipmentCost>(entity =>
            {
                entity.ToTable("PendingShipmentCost");

                entity.Property(e => e.PendingShipmentCostId).HasColumnName("PendingShipmentCostID");

                entity.Property(e => e.Amount).HasColumnType("decimal(13, 2)");

                entity.Property(e => e.BalanceDueSupplemental)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Bol)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("BOL");

                entity.Property(e => e.DivisionId).HasColumnName("DivisionID");

                entity.Property(e => e.ShipmentCostTypeId).HasColumnName("ShipmentCostTypeID");

                entity.HasOne(d => d.Division)
                    .WithMany(p => p.PendingShipmentCosts)
                    .HasForeignKey(d => d.DivisionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PendingShipmentCost_Division");

                entity.HasOne(d => d.ShipmentCostType)
                    .WithMany(p => p.PendingShipmentCosts)
                    .HasForeignKey(d => d.ShipmentCostTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PendingShipmentCost_ShipmentCostType");
            });

            modelBuilder.Entity<PersonDataViewColumn>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("PersonDataViewColumn");

                entity.Property(e => e.DataViewColumnId).HasColumnName("DataViewColumnID");

                entity.Property(e => e.DataViewId).HasColumnName("DataViewID");

                entity.Property(e => e.DisplayFieldName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FieldName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.FirstFilterOperator)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FirstFilterValue).IsUnicode(false);

                entity.Property(e => e.FooterTemplate).IsUnicode(false);

                entity.Property(e => e.Format)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.GroupByDirection)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.GroupFooterTemplate).IsUnicode(false);

                entity.Property(e => e.GroupHeaderTemplate).IsUnicode(false);

                entity.Property(e => e.HeaderTemplate).IsUnicode(false);

                entity.Property(e => e.HtmlAttributeClass).IsUnicode(false);

                entity.Property(e => e.HtmlAttributeStyle).IsUnicode(false);

                entity.Property(e => e.Logic)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PersonDataViewColumnTemplateId).HasColumnName("PersonDataViewColumnTemplateID");

                entity.Property(e => e.PersonDataViewTemplateId).HasColumnName("PersonDataViewTemplateID");

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.Property(e => e.SecondFilterOperator)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SecondFilterValue).IsUnicode(false);

                entity.Property(e => e.SortDirection)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PersonDataViewColumnTemplate>(entity =>
            {
                entity.ToTable("PersonDataViewColumnTemplate");

                entity.Property(e => e.PersonDataViewColumnTemplateId).HasColumnName("PersonDataViewColumnTemplateID");

                entity.Property(e => e.DataViewColumnId).HasColumnName("DataViewColumnID");

                entity.Property(e => e.FirstFilter)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstFilterOperator)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FirstFilterValue).IsUnicode(false);

                entity.Property(e => e.GroupByDirection)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Logic)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PersonDataViewTemplateId).HasColumnName("PersonDataViewTemplateID");

                entity.Property(e => e.SecondFilter)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SecondFilterOperator)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SecondFilterValue).IsUnicode(false);

                entity.Property(e => e.SortDirection)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.HasOne(d => d.DataViewColumn)
                    .WithMany(p => p.PersonDataViewColumnTemplates)
                    .HasForeignKey(d => d.DataViewColumnId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PersonDataViewColumnTemplate_DataViewColumn");

                entity.HasOne(d => d.PersonDataViewTemplate)
                    .WithMany(p => p.PersonDataViewColumnTemplates)
                    .HasForeignKey(d => d.PersonDataViewTemplateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PersonDataViewColumnTemplate_PersonDataViewTemplate");
            });

            modelBuilder.Entity<PersonDataViewTemplate>(entity =>
            {
                entity.ToTable("PersonDataViewTemplate");

                entity.Property(e => e.PersonDataViewTemplateId).HasColumnName("PersonDataViewTemplateID");

                entity.Property(e => e.DataViewId).HasColumnName("DataViewID");

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.HasOne(d => d.DataView)
                    .WithMany(p => p.PersonDataViewTemplates)
                    .HasForeignKey(d => d.DataViewId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PersonDataViewTemplate_DataView");
            });

            modelBuilder.Entity<PhoneNumber>(entity =>
            {
                entity.ToTable("PhoneNumber");

                entity.Property(e => e.PhoneNumberId).HasColumnName("PhoneNumberID");

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Number)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.PhoneNumbers)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK_PhoneNumber_Location");
            });

            modelBuilder.Entity<PipeSize>(entity =>
            {
                entity.HasKey(e => e.SizeId);

                entity.Property(e => e.SizeId).HasColumnName("sizeID");

                entity.Property(e => e.MaterialGroup)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Shape)
                    .HasMaxLength(3)
                    .HasColumnName("shape");

                entity.Property(e => e.Size1).HasColumnType("decimal(9, 4)");

                entity.Property(e => e.Size2).HasColumnType("decimal(9, 4)");
            });

            modelBuilder.Entity<Plant>(entity =>
            {
                entity.HasKey(e => e.LocationId);

                entity.ToTable("Plant");

                entity.Property(e => e.LocationId)
                    .ValueGeneratedNever()
                    .HasColumnName("LocationID");

                entity.Property(e => e.Code)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.HomeMillSapconditionGroupId).HasColumnName("HomeMillSAPConditionGroupID");

                entity.Property(e => e.SalesOrganization)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.HomeMillSapconditionGroup)
                    .WithMany(p => p.Plants)
                    .HasForeignKey(d => d.HomeMillSapconditionGroupId)
                    .HasConstraintName("FK_Plant_HomeMillSAPConditionGroup");

                entity.HasOne(d => d.Location)
                    .WithOne(p => p.Plant)
                    .HasForeignKey<Plant>(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Plant_Location");
            });

            modelBuilder.Entity<PlantComputer>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("PlantComputer");

                entity.Property(e => e.UserId)
                    .ValueGeneratedNever()
                    .HasColumnName("UserID");

                entity.Property(e => e.ComputerName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.PlantComputers)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK_PlantComputer_Department");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.PlantComputers)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlantComputer_Location");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.PlantComputer)
                    .HasForeignKey<PlantComputer>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlantComputer_User");
            });

            modelBuilder.Entity<PriceChangeRequest>(entity =>
            {
                entity.ToTable("PriceChangeRequest");

                entity.Property(e => e.PriceChangeRequestId).HasColumnName("PriceChangeRequestID");

                entity.Property(e => e.Currency)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DivisionId).HasColumnName("DivisionID");

                entity.Property(e => e.RegionSapconditionGroupId).HasColumnName("RegionSAPConditionGroupID");

                entity.Property(e => e.SapsoldToId).HasColumnName("SAPSoldToID");

                entity.Property(e => e.TierSapconditionGroupId).HasColumnName("TierSAPConditionGroupID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Division)
                    .WithMany(p => p.PriceChangeRequests)
                    .HasForeignKey(d => d.DivisionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PriceChangeRequest_Division");

                entity.HasOne(d => d.RegionSapconditionGroup)
                    .WithMany(p => p.PriceChangeRequestRegionSapconditionGroups)
                    .HasForeignKey(d => d.RegionSapconditionGroupId)
                    .HasConstraintName("FK_PriceChangeRequest_RegionSAPConditionGroup");

                entity.HasOne(d => d.SapsoldTo)
                    .WithMany(p => p.PriceChangeRequests)
                    .HasForeignKey(d => d.SapsoldToId)
                    .HasConstraintName("FK_PriceChangeRequest_SAPSoldTo");

                entity.HasOne(d => d.TierSapconditionGroup)
                    .WithMany(p => p.PriceChangeRequestTierSapconditionGroups)
                    .HasForeignKey(d => d.TierSapconditionGroupId)
                    .HasConstraintName("FK_PriceChangeRequest_TierSAPConditionGroup");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PriceChangeRequests)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PriceChangeRequest_User");
            });

            modelBuilder.Entity<PriceChangeRequestItem>(entity =>
            {
                entity.ToTable("PriceChangeRequestItem");

                entity.Property(e => e.PriceChangeRequestItemId).HasColumnName("PriceChangeRequestItemID");

                entity.Property(e => e.ApprovedDate).HasColumnType("datetime");

                entity.Property(e => e.ApprovedUserId).HasColumnName("ApprovedUserID");

                entity.Property(e => e.EffectiveDate).HasColumnType("datetime");

                entity.Property(e => e.FreightIndicator)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.IncoTerms2)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OldEffectiveDate).HasColumnType("datetime");

                entity.Property(e => e.OldRate).HasColumnType("decimal(13, 2)");

                entity.Property(e => e.PriceChangeRequestId).HasColumnName("PriceChangeRequestID");

                entity.Property(e => e.Rate).HasColumnType("decimal(13, 2)");

                entity.Property(e => e.SapconditionId).HasColumnName("SAPConditionID");

                entity.Property(e => e.SapmaterialGroupId).HasColumnName("SAPMaterialGroupID");

                entity.Property(e => e.SapmaterialPricingGroupId).HasColumnName("SAPMaterialPricingGroupID");

                entity.Property(e => e.SappricingGroupId).HasColumnName("SAPPricingGroupID");

                entity.Property(e => e.SapshipToId).HasColumnName("SAPShipToID");

                entity.HasOne(d => d.ApprovedUser)
                    .WithMany(p => p.PriceChangeRequestItems)
                    .HasForeignKey(d => d.ApprovedUserId)
                    .HasConstraintName("FK_PriceChangeRequestItem_User");

                entity.HasOne(d => d.PriceChangeRequest)
                    .WithMany(p => p.PriceChangeRequestItems)
                    .HasForeignKey(d => d.PriceChangeRequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PriceChangeRequestItem_PriceChangeRequest");

                entity.HasOne(d => d.Sapcondition)
                    .WithMany(p => p.PriceChangeRequestItems)
                    .HasForeignKey(d => d.SapconditionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PriceChangeRequestItem_SAPCondition");

                entity.HasOne(d => d.SapmaterialGroup)
                    .WithMany(p => p.PriceChangeRequestItemSapmaterialGroups)
                    .HasForeignKey(d => d.SapmaterialGroupId)
                    .HasConstraintName("FK_PriceChangeRequestItem_SAPMaterialGroup");

                entity.HasOne(d => d.SapmaterialPricingGroup)
                    .WithMany(p => p.PriceChangeRequestItemSapmaterialPricingGroups)
                    .HasForeignKey(d => d.SapmaterialPricingGroupId)
                    .HasConstraintName("FK_PriceChangeRequestItem_SAPMaterialPricingGroup");

                entity.HasOne(d => d.SappricingGroup)
                    .WithMany(p => p.PriceChangeRequestItemSappricingGroups)
                    .HasForeignKey(d => d.SappricingGroupId)
                    .HasConstraintName("FK_PriceChangeRequestItem_SAPPricingGroup");

                entity.HasOne(d => d.SapshipTo)
                    .WithMany(p => p.PriceChangeRequestItems)
                    .HasForeignKey(d => d.SapshipToId)
                    .HasConstraintName("FK_PriceChangeRequestItem_SAPShipTo");
            });

            modelBuilder.Entity<PriceChangeSetting>(entity =>
            {
                entity.ToTable("PriceChangeSetting");

                entity.Property(e => e.PriceChangeSettingId).HasColumnName("PriceChangeSettingID");

                entity.Property(e => e.DecimalValue).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StringValue)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PriceSheetNote>(entity =>
            {
                entity.ToTable("PriceSheetNote");

                entity.HasIndex(e => new { e.SapsoldToId, e.RegionSapconditionGroupId }, "IX_PriceSheetNote")
                    .IsUnique();

                entity.Property(e => e.PriceSheetNoteId).HasColumnName("PriceSheetNoteID");

                entity.Property(e => e.Notes)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.RegionSapconditionGroupId).HasColumnName("RegionSAPConditionGroupID");

                entity.Property(e => e.SapsoldToId).HasColumnName("SAPSoldToID");

                entity.HasOne(d => d.RegionSapconditionGroup)
                    .WithMany(p => p.PriceSheetNotes)
                    .HasForeignKey(d => d.RegionSapconditionGroupId)
                    .HasConstraintName("FK_PriceSheetNote_RegionSAPConditionGroup");

                entity.HasOne(d => d.SapsoldTo)
                    .WithMany(p => p.PriceSheetNotes)
                    .HasForeignKey(d => d.SapsoldToId)
                    .HasConstraintName("FK_PriceSheetNote_SAPSoldTo");
            });

            modelBuilder.Entity<ProductLine>(entity =>
            {
                entity.ToTable("ProductLine");

                entity.Property(e => e.ProductLineId).HasColumnName("ProductLineID");

                entity.Property(e => e.DivisionId).HasColumnName("DivisionID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Division)
                    .WithMany(p => p.ProductLines)
                    .HasForeignKey(d => d.DivisionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductLine_Division");
            });

            modelBuilder.Entity<QuickSearchCriterion>(entity =>
            {
                entity.HasKey(e => e.CriteriaId)
                    .HasName("PK_UserQuickSearch");

                entity.Property(e => e.Diameter).HasColumnType("decimal(17, 3)");

                entity.Property(e => e.GaugeRestrictable)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Grade)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Inches).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Length).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.RequestedDate).HasColumnType("datetime");

                entity.Property(e => e.Shape)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.SizeX).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SizeY).HasColumnType("decimal(18, 4)");

                entity.HasOne(d => d.Template)
                    .WithMany(p => p.QuickSearchCriteria)
                    .HasForeignKey(d => d.TemplateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserQuickSearch_UserQuickSearchTemplate");
            });

            modelBuilder.Entity<QuickSearchTemplate>(entity =>
            {
                entity.HasKey(e => e.TemplateId)
                    .HasName("PK_UserQuickSearchTemplate_1");

                entity.ToTable("QuickSearchTemplate");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.TemplateName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Quote>(entity =>
            {
                entity.ToTable("Quote");

                entity.Property(e => e.QuoteId).HasColumnName("QuoteID");

                entity.Property(e => e.Attention)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Comments)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.Currency)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DivisionId).HasColumnName("DivisionID");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.FreightIndicatorSapconditionGroupId).HasColumnName("FreightIndicatorSAPConditionGroupID");

                entity.Property(e => e.IncoTerms2)
                    .HasMaxLength(28)
                    .IsUnicode(false);

                entity.Property(e => e.LastUpdatedFromSap)
                    .HasColumnType("datetime")
                    .HasColumnName("LastUpdatedFromSAP");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NewCustomerName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ponumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PONumber");

                entity.Property(e => e.ProductionPlantId).HasColumnName("ProductionPlantID");

                entity.Property(e => e.PromiseDate).HasColumnType("datetime");

                entity.Property(e => e.RegionSapconditionGroupId).HasColumnName("RegionSAPConditionGroupID");

                entity.Property(e => e.SapshipToId).HasColumnName("SAPShipToID");

                entity.Property(e => e.SapsoldToId).HasColumnName("SAPSoldToID");

                entity.Property(e => e.TierSapconditionGroupId).HasColumnName("TierSAPConditionGroupID");

                entity.HasOne(d => d.Division)
                    .WithMany(p => p.Quotes)
                    .HasForeignKey(d => d.DivisionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Quote_Division");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Quotes)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Quote_Employee");

                entity.HasOne(d => d.FreightIndicatorSapconditionGroup)
                    .WithMany(p => p.QuoteFreightIndicatorSapconditionGroups)
                    .HasForeignKey(d => d.FreightIndicatorSapconditionGroupId)
                    .HasConstraintName("FK_Quote_FreightIndicatorSAPConditionGroup");

                entity.HasOne(d => d.ProductionPlant)
                    .WithMany(p => p.Quotes)
                    .HasForeignKey(d => d.ProductionPlantId)
                    .HasConstraintName("FK_Quote_Plant");

                entity.HasOne(d => d.RegionSapconditionGroup)
                    .WithMany(p => p.QuoteRegionSapconditionGroups)
                    .HasForeignKey(d => d.RegionSapconditionGroupId)
                    .HasConstraintName("FK_Quote_RegionSAPConditionGroup");

                entity.HasOne(d => d.SapshipTo)
                    .WithMany(p => p.Quotes)
                    .HasForeignKey(d => d.SapshipToId)
                    .HasConstraintName("FK_Quote_SAPShipTo");

                entity.HasOne(d => d.SapsoldTo)
                    .WithMany(p => p.Quotes)
                    .HasForeignKey(d => d.SapsoldToId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Quote_SAPSoldTo");

                entity.HasOne(d => d.TierSapconditionGroup)
                    .WithMany(p => p.QuoteTierSapconditionGroups)
                    .HasForeignKey(d => d.TierSapconditionGroupId)
                    .HasConstraintName("FK_Quote_TierSAPConditionGroup");
            });

            modelBuilder.Entity<QuoteMaterial>(entity =>
            {
                entity.ToTable("QuoteMaterial");

                entity.Property(e => e.QuoteMaterialId).HasColumnName("QuoteMaterialID");

                entity.Property(e => e.Comments)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.Length).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.LengthFractionalInches)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PrintedComments)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.PromiseDate).HasColumnType("datetime");

                entity.Property(e => e.QuoteId).HasColumnName("QuoteID");

                entity.Property(e => e.SapepoxycoatColorId).HasColumnName("SAPEpoxycoatColorID");

                entity.Property(e => e.SapkleenkoteColorId).HasColumnName("SAPKleenkoteColorID");

                entity.Property(e => e.SapmaterialId).HasColumnName("SAPMaterialID");

                entity.Property(e => e.SapmaterialUnitOfMeasureId).HasColumnName("SAPMaterialUnitOfMeasureID");

                entity.Property(e => e.SapsalesInstructionId).HasColumnName("SAPSalesInstructionID");

                entity.Property(e => e.SapsalesOrderItemId).HasColumnName("SAPSalesOrderItemID");

                entity.Property(e => e.SapspecificationId).HasColumnName("SAPSpecificationID");

                entity.Property(e => e.SaptubeStandardId).HasColumnName("SAPTubeStandardID");

                entity.Property(e => e.StockMaterialNumber)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.StockSapcode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("StockSAPCode");

                entity.Property(e => e.WeightPerFoot).HasColumnType("decimal(17, 3)");

                entity.HasOne(d => d.Quote)
                    .WithMany(p => p.QuoteMaterials)
                    .HasForeignKey(d => d.QuoteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuoteMaterial_Quote");

                entity.HasOne(d => d.SapepoxycoatColor)
                    .WithMany(p => p.QuoteMaterialSapepoxycoatColors)
                    .HasForeignKey(d => d.SapepoxycoatColorId)
                    .HasConstraintName("FK_QuoteMaterial_SAPEpoxycoatColor");

                entity.HasOne(d => d.SapkleenkoteColor)
                    .WithMany(p => p.QuoteMaterialSapkleenkoteColors)
                    .HasForeignKey(d => d.SapkleenkoteColorId)
                    .HasConstraintName("FK_QuoteMaterial_SAPKleenkoteColor");

                entity.HasOne(d => d.Sapmaterial)
                    .WithMany(p => p.QuoteMaterials)
                    .HasForeignKey(d => d.SapmaterialId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuoteMaterial_SAPMaterial");

                entity.HasOne(d => d.SapmaterialUnitOfMeasure)
                    .WithMany(p => p.QuoteMaterialSapmaterialUnitOfMeasures)
                    .HasForeignKey(d => d.SapmaterialUnitOfMeasureId)
                    .HasConstraintName("FK_QuoteMaterial_SAPMaterialUnitOfMeasure");

                entity.HasOne(d => d.SapsalesInstruction)
                    .WithMany(p => p.QuoteMaterialSapsalesInstructions)
                    .HasForeignKey(d => d.SapsalesInstructionId)
                    .HasConstraintName("FK_QuoteMaterial_SAPSalesInstruction");

                entity.HasOne(d => d.SapsalesOrderItem)
                    .WithMany(p => p.QuoteMaterials)
                    .HasForeignKey(d => d.SapsalesOrderItemId)
                    .HasConstraintName("FK_QuoteMaterial_SAPSalesOrderItem");

                entity.HasOne(d => d.Sapspecification)
                    .WithMany(p => p.QuoteMaterialSapspecifications)
                    .HasForeignKey(d => d.SapspecificationId)
                    .HasConstraintName("FK_QuoteMaterial_SAPSpecification");

                entity.HasOne(d => d.SaptubeStandard)
                    .WithMany(p => p.QuoteMaterialSaptubeStandards)
                    .HasForeignKey(d => d.SaptubeStandardId)
                    .HasConstraintName("FK_QuoteMaterial_SAPTubeStandard");
            });

            modelBuilder.Entity<QuoteMaterialSapcondition>(entity =>
            {
                entity.ToTable("QuoteMaterialSAPCondition");

                entity.HasIndex(e => new { e.QuoteMaterialId, e.SapconditionId }, "IX_QuoteMaterialSAPPricingCondition")
                    .IsUnique();

                entity.Property(e => e.QuoteMaterialSapconditionId).HasColumnName("QuoteMaterialSAPConditionID");

                entity.Property(e => e.PricePerUnit)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.QuoteMaterialId).HasColumnName("QuoteMaterialID");

                entity.Property(e => e.Rate).HasColumnType("decimal(13, 2)");

                entity.Property(e => e.RateUnit)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.SapconditionId).HasColumnName("SAPConditionID");

                entity.Property(e => e.ValidFrom).HasColumnType("datetime");

                entity.Property(e => e.ValidTo).HasColumnType("datetime");

                entity.HasOne(d => d.QuoteMaterial)
                    .WithMany(p => p.QuoteMaterialSapconditions)
                    .HasForeignKey(d => d.QuoteMaterialId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuoteMaterialSAPCondition_QuoteMaterial");

                entity.HasOne(d => d.Sapcondition)
                    .WithMany(p => p.QuoteMaterialSapconditions)
                    .HasForeignKey(d => d.SapconditionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuoteMaterialSAPCondition_SAPCondition");
            });

            modelBuilder.Entity<QuoteSapcondition>(entity =>
            {
                entity.ToTable("QuoteSAPCondition");

                entity.HasIndex(e => new { e.QuoteId, e.SapconditionId }, "IX_QuoteSAPPricingCondition")
                    .IsUnique();

                entity.Property(e => e.QuoteSapconditionId).HasColumnName("QuoteSAPConditionID");

                entity.Property(e => e.PricePerUnit)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.QuoteId).HasColumnName("QuoteID");

                entity.Property(e => e.Rate).HasColumnType("decimal(13, 2)");

                entity.Property(e => e.RateUnit)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.SapconditionId).HasColumnName("SAPConditionID");

                entity.Property(e => e.ValidFrom).HasColumnType("datetime");

                entity.Property(e => e.ValidTo).HasColumnType("datetime");

                entity.HasOne(d => d.Quote)
                    .WithMany(p => p.QuoteSapconditions)
                    .HasForeignKey(d => d.QuoteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuoteSAPCondition_Quote");

                entity.HasOne(d => d.Sapcondition)
                    .WithMany(p => p.QuoteSapconditions)
                    .HasForeignKey(d => d.SapconditionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuoteSAPCondition_SAPCondition");
            });

            modelBuilder.Entity<QuoteStatus>(entity =>
            {
                entity.ToTable("QuoteStatus");

                entity.Property(e => e.QuoteStatusId).HasColumnName("QuoteStatusID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<QuoteStatusChange>(entity =>
            {
                entity.ToTable("QuoteStatusChange");

                entity.Property(e => e.QuoteStatusChangeId).HasColumnName("QuoteStatusChangeID");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.QuoteId).HasColumnName("QuoteID");

                entity.Property(e => e.QuoteStatusId).HasColumnName("QuoteStatusID");

                entity.HasOne(d => d.Quote)
                    .WithMany(p => p.QuoteStatusChanges)
                    .HasForeignKey(d => d.QuoteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuoteStatusChange_Quote");

                entity.HasOne(d => d.QuoteStatus)
                    .WithMany(p => p.QuoteStatusChanges)
                    .HasForeignKey(d => d.QuoteStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuoteStatusChange_QuoteStatus");
            });

            modelBuilder.Entity<RandomLengthSapsoldTo>(entity =>
            {
                entity.ToTable("RandomLengthSAPSoldTo");

                entity.HasIndex(e => new { e.SapsoldToId, e.LocationId }, "IX_RandomLengthSAPSoldTo")
                    .IsUnique();

                entity.Property(e => e.RandomLengthSapsoldToId).HasColumnName("RandomLengthSAPSoldToID");

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.SapsoldToId).HasColumnName("SAPSoldToID");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.RandomLengthSapsoldTos)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RandomLengthSAPSoldTo_Location");

                entity.HasOne(d => d.SapsoldTo)
                    .WithMany(p => p.RandomLengthSapsoldTos)
                    .HasForeignKey(d => d.SapsoldToId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RandomLengthSAPSoldTo_SAPSoldTo");
            });

            modelBuilder.Entity<Rectangle>(entity =>
            {
                entity.ToTable("Rectangle");

                entity.Property(e => e.RectangleId).HasColumnName("RectangleID");

                entity.Property(e => e.MaterialGroup)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Size1).HasColumnType("decimal(9, 4)");

                entity.Property(e => e.Size2).HasColumnType("decimal(9, 4)");
            });

            modelBuilder.Entity<Rolling>(entity =>
            {
                entity.ToTable("Rolling");

                entity.Property(e => e.RollingId).HasColumnName("RollingID");

                entity.Property(e => e.AllocatedQuantity).HasColumnType("decimal(17, 3)");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.PlannedQuantity).HasColumnType("decimal(17, 3)");

                entity.Property(e => e.PlantId).HasColumnName("PlantID");

                entity.Property(e => e.QuoteMaterialId).HasColumnName("QuoteMaterialID");

                entity.HasOne(d => d.Plant)
                    .WithMany(p => p.Rollings)
                    .HasForeignKey(d => d.PlantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rolling_Plant");

                entity.HasOne(d => d.QuoteMaterial)
                    .WithMany(p => p.Rollings)
                    .HasForeignKey(d => d.QuoteMaterialId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rolling_QuoteMaterial");
            });

            modelBuilder.Entity<Round>(entity =>
            {
                entity.ToTable("Round");

                entity.Property(e => e.RoundId).HasColumnName("RoundID");

                entity.Property(e => e.MaterialGroup)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nps).HasColumnName("NPS");

                entity.Property(e => e.Npssize)
                    .HasColumnType("decimal(9, 4)")
                    .HasColumnName("NPSSize");

                entity.Property(e => e.Size).HasColumnType("decimal(9, 4)");
            });

            modelBuilder.Entity<SapbundlingOption>(entity =>
            {
                entity.ToTable("SAPBundlingOption");

                entity.HasIndex(e => new { e.LengthLow, e.LengthHigh, e.Bundling1, e.Bundling2 }, "IX_SAPBundlingOption");

                entity.Property(e => e.SapbundlingOptionId).HasColumnName("SAPBundlingOptionID");

                entity.Property(e => e.DivisionId).HasColumnName("DivisionID");

                entity.Property(e => e.LengthHigh).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.LengthLow).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Plant).HasMaxLength(10);

                entity.HasOne(d => d.Division)
                    .WithMany(p => p.SapbundlingOptions)
                    .HasForeignKey(d => d.DivisionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPBundlingOption_Division");
            });

            modelBuilder.Entity<SapcharacteristicOption>(entity =>
            {
                entity.ToTable("SAPCharacteristicOption");

                entity.HasIndex(e => new { e.SapcharacteristicTypeId, e.Sapcode }, "IX_SAPCharacteristicOption")
                    .IsUnique()
                    .HasFillFactor(100);

                entity.Property(e => e.SapcharacteristicOptionId).HasColumnName("SAPCharacteristicOptionID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductLineId).HasColumnName("ProductLineID");

                entity.Property(e => e.SapcharacteristicTypeId).HasColumnName("SAPCharacteristicTypeID");

                entity.Property(e => e.Sapcode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SAPCode");

                entity.HasOne(d => d.ProductLine)
                    .WithMany(p => p.SapcharacteristicOptions)
                    .HasForeignKey(d => d.ProductLineId)
                    .HasConstraintName("FK_SAPCharacteristicOption_ProductLine");

                entity.HasOne(d => d.SapcharacteristicType)
                    .WithMany(p => p.SapcharacteristicOptions)
                    .HasForeignKey(d => d.SapcharacteristicTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPCharacteristicOption_SAPCharacteristicType");
            });

            modelBuilder.Entity<SapcharacteristicType>(entity =>
            {
                entity.ToTable("SAPCharacteristicType");

                entity.HasIndex(e => new { e.DivisionId, e.Name }, "IX_SAPCharacteristicType")
                    .IsUnique();

                entity.Property(e => e.SapcharacteristicTypeId).HasColumnName("SAPCharacteristicTypeID");

                entity.Property(e => e.DivisionId).HasColumnName("DivisionID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sapcode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SAPCode");

                entity.HasOne(d => d.Division)
                    .WithMany(p => p.SapcharacteristicTypes)
                    .HasForeignKey(d => d.DivisionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPCharacteristicType_Division");
            });

            modelBuilder.Entity<Sapcondition>(entity =>
            {
                entity.ToTable("SAPCondition");

                entity.Property(e => e.SapconditionId).HasColumnName("SAPConditionID");

                entity.Property(e => e.Application)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.DivisionId).HasColumnName("DivisionID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sapcode)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("SAPCode");

                entity.HasOne(d => d.Division)
                    .WithMany(p => p.Sapconditions)
                    .HasForeignKey(d => d.DivisionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPCondition_Division");
            });

            modelBuilder.Entity<SapconditionGroup>(entity =>
            {
                entity.ToTable("SAPConditionGroup");

                entity.HasIndex(e => e.Sapcode, "IX_SAPConditionGroup")
                    .IsUnique();

                entity.Property(e => e.SapconditionGroupId).HasColumnName("SAPConditionGroupID");

                entity.Property(e => e.DivisionId).HasColumnName("DivisionID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sapcode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SAPCode");

                entity.HasOne(d => d.Division)
                    .WithMany(p => p.SapconditionGroups)
                    .HasForeignKey(d => d.DivisionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPConditionGroup_Division");
            });

            modelBuilder.Entity<SapcustomerGroup>(entity =>
            {
                entity.ToTable("SAPCustomerGroup");

                entity.HasIndex(e => e.Sapcode, "IX_SAPCustomerGroup")
                    .IsUnique();

                entity.Property(e => e.SapcustomerGroupId).HasColumnName("SAPCustomerGroupID");

                entity.Property(e => e.DivisionId).HasColumnName("DivisionID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RegionalManagerUserId).HasColumnName("RegionalManagerUserID");

                entity.Property(e => e.Sapcode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SAPCode");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Division)
                    .WithMany(p => p.SapcustomerGroups)
                    .HasForeignKey(d => d.DivisionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPCustomerGroup_Division");

                entity.HasOne(d => d.RegionalManagerUser)
                    .WithMany(p => p.SapcustomerGroupRegionalManagerUsers)
                    .HasForeignKey(d => d.RegionalManagerUserId)
                    .HasConstraintName("FK_SAPCustomerGroup_RegionalManagerUser");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SapcustomerGroupUsers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_SAPCustomerGroup_User");
            });

            modelBuilder.Entity<SapcustomerGroupSapsalesOrganization>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("SAPCustomerGroupSAPSalesOrganization");

                entity.HasIndex(e => new { e.SapcustomerGroupId, e.SapsalesOrganizationId }, "IX_SAPCustomerGroupSAPSalesOrganization")
                    .IsUnique();

                entity.Property(e => e.SapcustomerGroupId).HasColumnName("SAPCustomerGroupID");

                entity.Property(e => e.SapsalesOrganizationId).HasColumnName("SAPSalesOrganizationID");

                entity.HasOne(d => d.SapcustomerGroup)
                    .WithMany()
                    .HasForeignKey(d => d.SapcustomerGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPCustomerGroupSAPSalesOrganization_SAPCustomerGroup");

                entity.HasOne(d => d.SapsalesOrganization)
                    .WithMany()
                    .HasForeignKey(d => d.SapsalesOrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPCustomerGroupSAPSalesOrganization_SAPSalesOrganization");
            });

            modelBuilder.Entity<SapcustomerGroupUser>(entity =>
            {
                entity.ToTable("SAPCustomerGroupUser");

                entity.Property(e => e.SapcustomerGroupUserId).HasColumnName("SAPCustomerGroupUserID");

                entity.Property(e => e.SapcustomerGroupId).HasColumnName("SAPCustomerGroupID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.SapcustomerGroup)
                    .WithMany(p => p.SapcustomerGroupUsers)
                    .HasForeignKey(d => d.SapcustomerGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPCustomerGroupUser_SAPCustomerGroup");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SapcustomerGroupUsersNavigation)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPCustomerGroupUser_User");
            });

            modelBuilder.Entity<SapcustomerServiceRep>(entity =>
            {
                entity.ToTable("SAPCustomerServiceRep");

                entity.Property(e => e.SapcustomerServiceRepId).HasColumnName("SAPCustomerServiceRepID");

                entity.Property(e => e.DivisionId).HasColumnName("DivisionID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sapcode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SAPCode");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Division)
                    .WithMany(p => p.SapcustomerServiceReps)
                    .HasForeignKey(d => d.DivisionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPCustomerServiceRep_Division");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SapcustomerServiceReps)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_SAPCustomerServiceRep_User");
            });

            modelBuilder.Entity<SapcustomerServiceRepSapsalesOrganization>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("SAPCustomerServiceRepSAPSalesOrganization");

                entity.Property(e => e.SapcustomerServiceRepId).HasColumnName("SAPCustomerServiceRepID");

                entity.Property(e => e.SapsalesOrganizationId).HasColumnName("SAPSalesOrganizationID");

                entity.HasOne(d => d.SapcustomerServiceRep)
                    .WithMany()
                    .HasForeignKey(d => d.SapcustomerServiceRepId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPCustomerServiceRepSAPSalesOrganization_SAPCustomerServiceRep");

                entity.HasOne(d => d.SapsalesOrganization)
                    .WithMany()
                    .HasForeignKey(d => d.SapsalesOrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPCustomerServiceRepSAPSalesOrganization_SAPSalesOrganization");
            });

            modelBuilder.Entity<SapcustomerText>(entity =>
            {
                entity.ToTable("SAPCustomerText");

                entity.HasIndex(e => new { e.SapcustomerTextTypeId, e.SapsalesOrganizationId, e.SapshipToId, e.LineNumber }, "IX_SAPCustomerText")
                    .IsUnique()
                    .HasFillFactor(100);

                entity.HasIndex(e => e.SapshipToId, "missing_index_38_37_SAPCustomerText")
                    .HasFillFactor(100);

                entity.Property(e => e.SapcustomerTextId).HasColumnName("SAPCustomerTextID");

                entity.Property(e => e.SapcustomerTextTypeId).HasColumnName("SAPCustomerTextTypeID");

                entity.Property(e => e.SapsalesOrganizationId).HasColumnName("SAPSalesOrganizationID");

                entity.Property(e => e.SapshipToId).HasColumnName("SAPShipToID");

                entity.Property(e => e.Text)
                    .HasMaxLength(132)
                    .IsUnicode(false);

                entity.HasOne(d => d.SapcustomerTextType)
                    .WithMany(p => p.SapcustomerTexts)
                    .HasForeignKey(d => d.SapcustomerTextTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPCustomerText_SAPCharacteristicOption");

                entity.HasOne(d => d.SapsalesOrganization)
                    .WithMany(p => p.SapcustomerTexts)
                    .HasForeignKey(d => d.SapsalesOrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPCustomerText_SAPSalesOrganization");

                entity.HasOne(d => d.SapshipTo)
                    .WithMany(p => p.SapcustomerTexts)
                    .HasForeignKey(d => d.SapshipToId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPCustomerText_SAPShipTo");
            });

            modelBuilder.Entity<Sapdelivery>(entity =>
            {
                entity.ToTable("SAPDelivery");

                entity.HasIndex(e => new { e.ActualGoodsMovementDate, e.Tmsdeldate }, "IDX_SAPDELIVERY_DELIVERED");

                entity.HasIndex(e => new { e.DivisionId, e.PlantId, e.SapsoldToId }, "IDX_SAPDELIVERY_DIVPLANTSOLDTO");

                entity.HasIndex(e => new { e.Number, e.DivisionId }, "IX_SAPDelivery")
                    .IsUnique();

                entity.HasIndex(e => e.ActualGoodsMovementDate, "IX_SAPDelivery_ActlGoodsMvmtDate");

                entity.HasIndex(e => new { e.DivisionId, e.ActualGoodsMovementDate, e.SapsoldToId }, "missing_index_10_9_SAPDelivery");

                entity.Property(e => e.SapdeliveryId).HasColumnName("SAPDeliveryID");

                entity.Property(e => e.AccessCharges).HasColumnType("decimal(13, 2)");

                entity.Property(e => e.ActualGoodsMovementDate).HasColumnType("datetime");

                entity.Property(e => e.BapireturnMessage)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("BAPIReturnMessage");

                entity.Property(e => e.CarrierCadexchangeRateUsed)
                    .HasColumnType("decimal(10, 4)")
                    .HasColumnName("CarrierCADExchangeRateUsed");

                entity.Property(e => e.CarrierFreightCurrency)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.CheckoutDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.DeliveryType)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Distance).HasColumnType("decimal(10, 0)");

                entity.Property(e => e.DistanceUom)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("DistanceUOM");

                entity.Property(e => e.DivisionId).HasColumnName("DivisionID");

                entity.Property(e => e.EstimatedCost).HasColumnType("decimal(13, 2)");

                entity.Property(e => e.FreightPaidToCarrier).HasColumnType("decimal(13, 2)");

                entity.Property(e => e.Fsc)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("FSC");

                entity.Property(e => e.IncoTerms2)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IpsactualWeight)
                    .HasColumnType("decimal(18, 4)")
                    .HasColumnName("IPSActualWeight");

                entity.Property(e => e.IpsgotPaidWeight).HasColumnName("IPSGotPaidWeight");

                entity.Property(e => e.Number)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PickupAptDate).HasColumnType("datetime");

                entity.Property(e => e.PlantId).HasColumnName("PlantID");

                entity.Property(e => e.Reqdeldate)
                    .HasColumnType("datetime")
                    .HasColumnName("REQDELDate");

                entity.Property(e => e.SapshipmentId).HasColumnName("SAPShipmentID");

                entity.Property(e => e.SapsoldToId).HasColumnName("SAPSoldToID");

                entity.Property(e => e.SapvendorId).HasColumnName("SAPVendorID");

                entity.Property(e => e.ShipmentNumber)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Tmsdeldate).HasColumnName("TMSDELDate");

                entity.Property(e => e.TransportationPlanningDate).HasColumnType("datetime");

                entity.Property(e => e.UsdfreightPaidToCarrier)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("USDFreightPaidToCarrier");

                entity.Property(e => e.Weight).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.WeightUnit)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.Division)
                    .WithMany(p => p.Sapdeliveries)
                    .HasForeignKey(d => d.DivisionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPDelivery_Division");

                entity.HasOne(d => d.Plant)
                    .WithMany(p => p.Sapdeliveries)
                    .HasForeignKey(d => d.PlantId)
                    .HasConstraintName("FK_SAPDelivery_Plant");

                entity.HasOne(d => d.Sapshipment)
                    .WithMany(p => p.Sapdeliveries)
                    .HasForeignKey(d => d.SapshipmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPDelivery_SAPShipment");

                entity.HasOne(d => d.SapsoldTo)
                    .WithMany(p => p.Sapdeliveries)
                    .HasForeignKey(d => d.SapsoldToId)
                    .HasConstraintName("FK_SAPDelivery_SAPSoldTo");

                entity.HasOne(d => d.Sapvendor)
                    .WithMany(p => p.Sapdeliveries)
                    .HasForeignKey(d => d.SapvendorId)
                    .HasConstraintName("FK_SAPDelivery_SAPVendor");
            });

            modelBuilder.Entity<SapdeliveryItem>(entity =>
            {
                entity.ToTable("SAPDeliveryItem");

                entity.HasIndex(e => new { e.SapdeliveryId, e.Position }, "IX_SAPDeliveryItem")
                    .IsUnique();

                entity.HasIndex(e => e.SapsalesOrderItemId, "missing_index_26_25_SAPDeliveryItem");

                entity.Property(e => e.SapdeliveryItemId).HasColumnName("SAPDeliveryItemID");

                entity.Property(e => e.QuantityDelivered).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SalesUnit)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.SapdeliveryId).HasColumnName("SAPDeliveryID");

                entity.Property(e => e.SapsalesOrderItemId).HasColumnName("SAPSalesOrderItemID");

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Weight).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.WeightUnit)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.Sapdelivery)
                    .WithMany(p => p.SapdeliveryItems)
                    .HasForeignKey(d => d.SapdeliveryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPDeliveryItem_SAPDelivery");

                entity.HasOne(d => d.SapsalesOrderItem)
                    .WithMany(p => p.SapdeliveryItems)
                    .HasForeignKey(d => d.SapsalesOrderItemId)
                    .HasConstraintName("FK_SAPDeliveryItem_SAPSalesOrderItem");
            });

            modelBuilder.Entity<SapdeliveryType>(entity =>
            {
                entity.ToTable("SAPDeliveryType");

                entity.Property(e => e.SapdeliveryTypeId).HasColumnName("SAPDeliveryTypeID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sapcode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SAPCode");
            });

            modelBuilder.Entity<Sapmaterial>(entity =>
            {
                entity.ToTable("SAPMaterial");

                entity.HasIndex(e => new { e.DivisionId, e.Number }, "IX_SAPMaterial")
                    .IsUnique()
                    .HasFillFactor(100);

                entity.HasIndex(e => e.Number, "missing_index_20_19_SAPMaterial")
                    .HasFillFactor(100);

                entity.Property(e => e.SapmaterialId).HasColumnName("SAPMaterialID");

                entity.Property(e => e.BundleWeight).HasColumnType("decimal(17, 3)");

                entity.Property(e => e.CommissionGroup)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Diameter).HasColumnType("decimal(17, 3)");

                entity.Property(e => e.DivisionId).HasColumnName("DivisionID");

                entity.Property(e => e.GaugeRestrictable).HasColumnType("decimal(17, 3)");

                entity.Property(e => e.GrossWeight).HasColumnType("decimal(17, 3)");

                entity.Property(e => e.Length).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.LengthFractionalInches)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NetWeight).HasColumnType("decimal(17, 3)");

                entity.Property(e => e.Npsdescription)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NPSDescription");

                entity.Property(e => e.Number)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.OldMaterialNumber)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.PieceWeight).HasColumnType("decimal(17, 3)");

                entity.Property(e => e.PlanningMaterial).HasMaxLength(25);

                entity.Property(e => e.ProductGauge)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ProductSize)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.SapalternateCoilIndicatorId).HasColumnName("SAPAlternateCoilIndicatorID");

                entity.Property(e => e.SapepoxycoatColorId).HasColumnName("SAPEpoxycoatColorID");

                entity.Property(e => e.SapkleenkoteColorId).HasColumnName("SAPKleenkoteColorID");

                entity.Property(e => e.SapmaterialGroupId).HasColumnName("SAPMaterialGroupID");

                entity.Property(e => e.SapmaterialPricingGroupId).HasColumnName("SAPMaterialPricingGroupID");

                entity.Property(e => e.SapmaterialTypeId).HasColumnName("SAPMaterialTypeID");

                entity.Property(e => e.SapmetalGradeId).HasColumnName("SAPMetalGradeID");

                entity.Property(e => e.SappricingGroupId).HasColumnName("SAPPricingGroupID");

                entity.Property(e => e.SapproductColorFinishId).HasColumnName("SAPProductColorFinishID");

                entity.Property(e => e.SapproductEndFinishEnergexId).HasColumnName("SAPProductEndFinishEnergexID");

                entity.Property(e => e.SapproductEndFinishId).HasColumnName("SAPProductEndFinishID");

                entity.Property(e => e.SapproductGroupId).HasColumnName("SAPProductGroupID");

                entity.Property(e => e.SapproductLineId).HasColumnName("SAPProductLineID");

                entity.Property(e => e.SapproductTypeId).HasColumnName("SAPProductTypeID");

                entity.Property(e => e.SapsalesInstructionId).HasColumnName("SAPSalesInstructionID");

                entity.Property(e => e.SapspecificationId).HasColumnName("SAPSpecificationID");

                entity.Property(e => e.SaptubeShapeId).HasColumnName("SAPTubeShapeID");

                entity.Property(e => e.SaptubeStandardId).HasColumnName("SAPTubeStandardID");

                entity.Property(e => e.Size).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Size2).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.TotalBundleLength).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.VolumeRebateGroup)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.WeightPerFoot).HasColumnType("decimal(17, 3)");

                entity.HasOne(d => d.Division)
                    .WithMany(p => p.Sapmaterials)
                    .HasForeignKey(d => d.DivisionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPMaterial_Division");

                entity.HasOne(d => d.SapalternateCoilIndicator)
                    .WithMany(p => p.SapmaterialSapalternateCoilIndicators)
                    .HasForeignKey(d => d.SapalternateCoilIndicatorId)
                    .HasConstraintName("FK_SAPMaterial_SAPAlternateCoilIndicator");

                entity.HasOne(d => d.SapepoxycoatColor)
                    .WithMany(p => p.SapmaterialSapepoxycoatColors)
                    .HasForeignKey(d => d.SapepoxycoatColorId)
                    .HasConstraintName("FK_SAPMaterial_SAPEpoxycoatColor");

                entity.HasOne(d => d.SapkleenkoteColor)
                    .WithMany(p => p.SapmaterialSapkleenkoteColors)
                    .HasForeignKey(d => d.SapkleenkoteColorId)
                    .HasConstraintName("FK_SAPMaterial_SAPKleenkoteColor");

                entity.HasOne(d => d.SapmaterialGroup)
                    .WithMany(p => p.SapmaterialSapmaterialGroups)
                    .HasForeignKey(d => d.SapmaterialGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPMaterial_SAPMaterialGroup");

                entity.HasOne(d => d.SapmaterialPricingGroup)
                    .WithMany(p => p.SapmaterialSapmaterialPricingGroups)
                    .HasForeignKey(d => d.SapmaterialPricingGroupId)
                    .HasConstraintName("FK_SAPMaterial_SAPMaterialPricingGroup");

                entity.HasOne(d => d.SapmaterialType)
                    .WithMany(p => p.SapmaterialSapmaterialTypes)
                    .HasForeignKey(d => d.SapmaterialTypeId)
                    .HasConstraintName("FK_SAPMaterial_SAPMaterialType");

                entity.HasOne(d => d.SapmetalGrade)
                    .WithMany(p => p.SapmaterialSapmetalGrades)
                    .HasForeignKey(d => d.SapmetalGradeId)
                    .HasConstraintName("FK_SAPMaterial_SAPMetalGrade");

                entity.HasOne(d => d.SappricingGroup)
                    .WithMany(p => p.SapmaterialSappricingGroups)
                    .HasForeignKey(d => d.SappricingGroupId)
                    .HasConstraintName("FK_SAPMaterial_SAPPricingGroup");

                entity.HasOne(d => d.SapproductColorFinish)
                    .WithMany(p => p.SapmaterialSapproductColorFinishes)
                    .HasForeignKey(d => d.SapproductColorFinishId)
                    .HasConstraintName("FK_SAPMaterial_SAPProductColorFinish");

                entity.HasOne(d => d.SapproductEndFinishEnergex)
                    .WithMany(p => p.SapmaterialSapproductEndFinishEnergices)
                    .HasForeignKey(d => d.SapproductEndFinishEnergexId)
                    .HasConstraintName("FK_SAPMaterial_SAPProductEndFinishEnergexID");

                entity.HasOne(d => d.SapproductEndFinish)
                    .WithMany(p => p.SapmaterialSapproductEndFinishes)
                    .HasForeignKey(d => d.SapproductEndFinishId)
                    .HasConstraintName("FK_SAPMaterial_SAPProductEndFinish");

                entity.HasOne(d => d.SapproductGroup)
                    .WithMany(p => p.SapmaterialSapproductGroups)
                    .HasForeignKey(d => d.SapproductGroupId)
                    .HasConstraintName("FK_SAPMaterial_SAPProductGroup");

                entity.HasOne(d => d.SapproductLine)
                    .WithMany(p => p.SapmaterialSapproductLines)
                    .HasForeignKey(d => d.SapproductLineId)
                    .HasConstraintName("FK_SAPMaterial_SAPProductLine");

                entity.HasOne(d => d.SapproductType)
                    .WithMany(p => p.SapmaterialSapproductTypes)
                    .HasForeignKey(d => d.SapproductTypeId)
                    .HasConstraintName("FK_SAPMaterial_SAPProductType");

                entity.HasOne(d => d.SapsalesInstruction)
                    .WithMany(p => p.SapmaterialSapsalesInstructions)
                    .HasForeignKey(d => d.SapsalesInstructionId)
                    .HasConstraintName("FK_SAPMaterial_SAPSalesInstruction");

                entity.HasOne(d => d.Sapspecification)
                    .WithMany(p => p.SapmaterialSapspecifications)
                    .HasForeignKey(d => d.SapspecificationId)
                    .HasConstraintName("FK_SAPMaterial_SAPSpecification");

                entity.HasOne(d => d.SaptubeShape)
                    .WithMany(p => p.SapmaterialSaptubeShapes)
                    .HasForeignKey(d => d.SaptubeShapeId)
                    .HasConstraintName("FK_SAPMaterial_SAPTubeShape");

                entity.HasOne(d => d.SaptubeStandard)
                    .WithMany(p => p.SapmaterialSaptubeStandards)
                    .HasForeignKey(d => d.SaptubeStandardId)
                    .HasConstraintName("FK_SAPMaterial_SAPTubeStandard");
            });

            modelBuilder.Entity<SapmaterialPlant>(entity =>
            {
                entity.ToTable("SAPMaterialPlant");

                entity.HasIndex(e => new { e.SapmaterialId, e.PlantId }, "IX_SAPMaterialPlant")
                    .IsUnique()
                    .HasFillFactor(100);

                entity.Property(e => e.SapmaterialPlantId).HasColumnName("SAPMaterialPlantID");

                entity.Property(e => e.PlantId).HasColumnName("PlantID");

                entity.Property(e => e.PriceControlIndicator)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.PriceUnit).HasColumnType("decimal(7, 2)");

                entity.Property(e => e.SapmaterialId).HasColumnName("SAPMaterialID");

                entity.Property(e => e.StandardPrice).HasColumnType("decimal(13, 2)");

                entity.HasOne(d => d.Plant)
                    .WithMany(p => p.SapmaterialPlants)
                    .HasForeignKey(d => d.PlantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPMaterialPlant_Plant");

                entity.HasOne(d => d.Sapmaterial)
                    .WithMany(p => p.SapmaterialPlants)
                    .HasForeignKey(d => d.SapmaterialId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPMaterialPlant_SAPMaterial");
            });

            modelBuilder.Entity<SapmaterialSapbundlingOption>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("SAPMaterialSAPBundlingOption");

                entity.HasIndex(e => new { e.SapmaterialId, e.SapbundlingOptionId }, "IX_SAPMaterialSAPBundlingOption")
                    .IsUnique();

                entity.Property(e => e.SapbundlingOptionId).HasColumnName("SAPBundlingOptionID");

                entity.Property(e => e.SapmaterialId).HasColumnName("SAPMaterialID");

                entity.HasOne(d => d.SapbundlingOption)
                    .WithMany()
                    .HasForeignKey(d => d.SapbundlingOptionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPMaterialSAPBundlingOption_SAPBundlingOption");

                entity.HasOne(d => d.Sapmaterial)
                    .WithMany()
                    .HasForeignKey(d => d.SapmaterialId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPMaterialSAPBundlingOption_SAPMaterial");
            });

            modelBuilder.Entity<SapmaterialUnitOfMeasure>(entity =>
            {
                entity.ToTable("SAPMaterialUnitOfMeasure");

                entity.Property(e => e.SapmaterialUnitOfMeasureId).HasColumnName("SAPMaterialUnitOfMeasureID");

                entity.Property(e => e.AlternateUnitOfMeasureId).HasColumnName("AlternateUnitOfMeasureID");

                entity.Property(e => e.BaseUnitOfMeasureId).HasColumnName("BaseUnitOfMeasureID");

                entity.Property(e => e.Denominator).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.Numerator).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.SapmaterialId).HasColumnName("SAPMaterialID");

                entity.HasOne(d => d.AlternateUnitOfMeasure)
                    .WithMany(p => p.SapmaterialUnitOfMeasureAlternateUnitOfMeasures)
                    .HasForeignKey(d => d.AlternateUnitOfMeasureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPMaterialUnitOfMeasure_AlternateSAPUnitOfMeasure");

                entity.HasOne(d => d.BaseUnitOfMeasure)
                    .WithMany(p => p.SapmaterialUnitOfMeasureBaseUnitOfMeasures)
                    .HasForeignKey(d => d.BaseUnitOfMeasureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPMaterialUnitOfMeasure_BaseSAPUnitOfMeasure");

                entity.HasOne(d => d.Sapmaterial)
                    .WithMany(p => p.SapmaterialUnitOfMeasures)
                    .HasForeignKey(d => d.SapmaterialId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPMaterialUnitOfMeasure_SAPMaterial");
            });

            modelBuilder.Entity<Sapregion>(entity =>
            {
                entity.ToTable("SAPRegion");

                entity.Property(e => e.SapregionId).HasColumnName("SAPRegionID");

                entity.Property(e => e.DivisionId).HasColumnName("DivisionID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sapcode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SAPCode");

                entity.HasOne(d => d.Division)
                    .WithMany(p => p.Sapregions)
                    .HasForeignKey(d => d.DivisionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPRegion_Division");
            });

            modelBuilder.Entity<SapregionSapsalesOrganization>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("SAPRegionSAPSalesOrganization");

                entity.Property(e => e.SapregionId).HasColumnName("SAPRegionID");

                entity.Property(e => e.SapsalesOrganizationId).HasColumnName("SAPSalesOrganizationID");

                entity.HasOne(d => d.Sapregion)
                    .WithMany()
                    .HasForeignKey(d => d.SapregionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPRegionSAPSalesOrganization_SAPRegion");

                entity.HasOne(d => d.SapsalesOrganization)
                    .WithMany()
                    .HasForeignKey(d => d.SapsalesOrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPRegionSAPSalesOrganization_SAPSalesOrganization");
            });

            modelBuilder.Entity<SapsalesDelivery>(entity =>
            {
                entity.HasKey(e => e.SapdeliveryId);

                entity.ToTable("SAPSalesDelivery");

                entity.Property(e => e.SapdeliveryId)
                    .ValueGeneratedNever()
                    .HasColumnName("SAPDeliveryID");

                entity.Property(e => e.CustomerCadexchangeRateUsed)
                    .HasColumnType("decimal(18, 4)")
                    .HasColumnName("CustomerCADExchangeRateUsed");

                entity.Property(e => e.CustomerFreightCurrency)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.EqualizedFreight)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.FreightChargedToCustomer).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.InvoiceNumbers)
                    .HasMaxLength(600)
                    .IsUnicode(false);

                entity.Property(e => e.Length).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SapshipToId).HasColumnName("SAPShipToID");

                entity.Property(e => e.SapstorageLocationId).HasColumnName("SAPStorageLocationID");

                entity.Property(e => e.TrailorNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UsdfreightChargedToCustomer)
                    .HasColumnType("decimal(18, 4)")
                    .HasColumnName("USDFreightChargedToCustomer");

                entity.Property(e => e.WarehouseNumber)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.Sapdelivery)
                    .WithOne(p => p.SapsalesDelivery)
                    .HasForeignKey<SapsalesDelivery>(d => d.SapdeliveryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPSalesDelivery_SAPDelivery");

                entity.HasOne(d => d.SapshipTo)
                    .WithMany(p => p.SapsalesDeliveries)
                    .HasForeignKey(d => d.SapshipToId)
                    .HasConstraintName("FK_SAPSalesDelivery_SAPShipTo");

                entity.HasOne(d => d.SapstorageLocation)
                    .WithMany(p => p.SapsalesDeliveries)
                    .HasForeignKey(d => d.SapstorageLocationId)
                    .HasConstraintName("FK_SAPSalesDelivery_SAPStorageLocation");
            });

            modelBuilder.Entity<SapsalesGroup>(entity =>
            {
                entity.ToTable("SAPSalesGroup");

                entity.HasIndex(e => e.Sapcode, "IX_SAPSalesGroup")
                    .IsUnique();

                entity.Property(e => e.SapsalesGroupId).HasColumnName("SAPSalesGroupID");

                entity.Property(e => e.AlternateIsr).HasColumnName("AlternateISR");

                entity.Property(e => e.BoldChatId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("BoldChatID");

                entity.Property(e => e.DivisionId).HasColumnName("DivisionID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sapcode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SAPCode");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.AlternateIsrNavigation)
                    .WithMany(p => p.SapsalesGroupAlternateIsrNavigations)
                    .HasForeignKey(d => d.AlternateIsr)
                    .HasConstraintName("FK_SAPSalesGroup_User1");

                entity.HasOne(d => d.Division)
                    .WithMany(p => p.SapsalesGroups)
                    .HasForeignKey(d => d.DivisionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPSalesGroup_Division");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SapsalesGroupUsers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_SAPSalesGroup_User");
            });

            modelBuilder.Entity<SapsalesGroupSapsalesOrganization>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("SAPSalesGroupSAPSalesOrganization");

                entity.HasIndex(e => new { e.SapsalesGroupId, e.SapsalesOrganizationId }, "IX_SAPSalesGroupSAPSalesOrganization")
                    .IsUnique();

                entity.Property(e => e.SapsalesGroupId).HasColumnName("SAPSalesGroupID");

                entity.Property(e => e.SapsalesOrganizationId).HasColumnName("SAPSalesOrganizationID");

                entity.HasOne(d => d.SapsalesGroup)
                    .WithMany()
                    .HasForeignKey(d => d.SapsalesGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPSalesGroupSAPSalesOrganization_SAPSalesGroup");

                entity.HasOne(d => d.SapsalesOrganization)
                    .WithMany()
                    .HasForeignKey(d => d.SapsalesOrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPSalesGroupSAPSalesOrganization_SAPSalesOrganization");
            });

            modelBuilder.Entity<SapsalesOrder>(entity =>
            {
                entity.ToTable("SAPSalesOrder");

                entity.HasIndex(e => new { e.DivisionId, e.Number }, "IX_SAPSalesOrder")
                    .IsUnique();

                entity.HasIndex(e => e.Date, "Prf_SODate");

                entity.HasIndex(e => e.Number, "missing_index_35_34_SAPSalesOrder");

                entity.HasIndex(e => e.SapsoldToId, "missing_index_7_6_SAPSalesOrder")
                    .HasFillFactor(100);

                entity.Property(e => e.SapsalesOrderId).HasColumnName("SAPSalesOrderID");

                entity.Property(e => e.BapireturnMessage)
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("BAPIReturnMessage");

                entity.Property(e => e.CreditStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DealId).HasColumnName("DealID");

                entity.Property(e => e.DeliveryBlock)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.DeliveryBlockText)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DistributionChannel)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.DivisionId).HasColumnName("DivisionID");

                entity.Property(e => e.DocumentType)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Number)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.PlantId).HasColumnName("PlantID");

                entity.Property(e => e.Podate)
                    .HasColumnType("date")
                    .HasColumnName("PODate");

                entity.Property(e => e.Ponumber)
                    .HasMaxLength(35)
                    .IsUnicode(false)
                    .HasColumnName("PONumber");

                entity.Property(e => e.QuoteId).HasColumnName("QuoteID");

                entity.Property(e => e.SapshipToId).HasColumnName("SAPShipToID");

                entity.Property(e => e.SapsoldToId).HasColumnName("SAPSoldToID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.YourReference).HasMaxLength(50);

                entity.HasOne(d => d.Division)
                    .WithMany(p => p.SapsalesOrders)
                    .HasForeignKey(d => d.DivisionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPSalesOrder_Division");

                entity.HasOne(d => d.Plant)
                    .WithMany(p => p.SapsalesOrders)
                    .HasForeignKey(d => d.PlantId)
                    .HasConstraintName("FK_SAPSalesOrder_Plant");

                entity.HasOne(d => d.Quote)
                    .WithMany(p => p.SapsalesOrders)
                    .HasForeignKey(d => d.QuoteId)
                    .HasConstraintName("FK_SAPSalesOrder_Quote");

                entity.HasOne(d => d.SapshipTo)
                    .WithMany(p => p.SapsalesOrders)
                    .HasForeignKey(d => d.SapshipToId)
                    .HasConstraintName("FK_SAPSalesOrder_SAPShipTo");

                entity.HasOne(d => d.SapsoldTo)
                    .WithMany(p => p.SapsalesOrders)
                    .HasForeignKey(d => d.SapsoldToId)
                    .HasConstraintName("FK_SAPSalesOrder_SAPSoldTo");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SapsalesOrders)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_SAPSalesOrder_User");
            });

            modelBuilder.Entity<SapsalesOrderItem>(entity =>
            {
                entity.ToTable("SAPSalesOrderItem");

                entity.HasIndex(e => new { e.SapsalesOrderId, e.Position, e.SapshipToId }, "IX_SAPSalesOrderItem")
                    .IsUnique();

                entity.Property(e => e.SapsalesOrderItemId).HasColumnName("SAPSalesOrderItemID");

                entity.Property(e => e.BaseUnit)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.ConfirmedQuantity).HasColumnType("decimal(18, 3)");

                entity.Property(e => e.CustomerMaterialNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DeliveryStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.DisplayDeliveredWeight).HasColumnType("decimal(18, 3)");

                entity.Property(e => e.DisplayNotReadyWeight).HasColumnType("decimal(18, 3)");

                entity.Property(e => e.DisplayReadyWeight).HasColumnType("decimal(18, 3)");

                entity.Property(e => e.DisplayReleasedWeight).HasColumnType("decimal(18, 3)");

                entity.Property(e => e.GrossWeight).HasColumnType("decimal(18, 3)");

                entity.Property(e => e.ItemCategory)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.MaterialShortDescription)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.MaterialStagingDate).HasColumnType("date");

                entity.Property(e => e.NotReadyWeight).HasColumnType("decimal(18, 3)");

                entity.Property(e => e.OpenQuantity).HasColumnType("decimal(18, 3)");

                entity.Property(e => e.OrderQuantity).HasColumnType("decimal(18, 3)");

                entity.Property(e => e.PlantId).HasColumnName("PlantID");

                entity.Property(e => e.PolineNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("POLineNumber");

                entity.Property(e => e.Price).HasColumnType("decimal(13, 2)");

                entity.Property(e => e.ReadyDate).HasColumnType("date");

                entity.Property(e => e.ReadyWeight).HasColumnType("decimal(18, 3)");

                entity.Property(e => e.RecordSource)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.RejectionCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.RequirementsType)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.RollDate).HasColumnType("date");

                entity.Property(e => e.SalesUnit)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.SapmaterialId).HasColumnName("SAPMaterialID");

                entity.Property(e => e.SapsalesOrderId).HasColumnName("SAPSalesOrderID");

                entity.Property(e => e.SapshipToId).HasColumnName("SAPShipToID");

                entity.Property(e => e.ScheduleLineDate).HasColumnType("date");

                entity.Property(e => e.ScheduleOrderQuantity).HasColumnType("decimal(18, 3)");

                entity.HasOne(d => d.Plant)
                    .WithMany(p => p.SapsalesOrderItems)
                    .HasForeignKey(d => d.PlantId)
                    .HasConstraintName("FK_SAPSalesOrderItem_Plant");

                entity.HasOne(d => d.Sapmaterial)
                    .WithMany(p => p.SapsalesOrderItems)
                    .HasForeignKey(d => d.SapmaterialId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPSalesOrderItem_SAPMaterial");

                entity.HasOne(d => d.SapsalesOrder)
                    .WithMany(p => p.SapsalesOrderItems)
                    .HasForeignKey(d => d.SapsalesOrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPSalesOrderItem_SAPSalesOrder");

                entity.HasOne(d => d.SapshipTo)
                    .WithMany(p => p.SapsalesOrderItems)
                    .HasForeignKey(d => d.SapshipToId)
                    .HasConstraintName("FK_SAPSalesOrderItem_SAPShipTo");
            });

            modelBuilder.Entity<SapsalesOrganization>(entity =>
            {
                entity.ToTable("SAPSalesOrganization");

                entity.Property(e => e.SapsalesOrganizationId).HasColumnName("SAPSalesOrganizationID");

                entity.Property(e => e.DivisionId).HasColumnName("DivisionID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sapcode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SAPCode");

                entity.HasOne(d => d.Division)
                    .WithMany(p => p.SapsalesOrganizations)
                    .HasForeignKey(d => d.DivisionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPSalesOrganization_Division");
            });

            modelBuilder.Entity<SapscrapDelivery>(entity =>
            {
                entity.HasKey(e => e.SapdeliveryId);

                entity.ToTable("SAPScrapDelivery");

                entity.Property(e => e.SapdeliveryId)
                    .ValueGeneratedNever()
                    .HasColumnName("SAPDeliveryID");

                entity.HasOne(d => d.Sapdelivery)
                    .WithOne(p => p.SapscrapDelivery)
                    .HasForeignKey<SapscrapDelivery>(d => d.SapdeliveryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPScrapDelivery_SAPDelivery");
            });

            modelBuilder.Entity<SapscrapOrder>(entity =>
            {
                entity.HasKey(e => e.SapsalesOrderId);

                entity.ToTable("SAPScrapOrder");

                entity.Property(e => e.SapsalesOrderId)
                    .ValueGeneratedNever()
                    .HasColumnName("SAPSalesOrderID");

                entity.HasOne(d => d.SapsalesOrder)
                    .WithOne(p => p.SapscrapOrder)
                    .HasForeignKey<SapscrapOrder>(d => d.SapsalesOrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPScrapOrder_SAPSalesOrder");
            });

            modelBuilder.Entity<SapshipTo>(entity =>
            {
                entity.ToTable("SAPShipTo");

                entity.HasIndex(e => e.SapsalesGroupId, "IDX_SAPShipto_SalesGrpId");

                entity.HasIndex(e => new { e.DivisionId, e.Number }, "IX_SAPShipTo")
                    .IsUnique();

                entity.HasIndex(e => e.CityId, "ix_CityID")
                    .HasFillFactor(100);

                entity.Property(e => e.SapshipToId).HasColumnName("SAPShipToID");

                entity.Property(e => e.Address)
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.Currency)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.DivisionId).HasColumnName("DivisionID");

                entity.Property(e => e.Fax)
                    .HasMaxLength(31)
                    .IsUnicode(false);

                entity.Property(e => e.FreightIndicatorSapconditionGroupId).HasColumnName("FreightIndicatorSAPConditionGroupID");

                entity.Property(e => e.FuelSurchargeSapconditionGroupId).HasColumnName("FuelSurchargeSAPConditionGroupID");

                entity.Property(e => e.IncoTerms2)
                    .HasMaxLength(28)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.Name2)
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.Number)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.SapcustomerGroupId).HasColumnName("SAPCustomerGroupID");

                entity.Property(e => e.SapsalesGroupId).HasColumnName("SAPSalesGroupID");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.SapshipTos)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPShipTo_City");

                entity.HasOne(d => d.Division)
                    .WithMany(p => p.SapshipTos)
                    .HasForeignKey(d => d.DivisionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPShipTo_Division");

                entity.HasOne(d => d.FreightIndicatorSapconditionGroup)
                    .WithMany(p => p.SapshipToFreightIndicatorSapconditionGroups)
                    .HasForeignKey(d => d.FreightIndicatorSapconditionGroupId)
                    .HasConstraintName("FK_SAPShipTo_FreightIndicatorSAPConditionGroup");

                entity.HasOne(d => d.FuelSurchargeSapconditionGroup)
                    .WithMany(p => p.SapshipToFuelSurchargeSapconditionGroups)
                    .HasForeignKey(d => d.FuelSurchargeSapconditionGroupId)
                    .HasConstraintName("FK_SAPShipTo_FuelSurchargeSAPConditionGroup");

                entity.HasOne(d => d.SapcustomerGroup)
                    .WithMany(p => p.SapshipTos)
                    .HasForeignKey(d => d.SapcustomerGroupId)
                    .HasConstraintName("FK_SAPShipTo_SAPCustomerGroup");

                entity.HasOne(d => d.SapsalesGroup)
                    .WithMany(p => p.SapshipTos)
                    .HasForeignKey(d => d.SapsalesGroupId)
                    .HasConstraintName("FK_SAPShipTo_SAPSalesGroup");
            });

            modelBuilder.Entity<SapshipToSapsalesOrganization>(entity =>
            {
                entity.ToTable("SAPShipToSAPSalesOrganization");

                entity.HasIndex(e => new { e.SapshipToId, e.SapsalesOrganizationId }, "IX_SAPShipToSAPSalesOrganization")
                    .IsUnique()
                    .HasFillFactor(100);

                entity.Property(e => e.SapshipToSapsalesOrganizationId).HasColumnName("SAPShipToSAPSalesOrganizationID");

                entity.Property(e => e.Currency)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.IncoTerms1)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.IncoTerms2)
                    .HasMaxLength(28)
                    .IsUnicode(false);

                entity.Property(e => e.OrderBlock).HasMaxLength(10);

                entity.Property(e => e.PricingProcedure)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.SapcustomerGroupId).HasColumnName("SAPCustomerGroupID");

                entity.Property(e => e.SapcustomerServiceRepId).HasColumnName("SAPCustomerServiceRepID");

                entity.Property(e => e.SappaymentTermsId).HasColumnName("SAPPaymentTermsID");

                entity.Property(e => e.SapregionId).HasColumnName("SAPRegionID");

                entity.Property(e => e.SapsalesGroupId).HasColumnName("SAPSalesGroupID");

                entity.Property(e => e.SapsalesOrganizationId).HasColumnName("SAPSalesOrganizationID");

                entity.Property(e => e.SapshipToId).HasColumnName("SAPShipToID");

                entity.Property(e => e.SaptierId).HasColumnName("SAPTierID");

                entity.HasOne(d => d.SapcustomerGroup)
                    .WithMany(p => p.SapshipToSapsalesOrganizations)
                    .HasForeignKey(d => d.SapcustomerGroupId)
                    .HasConstraintName("FK_SAPShipToSAPSalesOrganization_SAPCustomerGroup");

                entity.HasOne(d => d.SapcustomerServiceRep)
                    .WithMany(p => p.SapshipToSapsalesOrganizations)
                    .HasForeignKey(d => d.SapcustomerServiceRepId)
                    .HasConstraintName("FK_SAPShipToSAPSalesOrganization_SAPCustomerServiceRep");

                entity.HasOne(d => d.SappaymentTerms)
                    .WithMany(p => p.SapshipToSapsalesOrganizations)
                    .HasForeignKey(d => d.SappaymentTermsId)
                    .HasConstraintName("FK_SAPShipToSAPSalesOrganization_SAPPaymentTerms");

                entity.HasOne(d => d.Sapregion)
                    .WithMany(p => p.SapshipToSapsalesOrganizations)
                    .HasForeignKey(d => d.SapregionId)
                    .HasConstraintName("FK_SAPShipToSAPSalesOrganization_SAPRegion");

                entity.HasOne(d => d.SapsalesGroup)
                    .WithMany(p => p.SapshipToSapsalesOrganizations)
                    .HasForeignKey(d => d.SapsalesGroupId)
                    .HasConstraintName("FK_SAPShipToSAPSalesOrganization_SAPSalesGroup");

                entity.HasOne(d => d.SapsalesOrganization)
                    .WithMany(p => p.SapshipToSapsalesOrganizations)
                    .HasForeignKey(d => d.SapsalesOrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPShipToSAPSalesOrganization_SAPSalesOrganization");

                entity.HasOne(d => d.SapshipTo)
                    .WithMany(p => p.SapshipToSapsalesOrganizations)
                    .HasForeignKey(d => d.SapshipToId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPShipToSAPSalesOrganization_SAPShipTo");

                entity.HasOne(d => d.Saptier)
                    .WithMany(p => p.SapshipToSapsalesOrganizations)
                    .HasForeignKey(d => d.SaptierId)
                    .HasConstraintName("FK_SAPShipToSAPSalesOrganization_SAPTier");
            });

            modelBuilder.Entity<Sapshipment>(entity =>
            {
                entity.ToTable("SAPShipment");

                entity.HasIndex(e => new { e.Number, e.DeliveryNumber, e.DivisionId }, "IX_SAPShipment")
                    .IsUnique();

                entity.HasIndex(e => e.ActualGoodsMovementDate, "IX_SAPShipment_ActGoodsMvmtDate");

                entity.Property(e => e.SapshipmentId).HasColumnName("SAPShipmentID");

                entity.Property(e => e.AccessCharges).HasColumnType("decimal(13, 2)");

                entity.Property(e => e.ActualGoodsMovementDate).HasColumnType("datetime");

                entity.Property(e => e.DeliveryMethodId).HasColumnName("DeliveryMethodID");

                entity.Property(e => e.DeliveryNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Distance).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.DistanceUom)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("DistanceUOM");

                entity.Property(e => e.DivisionId).HasColumnName("DivisionID");

                entity.Property(e => e.EstimatedCost).HasColumnType("decimal(13, 2)");

                entity.Property(e => e.Fsc)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("FSC");

                entity.Property(e => e.IpsactualWeight)
                    .HasColumnType("decimal(18, 4)")
                    .HasColumnName("IPSActualWeight");

                entity.Property(e => e.IpsadditionalCharges)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("IPSAdditionalCharges");

                entity.Property(e => e.IpscalculatedMiles)
                    .HasColumnType("decimal(10, 0)")
                    .HasColumnName("IPSCalculatedMiles");

                entity.Property(e => e.Ipsexception).HasColumnName("IPSException");

                entity.Property(e => e.Ipsfsc)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("IPSFSC");

                entity.Property(e => e.IpsgotPaidMiles).HasColumnName("IPSGotPaidMiles");

                entity.Property(e => e.IpsgotPaidWeight).HasColumnName("IPSGotPaidWeight");

                entity.Property(e => e.IpslineHaul)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("IPSLineHaul");

                entity.Property(e => e.Ipsonly).HasColumnName("IPSOnly");

                entity.Property(e => e.IpspaidDate)
                    .HasColumnType("datetime")
                    .HasColumnName("IPSPaidDate");

                entity.Property(e => e.IpstotalCost)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("IPSTotalCost");

                entity.Property(e => e.Number)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PlantId).HasColumnName("PlantID");

                entity.Property(e => e.ProductLineId).HasColumnName("ProductLineID");

                entity.Property(e => e.SapdeliveryTypeId).HasColumnName("SAPDeliveryTypeID");

                entity.Property(e => e.SapsoldToId).HasColumnName("SAPSoldToID");

                entity.Property(e => e.SapvendorId).HasColumnName("SAPVendorID");

                entity.Property(e => e.UsdfreightChargedToCustomer)
                    .HasColumnType("decimal(18, 4)")
                    .HasColumnName("USDFreightChargedToCustomer");

                entity.Property(e => e.UsdfreightPaidToCarrier)
                    .HasColumnType("decimal(18, 4)")
                    .HasColumnName("USDFreightPaidToCarrier");

                entity.Property(e => e.Weight).HasColumnType("decimal(18, 4)");

                entity.HasOne(d => d.DeliveryMethod)
                    .WithMany(p => p.Sapshipments)
                    .HasForeignKey(d => d.DeliveryMethodId)
                    .HasConstraintName("FK_SAPShipment_DeliveryMethod");

                entity.HasOne(d => d.Division)
                    .WithMany(p => p.Sapshipments)
                    .HasForeignKey(d => d.DivisionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPShipment_Division");

                entity.HasOne(d => d.Plant)
                    .WithMany(p => p.Sapshipments)
                    .HasForeignKey(d => d.PlantId)
                    .HasConstraintName("FK_SAPShipment_Plant");

                entity.HasOne(d => d.ProductLine)
                    .WithMany(p => p.Sapshipments)
                    .HasForeignKey(d => d.ProductLineId)
                    .HasConstraintName("FK_SAPShipment_ProductLine");

                entity.HasOne(d => d.SapdeliveryType)
                    .WithMany(p => p.Sapshipments)
                    .HasForeignKey(d => d.SapdeliveryTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPShipment_SAPDeliveryType");

                entity.HasOne(d => d.SapsoldTo)
                    .WithMany(p => p.Sapshipments)
                    .HasForeignKey(d => d.SapsoldToId)
                    .HasConstraintName("FK_SAPShipment_SAPSoldTo");

                entity.HasOne(d => d.Sapvendor)
                    .WithMany(p => p.Sapshipments)
                    .HasForeignKey(d => d.SapvendorId)
                    .HasConstraintName("FK_SAPShipment_SAPVendor");
            });

            modelBuilder.Entity<SapsoldTo>(entity =>
            {
                entity.HasKey(e => e.SapshipToId);

                entity.ToTable("SAPSoldTo");

                entity.Property(e => e.SapshipToId)
                    .ValueGeneratedNever()
                    .HasColumnName("SAPShipToID");

                entity.Property(e => e.DefaultSapshipToId).HasColumnName("DefaultSAPShipToID");

                entity.Property(e => e.HomeMillSapconditionGroupId).HasColumnName("HomeMillSAPConditionGroupID");

                entity.Property(e => e.LastBacklogRefresh).HasColumnType("datetime");

                entity.Property(e => e.Oem).HasColumnName("OEM");

                entity.Property(e => e.PricingNotes)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.PricingProcedure)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.RegionSapconditionGroupId).HasColumnName("RegionSAPConditionGroupID");

                entity.Property(e => e.TierSapconditionGroupId).HasColumnName("TierSAPConditionGroupID");

                entity.HasOne(d => d.DefaultSapshipTo)
                    .WithMany(p => p.SapsoldToDefaultSapshipTos)
                    .HasForeignKey(d => d.DefaultSapshipToId)
                    .HasConstraintName("FK_SAPSoldTo_DefaultSAPShipTo");

                entity.HasOne(d => d.HomeMillSapconditionGroup)
                    .WithMany(p => p.SapsoldToHomeMillSapconditionGroups)
                    .HasForeignKey(d => d.HomeMillSapconditionGroupId)
                    .HasConstraintName("FK_SAPSoldTo_HomeMillSAPConditionGroup");

                entity.HasOne(d => d.RegionSapconditionGroup)
                    .WithMany(p => p.SapsoldToRegionSapconditionGroups)
                    .HasForeignKey(d => d.RegionSapconditionGroupId)
                    .HasConstraintName("FK_SAPSoldTo_RegionSAPConditionGroup");

                entity.HasOne(d => d.SapshipTo)
                    .WithOne(p => p.SapsoldToSapshipTo)
                    .HasForeignKey<SapsoldTo>(d => d.SapshipToId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPSoldTo_SAPShipTo");

                entity.HasOne(d => d.TierSapconditionGroup)
                    .WithMany(p => p.SapsoldToTierSapconditionGroups)
                    .HasForeignKey(d => d.TierSapconditionGroupId)
                    .HasConstraintName("FK_SAPSoldTo_TierSAPConditionGroup");
            });

            modelBuilder.Entity<SapsoldToPlantExclusion>(entity =>
            {
                entity.HasKey(e => new { e.SapshipToId, e.PlantId });

                entity.ToTable("SAPSoldToPlantExclusion");

                entity.Property(e => e.SapshipToId).HasColumnName("SAPShipToID");

                entity.Property(e => e.PlantId).HasColumnName("PlantID");

                entity.Property(e => e.CanOrder).HasDefaultValueSql("((1))");

                entity.Property(e => e.ViewStockRollings)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Plant)
                    .WithMany(p => p.SapsoldToPlantExclusions)
                    .HasForeignKey(d => d.PlantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPSoldToPlantExclusion_Plant");

                entity.HasOne(d => d.SapshipTo)
                    .WithMany(p => p.SapsoldToPlantExclusions)
                    .HasForeignKey(d => d.SapshipToId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPSoldToPlantExclusion_SAPSoldTo");
            });

            modelBuilder.Entity<SapsoldToSapshipTo>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("SAPSoldToSAPShipTo");

                entity.HasIndex(e => new { e.SapsoldToId, e.SapshipToId }, "IX_SAPSoldToSAPShipTo")
                    .IsUnique()
                    .HasFillFactor(100);

                entity.Property(e => e.SapshipToId).HasColumnName("SAPShipToID");

                entity.Property(e => e.SapsoldToId).HasColumnName("SAPSoldToID");

                entity.HasOne(d => d.SapshipTo)
                    .WithMany()
                    .HasForeignKey(d => d.SapshipToId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPSoldToSAPShipTo_SAPShipTo");

                entity.HasOne(d => d.SapsoldTo)
                    .WithMany()
                    .HasForeignKey(d => d.SapsoldToId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPSoldToSAPShipTo_SAPSoldTo");
            });

            modelBuilder.Entity<Sapstock>(entity =>
            {
                entity.ToTable("SAPStock");

                entity.Property(e => e.SapstockId).HasColumnName("SAPStockID");

                entity.Property(e => e.BatchDate).HasColumnType("datetime");

                entity.Property(e => e.BatchNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CloseRollingWeight).HasColumnType("decimal(17, 3)");

                entity.Property(e => e.Grade)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PlantId).HasColumnName("PlantID");

                entity.Property(e => e.RollingEndDate).HasColumnType("date");

                entity.Property(e => e.Sapcode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SAPCode");

                entity.Property(e => e.SapmaterialId).HasColumnName("SAPMaterialID");

                entity.Property(e => e.SapsalesOrderItemId).HasColumnName("SAPSalesOrderItemID");

                entity.Property(e => e.SapsalesOrderItemNumber).HasColumnName("SAPSalesOrderItemNumber");

                entity.Property(e => e.SapsalesOrderNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("SAPSalesOrderNumber")
                    .IsFixedLength();

                entity.Property(e => e.SapspecificationId).HasColumnName("SAPSpecificationID");

                entity.Property(e => e.SaptubeStandardId).HasColumnName("SAPTubeStandardID");

                entity.Property(e => e.TubeLength).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Uom)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("UOM");

                entity.Property(e => e.Weight).HasColumnType("decimal(17, 3)");

                entity.HasOne(d => d.Plant)
                    .WithMany(p => p.Sapstocks)
                    .HasForeignKey(d => d.PlantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPStock_Plant");

                entity.HasOne(d => d.Sapmaterial)
                    .WithMany(p => p.Sapstocks)
                    .HasForeignKey(d => d.SapmaterialId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPStock_SAPMaterial");

                entity.HasOne(d => d.SapsalesOrderItem)
                    .WithMany(p => p.Sapstocks)
                    .HasForeignKey(d => d.SapsalesOrderItemId)
                    .HasConstraintName("FK_SAPStock_SAPSalesOrderItem");

                entity.HasOne(d => d.Sapspecification)
                    .WithMany(p => p.SapstockSapspecifications)
                    .HasForeignKey(d => d.SapspecificationId)
                    .HasConstraintName("FK_SAPStock_SAPSpecification");

                entity.HasOne(d => d.SaptubeStandard)
                    .WithMany(p => p.SapstockSaptubeStandards)
                    .HasForeignKey(d => d.SaptubeStandardId)
                    .HasConstraintName("FK_SAPStock_SAPTubeStandard");
            });

            modelBuilder.Entity<SapstorageLocation>(entity =>
            {
                entity.ToTable("SAPStorageLocation");

                entity.Property(e => e.SapstorageLocationId).HasColumnName("SAPStorageLocationID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PlantId).HasColumnName("PlantID");

                entity.Property(e => e.Sapcode)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("SAPCode");

                entity.HasOne(d => d.Plant)
                    .WithMany(p => p.SapstorageLocations)
                    .HasForeignKey(d => d.PlantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPStorageLocation_Plant");
            });

            modelBuilder.Entity<Saptier>(entity =>
            {
                entity.ToTable("SAPTier");

                entity.Property(e => e.SaptierId).HasColumnName("SAPTierID");

                entity.Property(e => e.DivisionId).HasColumnName("DivisionID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sapcode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SAPCode");

                entity.HasOne(d => d.Division)
                    .WithMany(p => p.Saptiers)
                    .HasForeignKey(d => d.DivisionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPTier_Division");
            });

            modelBuilder.Entity<SaptierSapsalesOrganization>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("SAPTierSAPSalesOrganization");

                entity.Property(e => e.SapsalesOrganizationId).HasColumnName("SAPSalesOrganizationID");

                entity.Property(e => e.SaptierId).HasColumnName("SAPTierID");

                entity.HasOne(d => d.SapsalesOrganization)
                    .WithMany()
                    .HasForeignKey(d => d.SapsalesOrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPTierSAPSalesOrganization_SAPSalesOrganization");

                entity.HasOne(d => d.Saptier)
                    .WithMany()
                    .HasForeignKey(d => d.SaptierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPTierSAPSalesOrganization_SAPTier");
            });

            modelBuilder.Entity<SapunitOfMeasure>(entity =>
            {
                entity.ToTable("SAPUnitOfMeasure");

                entity.Property(e => e.SapunitOfMeasureId).HasColumnName("SAPUnitOfMeasureID");

                entity.Property(e => e.Sapcode)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("SAPCode");
            });

            modelBuilder.Entity<Sapvendor>(entity =>
            {
                entity.ToTable("SAPVendor");

                entity.HasIndex(e => new { e.DivisionId, e.Number }, "IX_SAPVendor")
                    .IsUnique();

                entity.Property(e => e.SapvendorId).HasColumnName("SAPVendorID");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Address)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.DivisionId).HasColumnName("DivisionID");

                entity.Property(e => e.Fax)
                    .HasMaxLength(31)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Number)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Sapvendors)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPVendor_City");

                entity.HasOne(d => d.Division)
                    .WithMany(p => p.Sapvendors)
                    .HasForeignKey(d => d.DivisionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPVendor_Division");
            });

            modelBuilder.Entity<SapwheatlandDelivery>(entity =>
            {
                entity.HasKey(e => e.DeliveryId);

                entity.ToTable("SAPWheatlandDelivery");

                entity.HasIndex(e => e.SoldToNumber, "IX_SAPWheatlandDelivery");

                entity.Property(e => e.DeliveryId).HasColumnName("DeliveryID");

                entity.Property(e => e.BatchNumber).HasMaxLength(50);

                entity.Property(e => e.CustomerPo)
                    .HasMaxLength(50)
                    .HasColumnName("CustomerPO");

                entity.Property(e => e.DeliveryNumber).HasMaxLength(50);

                entity.Property(e => e.HeatNumber).HasMaxLength(50);

                entity.Property(e => e.ItemDesc).HasMaxLength(100);

                entity.Property(e => e.MaterialNumber).HasMaxLength(50);

                entity.Property(e => e.Pgidate)
                    .HasColumnType("date")
                    .HasColumnName("PGIDate");

                entity.Property(e => e.RunNumber).HasMaxLength(50);

                entity.Property(e => e.SalesOrderNumber).HasMaxLength(50);

                entity.Property(e => e.ShipToNumber).HasMaxLength(50);

                entity.Property(e => e.SoldToNumber).HasMaxLength(20);
            });

            modelBuilder.Entity<ScrapSale>(entity =>
            {
                entity.ToTable("ScrapSale");

                entity.Property(e => e.ScrapSaleId).HasColumnName("ScrapSaleID");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DivisionId).HasColumnName("DivisionID");

                entity.Property(e => e.GrossWeight).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PlantId).HasColumnName("PlantID");

                entity.Property(e => e.RandomLengthSapsoldToId).HasColumnName("RandomLengthSAPSoldToID");

                entity.Property(e => e.SapscrapDeliveryId).HasColumnName("SAPScrapDeliveryID");

                entity.Property(e => e.ScrapSapsoldToId).HasColumnName("ScrapSAPSoldToID");

                entity.Property(e => e.TareWeight).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TrailerId).HasColumnName("TrailerID");

                entity.Property(e => e.TruckId).HasColumnName("TruckID");

                entity.Property(e => e.WeightAddedToInventory).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.WeightAddedToSapsalesOrder).HasColumnName("WeightAddedToSAPSalesOrder");

                entity.HasOne(d => d.Division)
                    .WithMany(p => p.ScrapSales)
                    .HasForeignKey(d => d.DivisionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScrapSale_Division");

                entity.HasOne(d => d.Plant)
                    .WithMany(p => p.ScrapSales)
                    .HasForeignKey(d => d.PlantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScrapSale_Plant");

                entity.HasOne(d => d.RandomLengthSapsoldTo)
                    .WithMany(p => p.ScrapSales)
                    .HasForeignKey(d => d.RandomLengthSapsoldToId)
                    .HasConstraintName("FK_ScrapSale_RandomLengthSAPSoldTo");

                entity.HasOne(d => d.SapscrapDelivery)
                    .WithMany(p => p.ScrapSales)
                    .HasForeignKey(d => d.SapscrapDeliveryId)
                    .HasConstraintName("FK_ScrapSale_SAPScrapDelivery");

                entity.HasOne(d => d.ScrapSapsoldTo)
                    .WithMany(p => p.ScrapSales)
                    .HasForeignKey(d => d.ScrapSapsoldToId)
                    .HasConstraintName("FK_ScrapSale_ScrapSAPSoldTo");

                entity.HasOne(d => d.Trailer)
                    .WithMany(p => p.ScrapSales)
                    .HasForeignKey(d => d.TrailerId)
                    .HasConstraintName("FK_ScrapSale_Trailer");

                entity.HasOne(d => d.Truck)
                    .WithMany(p => p.ScrapSales)
                    .HasForeignKey(d => d.TruckId)
                    .HasConstraintName("FK_ScrapSale_Truck");
            });

            modelBuilder.Entity<ScrapSaleBox>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ScrapSaleBox");

                entity.Property(e => e.BoxId).HasColumnName("BoxID");

                entity.Property(e => e.ScrapSaleId).HasColumnName("ScrapSaleID");

                entity.HasOne(d => d.Box)
                    .WithMany()
                    .HasForeignKey(d => d.BoxId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScrapSaleBox_Box");

                entity.HasOne(d => d.ScrapSale)
                    .WithMany()
                    .HasForeignKey(d => d.ScrapSaleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScrapSaleBox_ScrapSale");
            });

            modelBuilder.Entity<ScrapSaleSapsalesOrderItem>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ScrapSaleSAPSalesOrderItem");

                entity.Property(e => e.SapsalesOrderItemId).HasColumnName("SAPSalesOrderItemID");

                entity.Property(e => e.ScrapSaleId).HasColumnName("ScrapSaleID");

                entity.HasOne(d => d.SapsalesOrderItem)
                    .WithMany()
                    .HasForeignKey(d => d.SapsalesOrderItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScrapRandomLengthSaleSAPSalesOrderItem_SAPSalesOrderItem");

                entity.HasOne(d => d.ScrapSale)
                    .WithMany()
                    .HasForeignKey(d => d.ScrapSaleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScrapSaleSAPSalesOrderItem_ScrapSale");
            });

            modelBuilder.Entity<ScrapSapsoldTo>(entity =>
            {
                entity.ToTable("ScrapSAPSoldTo");

                entity.HasIndex(e => new { e.SapsoldToId, e.LocationId }, "IX_ScrapSAPSoldTo")
                    .IsUnique();

                entity.Property(e => e.ScrapSapsoldToId).HasColumnName("ScrapSAPSoldToID");

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.SapsoldToId).HasColumnName("SAPSoldToID");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.ScrapSapsoldTos)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScrapSAPSoldTo_Location");

                entity.HasOne(d => d.SapsoldTo)
                    .WithMany(p => p.ScrapSapsoldTos)
                    .HasForeignKey(d => d.SapsoldToId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScrapSAPSoldTo_SAPSoldTo");
            });

            modelBuilder.Entity<ShipmentCost>(entity =>
            {
                entity.ToTable("ShipmentCost");

                entity.Property(e => e.ShipmentCostId).HasColumnName("ShipmentCostID");

                entity.Property(e => e.Amount).HasColumnType("decimal(13, 2)");

                entity.Property(e => e.ShipmentCostTypeId).HasColumnName("ShipmentCostTypeID");

                entity.Property(e => e.ShipmentId).HasColumnName("ShipmentID");

                entity.HasOne(d => d.ShipmentCostType)
                    .WithMany(p => p.ShipmentCosts)
                    .HasForeignKey(d => d.ShipmentCostTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShipmentCost_ShipmentCostType");

                entity.HasOne(d => d.Shipment)
                    .WithMany(p => p.ShipmentCosts)
                    .HasForeignKey(d => d.ShipmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShipmentCost_SAPShipment");
            });

            modelBuilder.Entity<ShipmentCostType>(entity =>
            {
                entity.ToTable("ShipmentCostType");

                entity.Property(e => e.ShipmentCostTypeId).HasColumnName("ShipmentCostTypeID");

                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Ips).HasColumnName("IPS");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sap).HasColumnName("SAP");
            });

            modelBuilder.Entity<ShippingCart>(entity =>
            {
                entity.ToTable("ShippingCart");

                entity.Property(e => e.ShippingCartId).HasColumnName("ShippingCartID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Plant).HasMaxLength(50);

                entity.Property(e => e.ShipTo).HasMaxLength(50);

                entity.Property(e => e.SoldTo).HasMaxLength(50);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ShippingCarts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShippingCart_User");
            });

            modelBuilder.Entity<ShippingCartSapsalesOrderItem>(entity =>
            {
                entity.ToTable("ShippingCartSAPSalesOrderItem");

                entity.Property(e => e.ShippingCartSapsalesOrderItemId).HasColumnName("ShippingCartSAPSalesOrderItemID");

                entity.Property(e => e.CartQuantity).HasColumnType("decimal(18, 3)");

                entity.Property(e => e.SapsalesOrderItemId).HasColumnName("SAPSalesOrderItemID");

                entity.Property(e => e.ShippingCartId).HasColumnName("ShippingCartID");

                entity.HasOne(d => d.SapsalesOrderItem)
                    .WithMany(p => p.ShippingCartSapsalesOrderItems)
                    .HasForeignKey(d => d.SapsalesOrderItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShippingCartSAPSalesOrderItem_SAPSalesOrderItem");

                entity.HasOne(d => d.ShippingCart)
                    .WithMany(p => p.ShippingCartSapsalesOrderItems)
                    .HasForeignKey(d => d.ShippingCartId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShippingCartSAPSalesOrderItem_ShippingCart");
            });

            modelBuilder.Entity<ShoppingCart>(entity =>
            {
                entity.HasKey(e => e.ShoppingCardId);

                entity.ToTable("ShoppingCart");

                entity.Property(e => e.ShoppingCardId).HasColumnName("ShoppingCardID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ShoppingCarts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShoppingCart_User");
            });

            modelBuilder.Entity<ShoppingCartSaprolling>(entity =>
            {
                entity.ToTable("ShoppingCartSAPRolling");

                entity.Property(e => e.ShoppingCartSaprollingId).HasColumnName("ShoppingCartSAPRollingID");

                entity.Property(e => e.AvailableQuantity).HasColumnType("decimal(18, 3)");

                entity.Property(e => e.CartQuantity).HasColumnType("decimal(18, 3)");

                entity.Property(e => e.Note)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PlantId).HasColumnName("PlantID");

                entity.Property(e => e.RecordSource)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.RequestedDate).HasColumnType("date");

                entity.Property(e => e.RollDate).HasColumnType("date");

                entity.Property(e => e.SalesInstructionId)
                    .HasMaxLength(50)
                    .HasColumnName("SalesInstructionID");

                entity.Property(e => e.SapmaterialId).HasColumnName("SAPMaterialID");

                entity.Property(e => e.SaprollingId).HasColumnName("SAPRollingID");

                entity.Property(e => e.SaptubeStandardId).HasColumnName("SAPTubeStandardID");

                entity.Property(e => e.ShoppingCartId).HasColumnName("ShoppingCartID");

                entity.Property(e => e.SpecificationId)
                    .HasMaxLength(50)
                    .HasColumnName("SpecificationID");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Plant)
                    .WithMany(p => p.ShoppingCartSaprollings)
                    .HasForeignKey(d => d.PlantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShoppingCartSAPRolling_Plant");

                entity.HasOne(d => d.Sapmaterial)
                    .WithMany(p => p.ShoppingCartSaprollings)
                    .HasForeignKey(d => d.SapmaterialId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShoppingCartSAPRolling_SAPMaterial");

                entity.HasOne(d => d.SaptubeStandard)
                    .WithMany(p => p.ShoppingCartSaprollings)
                    .HasForeignKey(d => d.SaptubeStandardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShoppingCartSAPRolling_SAPTubeStandard");

                entity.HasOne(d => d.ShoppingCart)
                    .WithMany(p => p.ShoppingCartSaprollings)
                    .HasForeignKey(d => d.ShoppingCartId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShoppingCartSAPRolling_ShoppingCart");
            });

            modelBuilder.Entity<ShoppingCartSapstock>(entity =>
            {
                entity.ToTable("ShoppingCartSAPStock");

                entity.Property(e => e.ShoppingCartSapstockId).HasColumnName("ShoppingCartSAPStockID");

                entity.Property(e => e.CartQuantity).HasColumnType("decimal(18, 3)");

                entity.Property(e => e.Note)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.RecordSource)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.RollDate)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.SapstockId).HasColumnName("SAPStockID");

                entity.Property(e => e.ShoppingCartId).HasColumnName("ShoppingCartID");

                entity.Property(e => e.Status)
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.HasOne(d => d.Sapstock)
                    .WithMany(p => p.ShoppingCartSapstocks)
                    .HasForeignKey(d => d.SapstockId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShoppingCartSAPStock_SAPStock");

                entity.HasOne(d => d.ShoppingCart)
                    .WithMany(p => p.ShoppingCartSapstocks)
                    .HasForeignKey(d => d.ShoppingCartId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShoppingCartSAPStock_ShoppingCart");
            });

            modelBuilder.Entity<ShoppingCartWheatland>(entity =>
            {
                entity.ToTable("ShoppingCartWheatland");

                entity.Property(e => e.ShoppingCartWheatlandId).HasColumnName("ShoppingCartWheatlandID");

                entity.Property(e => e.CartQuantity).HasColumnType("decimal(18, 3)");

                entity.Property(e => e.PlantId).HasColumnName("PlantID");

                entity.Property(e => e.RequestedDate).HasColumnType("date");

                entity.Property(e => e.RollingDate).HasColumnType("date");

                entity.Property(e => e.SapMaterialId).HasColumnName("SapMaterialID");

                entity.Property(e => e.ShoppingCartId).HasColumnName("ShoppingCartID");

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Type)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<SoldToPriceSheetDefaultOption>(entity =>
            {
                entity.HasKey(e => e.SapsoldToId);

                entity.ToTable("SoldToPriceSheetDefaultOption");

                entity.Property(e => e.SapsoldToId)
                    .ValueGeneratedNever()
                    .HasColumnName("SAPSoldToID");

                entity.Property(e => e.DefaultShiptos).HasMaxLength(255);

                entity.Property(e => e.DefaultUsers).HasMaxLength(255);

                entity.Property(e => e.FlgsoldToPrice).HasColumnName("FLGSoldToPrice");

                entity.Property(e => e.OtherEmailId)
                    .HasMaxLength(500)
                    .HasColumnName("OtherEmailID");

                entity.Property(e => e.PriceSheetType).HasMaxLength(50);

                entity.Property(e => e.PriceSheetView).HasMaxLength(50);
            });

            modelBuilder.Entity<Square>(entity =>
            {
                entity.ToTable("Square");

                entity.Property(e => e.SquareId).HasColumnName("SquareID");

                entity.Property(e => e.MaterialGroup)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Size).HasColumnType("decimal(9, 4)");
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.ToTable("State");

                entity.HasIndex(e => new { e.CountryId, e.Abbreviation }, "IX_State")
                    .IsUnique();

                entity.Property(e => e.StateId).HasColumnName("StateID");

                entity.Property(e => e.Abbreviation)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.States)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_State_Country");
            });

            modelBuilder.Entity<Stock>(entity =>
            {
                entity.ToTable("Stock");

                entity.Property(e => e.StockId).HasColumnName("StockID");

                entity.Property(e => e.Grade)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PlantId).HasColumnName("PlantID");

                entity.Property(e => e.QuoteMaterialId).HasColumnName("QuoteMaterialID");

                entity.Property(e => e.Sapcode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SAPCode");

                entity.Property(e => e.SapsalesOrderItemId).HasColumnName("SAPSalesOrderItemID");

                entity.Property(e => e.TubeLength).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Uom)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("UOM");

                entity.Property(e => e.Weight).HasColumnType("decimal(17, 3)");

                entity.HasOne(d => d.Plant)
                    .WithMany(p => p.Stocks)
                    .HasForeignKey(d => d.PlantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Stock_Plant");

                entity.HasOne(d => d.QuoteMaterial)
                    .WithMany(p => p.Stocks)
                    .HasForeignKey(d => d.QuoteMaterialId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Stock_QuoteMaterial");

                entity.HasOne(d => d.SapsalesOrderItem)
                    .WithMany(p => p.Stocks)
                    .HasForeignKey(d => d.SapsalesOrderItemId)
                    .HasConstraintName("FK_Stock_SAPSalesOrderItem");
            });

            modelBuilder.Entity<StockingList>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("StockingList");

                entity.Property(e => e.SapmaterialId).HasColumnName("SAPMaterialID");

                entity.HasOne(d => d.Sapmaterial)
                    .WithMany()
                    .HasForeignKey(d => d.SapmaterialId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StockingList_SAPMaterial");
            });

            modelBuilder.Entity<Trailer>(entity =>
            {
                entity.ToTable("Trailer");

                entity.Property(e => e.TrailerId).HasColumnName("TrailerID");

                entity.Property(e => e.ChagedDate).HasColumnType("datetime");

                entity.Property(e => e.ChangedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PreviousWeight)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("previousWeight");

                entity.Property(e => e.RandomLengthSapsoldToId).HasColumnName("RandomLengthSAPSoldToID");

                entity.Property(e => e.SapvendorId).HasColumnName("SAPVendorID");

                entity.Property(e => e.ScrapSapsoldToId).HasColumnName("ScrapSAPSoldToID");

                entity.Property(e => e.Weight).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.RandomLengthSapsoldTo)
                    .WithMany(p => p.Trailers)
                    .HasForeignKey(d => d.RandomLengthSapsoldToId)
                    .HasConstraintName("FK_Trailer_RandomLengthSAPSoldTo");

                entity.HasOne(d => d.Sapvendor)
                    .WithMany(p => p.Trailers)
                    .HasForeignKey(d => d.SapvendorId)
                    .HasConstraintName("FK_Trailer_SAPVendor");

                entity.HasOne(d => d.ScrapSapsoldTo)
                    .WithMany(p => p.Trailers)
                    .HasForeignKey(d => d.ScrapSapsoldToId)
                    .HasConstraintName("FK_Trailer_ScrapSAPSoldTo");
            });

            modelBuilder.Entity<Truck>(entity =>
            {
                entity.ToTable("Truck");

                entity.Property(e => e.TruckId).HasColumnName("TruckID");

                entity.Property(e => e.ChagedDate).HasColumnType("datetime");

                entity.Property(e => e.ChangedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PreviousWeight)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("previousWeight");

                entity.Property(e => e.RandomLengthSapsoldToId).HasColumnName("RandomLengthSAPSoldToID");

                entity.Property(e => e.SapvendorId).HasColumnName("SAPVendorID");

                entity.Property(e => e.ScrapSapsoldToId).HasColumnName("ScrapSAPSoldToID");

                entity.Property(e => e.Weight).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.RandomLengthSapsoldTo)
                    .WithMany(p => p.Trucks)
                    .HasForeignKey(d => d.RandomLengthSapsoldToId)
                    .HasConstraintName("FK_Truck_RandomLengthSAPSoldTo");

                entity.HasOne(d => d.Sapvendor)
                    .WithMany(p => p.Trucks)
                    .HasForeignKey(d => d.SapvendorId)
                    .HasConstraintName("FK_Truck_SAPVendor");

                entity.HasOne(d => d.ScrapSapsoldTo)
                    .WithMany(p => p.Trucks)
                    .HasForeignKey(d => d.ScrapSapsoldToId)
                    .HasConstraintName("FK_Truck_ScrapSAPSoldTo");
            });

            modelBuilder.Entity<Tzcountry>(entity =>
            {
                entity.HasKey(e => e.CountryCode);

                entity.ToTable("TZCountry");

                entity.Property(e => e.CountryCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("country_code")
                    .IsFixedLength();

                entity.Property(e => e.CountryName)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("country_name");
            });

            modelBuilder.Entity<Tztimezone>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TZTimezone");

                entity.Property(e => e.Abbreviation)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("abbreviation");

                entity.Property(e => e.Dst)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("dst")
                    .IsFixedLength();

                entity.Property(e => e.GmtOffset).HasColumnName("gmt_offset");

                entity.Property(e => e.TimeStart)
                    .HasColumnType("decimal(11, 0)")
                    .HasColumnName("time_start");

                entity.Property(e => e.ZoneId).HasColumnName("zone_id");

                entity.HasOne(d => d.Zone)
                    .WithMany()
                    .HasForeignKey(d => d.ZoneId)
                    .HasConstraintName("FK_TZTimezone_TZZone");
            });

            modelBuilder.Entity<Tzzone>(entity =>
            {
                entity.HasKey(e => e.ZoneId)
                    .HasName("PK_zone");

                entity.ToTable("TZZone");

                entity.Property(e => e.ZoneId)
                    .ValueGeneratedNever()
                    .HasColumnName("zone_id");

                entity.Property(e => e.CountryCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("country_code")
                    .IsFixedLength();

                entity.Property(e => e.ZoneName)
                    .HasMaxLength(35)
                    .IsUnicode(false)
                    .HasColumnName("zone_name");

                entity.HasOne(d => d.CountryCodeNavigation)
                    .WithMany(p => p.Tzzones)
                    .HasForeignKey(d => d.CountryCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TZZone_TZCountry");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasIndex(e => e.Email, "IX_User");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Extension)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FaxNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastLoginDate).HasColumnType("datetime");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordSalt)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PrimarySapsoldToId).HasColumnName("PrimarySAPSoldToID");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.PrimarySapsoldTo)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.PrimarySapsoldToId)
                    .HasConstraintName("FK_User_SAPSoldTo");

                entity.HasMany(d => d.SapshipTos)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "UserSapshipToExclusion",
                        l => l.HasOne<SapshipTo>().WithMany().HasForeignKey("SapshipToId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_UserSAPShipToExclusion_SAPShipTo"),
                        r => r.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_UserSAPShipToExclusion_User"),
                        j =>
                        {
                            j.HasKey("UserId", "SapshipToId");

                            j.ToTable("UserSAPShipToExclusion");

                            j.IndexerProperty<long>("UserId").HasColumnName("UserID");

                            j.IndexerProperty<long>("SapshipToId").HasColumnName("SAPShipToID");
                        });
            });

            modelBuilder.Entity<UserApplicationRole>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("UserApplicationRole");

                entity.HasIndex(e => e.UserId, "IX_UserApplicationRole")
                    .IsClustered();

                entity.HasIndex(e => new { e.UserId, e.ApplicationRoleId }, "IX_UserApplicationRole_1")
                    .IsUnique();

                entity.Property(e => e.ApplicationRoleId).HasColumnName("ApplicationRoleID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.ApplicationRole)
                    .WithMany()
                    .HasForeignKey(d => d.ApplicationRoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserApplicationRole_ApplicationRole");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserApplicationRole_User");
            });

            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.ToTable("UserProfile");

                entity.Property(e => e.UserProfileId).HasColumnName("UserProfileID");

                entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");

                entity.Property(e => e.SettingName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.UserProfiles)
                    .HasForeignKey(d => d.ApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserProfile_Application");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserProfiles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserProfile_User");
            });

            modelBuilder.Entity<UserSapsoldTo>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("UserSAPSoldTo");

                entity.HasIndex(e => new { e.UserId, e.SapsoldToId }, "IX_UserSAPSoldTo")
                    .IsUnique();

                entity.Property(e => e.SapsoldToId).HasColumnName("SAPSoldToID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.SapsoldTo)
                    .WithMany()
                    .HasForeignKey(d => d.SapsoldToId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserSAPSoldTo_SAPSoldTo");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserSAPSoldTo_User");
            });

            modelBuilder.Entity<WebRelasePlantSapsalesOrderItemSapdeliveryItem>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("WebRelasePlantSAPSalesOrderItemSAPDeliveryItem");

                entity.HasIndex(e => e.SapdeliveryItemId, "missing_index_12_11_WebRelasePlantSAPSalesOrderItemS");

                entity.Property(e => e.SapdeliveryItemId).HasColumnName("SAPDeliveryItemID");

                entity.Property(e => e.WebReleasePlantSapsalesOrderItemId).HasColumnName("WebReleasePlantSAPSalesOrderItemID");

                entity.HasOne(d => d.SapdeliveryItem)
                    .WithMany()
                    .HasForeignKey(d => d.SapdeliveryItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WebRelasePlantSAPSalesOrderItemSAPDeliveryItem_SAPDeliveryItem");

                entity.HasOne(d => d.WebReleasePlantSapsalesOrderItem)
                    .WithMany()
                    .HasForeignKey(d => d.WebReleasePlantSapsalesOrderItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WebRelasePlantSAPSalesOrderItemSAPDeliveryItem_WebReleasePlantSAPSalesOrderItem");
            });

            modelBuilder.Entity<WebRelease>(entity =>
            {
                entity.ToTable("WebRelease");

                entity.Property(e => e.WebReleaseId).HasColumnName("WebReleaseID");

                entity.Property(e => e.CustReleaseNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.WebReleases)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WebRelease_User");
            });

            modelBuilder.Entity<WebReleasePlant>(entity =>
            {
                entity.ToTable("WebReleasePlant");

                entity.Property(e => e.WebReleasePlantId).HasColumnName("WebReleasePlantID");

                entity.Property(e => e.Csrcomments)
                    .HasMaxLength(4000)
                    .IsUnicode(false)
                    .HasColumnName("CSRComments");

                entity.Property(e => e.CustomerComments)
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.DepartureDate).HasColumnType("date");

                entity.Property(e => e.EtOutputMessages)
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.ExpectedDate).HasColumnType("date");

                entity.Property(e => e.ExpectedTime)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.OnOrBefore)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.PlantId).HasColumnName("PlantID");

                entity.Property(e => e.Stoponumber)
                    .HasMaxLength(35)
                    .IsUnicode(false)
                    .HasColumnName("STOPONumber");

                entity.Property(e => e.Stosuccess).HasColumnName("STOSuccess");

                entity.Property(e => e.WebReleaseId).HasColumnName("WebReleaseID");

                entity.HasOne(d => d.Plant)
                    .WithMany(p => p.WebReleasePlants)
                    .HasForeignKey(d => d.PlantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WebReleasePlant_Plant");

                entity.HasOne(d => d.WebRelease)
                    .WithMany(p => p.WebReleasePlants)
                    .HasForeignKey(d => d.WebReleaseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WebReleasePlant_WebRelease");
            });

            modelBuilder.Entity<WebReleasePlantSapsalesOrderItem>(entity =>
            {
                entity.ToTable("WebReleasePlantSAPSalesOrderItem");

                entity.HasIndex(e => new { e.WebReleasePlantId, e.SapsalesOrderItemId }, "IX_WebReleasePlantSAPSalesOrderItem")
                    .IsUnique();

                entity.HasIndex(e => e.SapsalesOrderItemId, "missing_index_24_23_WebReleasePlantSAPSalesOrderItem");

                entity.Property(e => e.WebReleasePlantSapsalesOrderItemId).HasColumnName("WebReleasePlantSAPSalesOrderItemID");

                entity.Property(e => e.Quantity).HasColumnType("decimal(18, 3)");

                entity.Property(e => e.SapsalesOrderItemId).HasColumnName("SAPSalesOrderItemID");

                entity.Property(e => e.WebReleasePlantId).HasColumnName("WebReleasePlantID");

                entity.Property(e => e.Weight).HasColumnType("decimal(18, 3)");

                entity.HasOne(d => d.SapsalesOrderItem)
                    .WithMany(p => p.WebReleasePlantSapsalesOrderItems)
                    .HasForeignKey(d => d.SapsalesOrderItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WebReleasePlantSAPSalesOrderItem_SAPSalesOrderItem");

                entity.HasOne(d => d.WebReleasePlant)
                    .WithMany(p => p.WebReleasePlantSapsalesOrderItems)
                    .HasForeignKey(d => d.WebReleasePlantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WebReleasePlantSAPSalesOrderItem_WebReleasePlant");
            });

            modelBuilder.Entity<WtcpipeBnb>(entity =>
            {
                entity.HasKey(e => e.MaterialId)
                    .HasName("PK__WTCPipe___3A09B0FDF791C4C3");

                entity.ToTable("WTCPipe_BNB");

                entity.Property(e => e.MaterialId).HasColumnName("Material_ID");

                entity.Property(e => e.MaterialNumber)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("Material_Number");
            });

            modelBuilder.Entity<WtcpipeSize>(entity =>
            {
                entity.HasKey(e => e.RoundId);

                entity.ToTable("WTCPipeSize");

                entity.Property(e => e.RoundId).HasColumnName("RoundID");

                entity.Property(e => e.ProductSize)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Size)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Zep1>(entity =>
            {
                entity.ToTable("ZEP1");

                entity.Property(e => e.Zep1id).HasColumnName("ZEP1ID");

                entity.Property(e => e.Currency)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Rate).HasColumnType("decimal(13, 2)");

                entity.Property(e => e.SappricingGroupId).HasColumnName("SAPPricingGroupID");

                entity.Property(e => e.Unit)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ValidFrom).HasColumnType("date");

                entity.Property(e => e.ValidTo).HasColumnType("date");

                entity.HasOne(d => d.SappricingGroup)
                    .WithMany(p => p.Zep1s)
                    .HasForeignKey(d => d.SappricingGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ZEP1_SAPCharacteristicOption");
            });

            modelBuilder.Entity<Zg01>(entity =>
            {
                entity.HasKey(e => e.GradeXtraId);

                entity.ToTable("ZG01");

                entity.Property(e => e.GradeXtraId).HasColumnName("GradeXtraID");

                entity.Property(e => e.Currency)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Rate).HasColumnType("decimal(13, 2)");

                entity.Property(e => e.SapsoldtoId).HasColumnName("SAPSoldtoID");

                entity.Property(e => e.SaptubeStandardId).HasColumnName("SAPTubeStandardID");

                entity.Property(e => e.Unit)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ValidFrom).HasColumnType("date");

                entity.Property(e => e.ValidTo).HasColumnType("date");

                entity.HasOne(d => d.SaptubeStandard)
                    .WithMany(p => p.Zg01s)
                    .HasForeignKey(d => d.SaptubeStandardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ZG01_SAPCharacteristicOption");
            });

            modelBuilder.Entity<Zr00>(entity =>
            {
                entity.HasKey(e => e.ConditionId);

                entity.ToTable("ZR00");

                entity.Property(e => e.ConditionId).HasColumnName("conditionID");

                entity.Property(e => e.Currency)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Rate).HasColumnType("decimal(13, 2)");

                entity.Property(e => e.SapmaterialPricingGroupId).HasColumnName("SAPMaterialPricingGroupID");

                entity.Property(e => e.SapsoldtoId).HasColumnName("SAPSoldtoID");

                entity.Property(e => e.Unit)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ValidFrom).HasColumnType("date");

                entity.Property(e => e.ValidTo).HasColumnType("date");

                entity.HasOne(d => d.SapmaterialPricingGroup)
                    .WithMany(p => p.Zr00s)
                    .HasForeignKey(d => d.SapmaterialPricingGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ZR00_SAPCharacteristicOption");

                entity.HasOne(d => d.Sapsoldto)
                    .WithMany(p => p.Zr00s)
                    .HasForeignKey(d => d.SapsoldtoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ZR00_SAPShipTo");
            });

            modelBuilder.Entity<Zr01>(entity =>
            {
                entity.ToTable("ZR01");

                entity.Property(e => e.Zr01id).HasColumnName("ZR01ID");

                entity.Property(e => e.Currency)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Rate).HasColumnType("decimal(13, 2)");

                entity.Property(e => e.SappricingGroupId).HasColumnName("SAPPricingGroupID");

                entity.Property(e => e.SapsoldtoId).HasColumnName("SAPSoldtoID");

                entity.Property(e => e.Unit)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ValidFrom).HasColumnType("date");

                entity.Property(e => e.ValidTo).HasColumnType("date");

                entity.HasOne(d => d.SappricingGroup)
                    .WithMany(p => p.Zr01s)
                    .HasForeignKey(d => d.SappricingGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ZR01_SAPCharacteristicOption");

                entity.HasOne(d => d.Sapsoldto)
                    .WithMany(p => p.Zr01s)
                    .HasForeignKey(d => d.SapsoldtoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ZR01_SAPShipTo");
            });

            modelBuilder.Entity<Zr04>(entity =>
            {
                entity.ToTable("ZR04");

                entity.Property(e => e.Zr04id).HasColumnName("ZR04ID");

                entity.Property(e => e.Currency)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Rate).HasColumnType("decimal(13, 2)");

                entity.Property(e => e.SapmaterialGroupId).HasColumnName("SAPMaterialGroupID");

                entity.Property(e => e.SapsoldtoId).HasColumnName("SAPSoldtoID");

                entity.Property(e => e.Unit)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ValidFrom).HasColumnType("date");

                entity.Property(e => e.ValidTo).HasColumnType("date");

                entity.HasOne(d => d.SapmaterialGroup)
                    .WithMany(p => p.Zr04s)
                    .HasForeignKey(d => d.SapmaterialGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ZR04_SAPCharacteristicOption");

                entity.HasOne(d => d.Sapsoldto)
                    .WithMany(p => p.Zr04s)
                    .HasForeignKey(d => d.SapsoldtoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ZR04_SAPSoldTo");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
