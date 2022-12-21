using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace JMC.Portal.Business.IntranetModel
{
    public partial class IntranetEntities : DbContext
    {
        public IntranetEntities()
        {
        }

        public IntranetEntities(DbContextOptions<IntranetEntities> options)
            : base(options)
        {
        }

        public virtual DbSet<AdditionalPhoneNumber> AdditionalPhoneNumbers { get; set; } = null!;
        public virtual DbSet<AlternateCoilIndicator> AlternateCoilIndicators { get; set; } = null!;
        public virtual DbSet<Application> Applications { get; set; } = null!;
        public virtual DbSet<ApplicationRole> ApplicationRoles { get; set; } = null!;
        public virtual DbSet<BusinessArea> BusinessAreas { get; set; } = null!;
        public virtual DbSet<Carrier> Carriers { get; set; } = null!;
        public virtual DbSet<City> Cities { get; set; } = null!;
        public virtual DbSet<Country> Countries { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<CustomerSalesOrg> CustomerSalesOrgs { get; set; } = null!;
        public virtual DbSet<CustomerShipTo> CustomerShipTos { get; set; } = null!;
        public virtual DbSet<CustomerUser> CustomerUsers { get; set; } = null!;
        public virtual DbSet<CustomerUserCustomer> CustomerUserCustomers { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Division> Divisions { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<EmployeePosition> EmployeePositions { get; set; } = null!;
        public virtual DbSet<EpoxycoatColor> EpoxycoatColors { get; set; } = null!;
        public virtual DbSet<InsideSalesRep> InsideSalesReps { get; set; } = null!;
        public virtual DbSet<KleenkoteColor> KleenkoteColors { get; set; } = null!;
        public virtual DbSet<Location> Locations { get; set; } = null!;
        public virtual DbSet<LocationDepartment> LocationDepartments { get; set; } = null!;
        public virtual DbSet<LoginHistory> LoginHistories { get; set; } = null!;
        public virtual DbSet<Material> Materials { get; set; } = null!;
        public virtual DbSet<MaterialBundling> MaterialBundlings { get; set; } = null!;
        public virtual DbSet<MaterialGroup> MaterialGroups { get; set; } = null!;
        public virtual DbSet<MaterialUnitOfMeasure> MaterialUnitOfMeasures { get; set; } = null!;
        public virtual DbSet<Mill> Mills { get; set; } = null!;
        public virtual DbSet<NewsStory> NewsStories { get; set; } = null!;
        public virtual DbSet<OutsideSalesRep> OutsideSalesReps { get; set; } = null!;
        public virtual DbSet<PhoneNumber> PhoneNumbers { get; set; } = null!;
        public virtual DbSet<Plant> Plants { get; set; } = null!;
        public virtual DbSet<PlantComputer> PlantComputers { get; set; } = null!;
        public virtual DbSet<PricingCondition> PricingConditions { get; set; } = null!;
        public virtual DbSet<RandomLengthCustomer> RandomLengthCustomers { get; set; } = null!;
        public virtual DbSet<RandomLengthSAPSoldTo> RandomLengthSAPSoldTos { get; set; } = null!;
        public virtual DbSet<RectangleMaterialBundling> RectangleMaterialBundlings { get; set; } = null!;
        public virtual DbSet<RoundMaterialBundling> RoundMaterialBundlings { get; set; } = null!;
        public virtual DbSet<SalesInstruction> SalesInstructions { get; set; } = null!;
        public virtual DbSet<SalesOrg> SalesOrgs { get; set; } = null!;
        public virtual DbSet<SAPAlternateCoilIndicator> SAPAlternateCoilIndicators { get; set; } = null!;
        public virtual DbSet<SAPBundlingOption> SAPBundlingOptions { get; set; } = null!;
        public virtual DbSet<SAPConditionGroup> SAPConditionGroups { get; set; } = null!;
        public virtual DbSet<SAPCustomerGroup> SAPCustomerGroups { get; set; } = null!;
        public virtual DbSet<SAPCustomerGroupUser> SAPCustomerGroupUsers { get; set; } = null!;
        public virtual DbSet<SAPEpoxycoatColor> SAPEpoxycoatColors { get; set; } = null!;
        public virtual DbSet<SAPKleenkoteColor> SAPKleenkoteColors { get; set; } = null!;
        public virtual DbSet<SAPMaterial> SAPMaterials { get; set; } = null!;
        public virtual DbSet<SAPMaterialGroup> SAPMaterialGroups { get; set; } = null!;
        public virtual DbSet<SAPMaterialPricingGroup> SAPMaterialPricingGroups { get; set; } = null!;
        public virtual DbSet<SAPMaterialSAPBundlingOption> SAPMaterialSAPBundlingOptions { get; set; } = null!;
        public virtual DbSet<SAPMaterialType> SAPMaterialTypes { get; set; } = null!;
        public virtual DbSet<SAPMaterialUnitOfMeasure> SAPMaterialUnitOfMeasures { get; set; } = null!;
        public virtual DbSet<SAPPricingCondition> SAPPricingConditions { get; set; } = null!;
        public virtual DbSet<SAPPricingGroup> SAPPricingGroups { get; set; } = null!;
        public virtual DbSet<SAPSalesGroup> SAPSalesGroups { get; set; } = null!;
        public virtual DbSet<SAPSalesInstruction> SAPSalesInstructions { get; set; } = null!;
        public virtual DbSet<SAPShipTo> SAPShipTos { get; set; } = null!;
        public virtual DbSet<SAPSoldTo> SAPSoldTos { get; set; } = null!;
        public virtual DbSet<SAPSoldToSAPShipTo> SAPSoldToSAPShipTos { get; set; } = null!;
        public virtual DbSet<SAPSpecification> SAPSpecifications { get; set; } = null!;
        public virtual DbSet<SAPTubeShape> SAPTubeShapes { get; set; } = null!;
        public virtual DbSet<SAPTubeStandard> SAPTubeStandards { get; set; } = null!;
        public virtual DbSet<SAPDelivery> SAPDeliverys { get; set; } = null!;
        public virtual DbSet<ScrapCustomer> ScrapCustomers { get; set; } = null!;
        public virtual DbSet<ScrapSAPSoldTo> ScrapSAPSoldTos { get; set; } = null!;
        public virtual DbSet<Shift> Shifts { get; set; } = null!;
        public virtual DbSet<ShipTo> ShipTos { get; set; } = null!;
        public virtual DbSet<State> States { get; set; } = null!;
        public virtual DbSet<SteelGrade> SteelGrades { get; set; } = null!;
        public virtual DbSet<TubeShape> TubeShapes { get; set; } = null!;
        public virtual DbSet<TubeSpecification> TubeSpecifications { get; set; } = null!;
        public virtual DbSet<TubeStandard> TubeStandards { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserApplicationRole> UserApplicationRoles { get; set; } = null!;
        public virtual DbSet<UserProfile> UserProfiles { get; set; } = null!;
        public virtual DbSet<Vendor> Vendors { get; set; } = null!;
        public virtual DbSet<VwContact> VwContacts { get; set; } = null!;
        public virtual DbSet<VwEmployee> VwEmployees { get; set; } = null!;
        public virtual DbSet<VwMaterial> VwMaterials { get; set; } = null!;
        public virtual DbSet<VwPhoneNumber> VwPhoneNumbers { get; set; } = null!;
        public virtual DbSet<VwShipTo> VwShipTos { get; set; } = null!;
        public virtual DbSet<Warehouse> Warehouses { get; set; } = null!;

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Data Source=JMCWEBDEV\\DEV;Initial Catalog=Intranet;User ID=WebsiteUser;Password=p@ssw0rd;MultipleActiveResultSets=True;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdditionalPhoneNumber>(entity =>
            {
                entity.HasOne(d => d.Location)
                    .WithMany(p => p.AdditionalPhoneNumbers)
                    .HasForeignKey(d => d.LocationID)
                    .HasConstraintName("FK_AdditionalPhoneNumber_Location");
            });

            modelBuilder.Entity<ApplicationRole>(entity =>
            {
                entity.HasOne(d => d.Application)
                    .WithMany(p => p.ApplicationRoles)
                    .HasForeignKey(d => d.ApplicationID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IntranetApplicationRole_IntranetApplication");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasOne(d => d.State)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.StateID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_City_State");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.SAPCustomerID).IsFixedLength();

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.CityID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Customer_City");

                entity.HasOne(d => d.InsideSalesRep)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.InsideSalesRepID)
                    .HasConstraintName("FK_Customer_InsideSalesRep1");

                entity.HasOne(d => d.OutsideSalesRep)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.OutsideSalesRepID)
                    .HasConstraintName("FK_Customer_OutsideSalesRep1");
            });

            modelBuilder.Entity<CustomerUser>(entity =>
            {
                entity.Property(e => e.CustomerUserID).ValueGeneratedNever();

                entity.HasOne(d => d.CustomerUserNavigation)
                    .WithOne(p => p.CustomerUser)
                    .HasForeignKey<CustomerUser>(d => d.CustomerUserID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerUser_User");

                entity.HasOne(d => d.PrimaryCustomer)
                    .WithMany(p => p.CustomerUsers)
                    .HasForeignKey(d => d.PrimaryCustomerID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerUser_Customer");
            });

            modelBuilder.Entity<CustomerUserCustomer>(entity =>
            {
                entity.HasIndex(e => e.CustomerUserID, "IX_CustomerUserCustomer")
                    .IsClustered();

                entity.HasOne(d => d.Customer)
                    .WithMany()
                    .HasForeignKey(d => d.CustomerID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerUserCustomer_Customer");

                entity.HasOne(d => d.CustomerUser)
                    .WithMany()
                    .HasForeignKey(d => d.CustomerUserID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerUserCustomer_CustomerUser");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.EmployeeID).ValueGeneratedNever();

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DepartmentID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_Department");

                entity.HasOne(d => d.Division)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DivisionID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_Division");

                entity.HasOne(d => d.EmployeeNavigation)
                    .WithOne(p => p.Employee)
                    .HasForeignKey<Employee>(d => d.EmployeeID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_User");

                entity.HasOne(d => d.EmployeePosition)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.EmployeePositionID)
                    .HasConstraintName("FK_Employee_EmployeePosition");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.LocationID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_Location");
            });

            modelBuilder.Entity<InsideSalesRep>(entity =>
            {
                entity.Property(e => e.SAPSalesGroupID).IsFixedLength();

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.InsideSalesReps)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_InsideSalesRep_Employee");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.Property(e => e.LocationPrefix).IsFixedLength();

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.CityID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Location_City");

                entity.HasOne(d => d.Division)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.DivisionID)
                    .HasConstraintName("FK_Location_Division");
            });

            modelBuilder.Entity<LocationDepartment>(entity =>
            {
                entity.HasIndex(e => e.LocationID, "IX_LocationDepartment")
                    .IsClustered();

                entity.HasOne(d => d.Department)
                    .WithMany()
                    .HasForeignKey(d => d.DepartmentID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LocationDepartment_Department");

                entity.HasOne(d => d.Location)
                    .WithMany()
                    .HasForeignKey(d => d.LocationID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LocationDepartment_Location");
            });

            modelBuilder.Entity<LoginHistory>(entity =>
            {
                entity.HasIndex(e => new { e.UserID, e.LoginDate }, "ix_UserID_LoginDate")
                    .HasFillFactor(100);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.LoginHistories)
                    .HasForeignKey(d => d.UserID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LoginHistory_IntranetUser");
            });

            modelBuilder.Entity<Material>(entity =>
            {
                entity.HasIndex(e => e.Number, "IX_Material")
                    .IsUnique()
                    .HasFillFactor(100);

                entity.HasIndex(e => new { e.GaugeRestrictable, e.TubeWeightPerFoot }, "ix_GaugeRestrictable_TubeWeightPerFoot")
                    .HasFillFactor(100);

                entity.HasIndex(e => new { e.TubeDiameter, e.GaugeRestrictable, e.TubeWeightPerFoot }, "ix_TubeDiameter_GaugeRestrictable_TubeWeightPerFoot")
                    .HasFillFactor(100);

                entity.HasIndex(e => new { e.TubeDiameter, e.TubeLength, e.GaugeRestrictable, e.TubeWeightPerFoot }, "ix_TubeDiameter_TubeLength_GaugeRestrictable_TubeWeightPerFoot")
                    .HasFillFactor(100);

                entity.HasIndex(e => new { e.TubeDiameter, e.TubeWeightPerFoot }, "ix_TubeDiameter_TubeWeightPerFoot")
                    .HasFillFactor(100);

                entity.HasIndex(e => new { e.TubeSize, e.GaugeRestrictable, e.TubeWeightPerFoot }, "ix_TubeSize_GaugeRestrictable_TubeWeightPerFoot")
                    .HasFillFactor(100);

                entity.HasIndex(e => new { e.TubeSize, e.TubeLength, e.GaugeRestrictable, e.TubeWeightPerFoot }, "ix_TubeSize_TubeLength_GaugeRestrictable_TubeWeightPerFoot")
                    .HasFillFactor(100);

                entity.HasIndex(e => new { e.TubeSize, e.TubeLength, e.TubeStandardID, e.GaugeRestrictable, e.TubeWeightPerFoot }, "ix_TubeSize_TubeLength_TubeStandardID_GaugeRestrictable_TubeWeightPerFoot")
                    .HasFillFactor(100);

                entity.HasIndex(e => new { e.TubeSize, e.TubeSize2, e.GaugeRestrictable, e.TubeWeightPerFoot }, "ix_TubeSize_TubeSize2_GaugeRestrictable_TubeWeightPerFoot")
                    .HasFillFactor(100);

                entity.HasIndex(e => new { e.TubeSize, e.TubeSize2, e.TubeLength, e.GaugeRestrictable, e.TubeWeightPerFoot }, "ix_TubeSize_TubeSize2_TubeLength_GaugeRestrictable_TubeWeightPerFoot")
                    .HasFillFactor(100);

                entity.HasIndex(e => new { e.TubeSize, e.TubeSize2, e.TubeLength, e.TubeStandardID, e.GaugeRestrictable, e.TubeWeightPerFoot }, "ix_TubeSize_TubeSize2_TubeLength_TubeStandardID_GaugeRestrictable_TubeWeightPerFoot")
                    .HasFillFactor(100);

                entity.HasIndex(e => new { e.TubeSize, e.TubeSize2, e.TubeStandardID, e.TubeWeightPerFoot }, "ix_TubeSize_TubeSize2_TubeStandardID_TubeWeightPerFoot")
                    .HasFillFactor(100);

                entity.HasIndex(e => new { e.TubeSize, e.TubeSize2, e.TubeWeightPerFoot }, "ix_TubeSize_TubeSize2_TubeWeightPerFoot")
                    .HasFillFactor(100);

                entity.HasIndex(e => new { e.TubeSize, e.TubeWeightPerFoot }, "ix_TubeSize_TubeWeightPerFoot")
                    .HasFillFactor(100);

                entity.HasIndex(e => e.TubeWeightPerFoot, "ix_TubeWeightPerFoot")
                    .HasFillFactor(100);

                entity.HasOne(d => d.AlternateCoilIndicator)
                    .WithMany(p => p.Materials)
                    .HasForeignKey(d => d.AlternateCoilIndicatorId)
                    .HasConstraintName("FK_Material_AlternateCoilIndicator");

                entity.HasOne(d => d.EpoxycoatColor)
                    .WithMany(p => p.Materials)
                    .HasForeignKey(d => d.EpoxycoatColorID)
                    .HasConstraintName("FK_Material_EpoxycoatColor");

                entity.HasOne(d => d.KleenkoteColor)
                    .WithMany(p => p.Materials)
                    .HasForeignKey(d => d.KleenkoteColorID)
                    .HasConstraintName("FK_Material_KleenkoteColor");

                entity.HasOne(d => d.MaterialGroup)
                    .WithMany(p => p.Materials)
                    .HasForeignKey(d => d.MaterialGroupID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Material_MaterialGroup");

                entity.HasOne(d => d.SalesInstruction)
                    .WithMany(p => p.Materials)
                    .HasForeignKey(d => d.SalesInstructionID)
                    .HasConstraintName("FK_Material_SalesInstruction");

                entity.HasOne(d => d.TubeShape)
                    .WithMany(p => p.Materials)
                    .HasForeignKey(d => d.TubeShapeID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Material_TubeShape");

                entity.HasOne(d => d.TubeSpecification)
                    .WithMany(p => p.Materials)
                    .HasForeignKey(d => d.TubeSpecificationID)
                    .HasConstraintName("FK_Material_TubeSpecification");

                entity.HasOne(d => d.TubeStandard)
                    .WithMany(p => p.Materials)
                    .HasForeignKey(d => d.TubeStandardID)
                    .HasConstraintName("FK_Material_TubeStandard");
            });

            modelBuilder.Entity<MaterialBundling>(entity =>
            {
                entity.HasOne(d => d.Material)
                    .WithMany(p => p.MaterialBundlings)
                    .HasForeignKey(d => d.MaterialID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MaterialBundling_Material");

                entity.HasOne(d => d.RectangleMaterialBundling)
                    .WithMany(p => p.MaterialBundlings)
                    .HasForeignKey(d => d.RectangleMaterialBundlingID)
                    .HasConstraintName("FK_MaterialBundling_RectangleMaterialBundling");

                entity.HasOne(d => d.RoundMaterialBundling)
                    .WithMany(p => p.MaterialBundlings)
                    .HasForeignKey(d => d.RoundMaterialBundlingID)
                    .HasConstraintName("FK_MaterialBundling_RoundMaterialBundling");
            });

            modelBuilder.Entity<Mill>(entity =>
            {
                entity.HasOne(d => d.Plant)
                    .WithMany(p => p.Mills)
                    .HasForeignKey(d => d.PlantID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Mill_Plant");
            });

            modelBuilder.Entity<OutsideSalesRep>(entity =>
            {
                entity.Property(e => e.SAPCustomerGroupID).IsFixedLength();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.OutsideSalesReps)
                    .HasForeignKey(d => d.UserID)
                    .HasConstraintName("FK_OutsideSalesRep_IntranetUser");
            });

            modelBuilder.Entity<PhoneNumber>(entity =>
            {
                entity.HasOne(d => d.Location)
                    .WithMany(p => p.PhoneNumbers)
                    .HasForeignKey(d => d.LocationID)
                    .HasConstraintName("FK_PhoneNumber_Location");
            });

            modelBuilder.Entity<Plant>(entity =>
            {
                entity.Property(e => e.PlantID).ValueGeneratedNever();

                entity.Property(e => e.PlantCode).IsFixedLength();

                entity.Property(e => e.SalesOrganization).IsFixedLength();

                entity.HasOne(d => d.PlantNavigation)
                    .WithOne(p => p.Plant)
                    .HasForeignKey<Plant>(d => d.PlantID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Plant_Location");
            });

            modelBuilder.Entity<PlantComputer>(entity =>
            {
                entity.Property(e => e.PlantComputerID).ValueGeneratedNever();

                entity.HasOne(d => d.Division)
                    .WithMany(p => p.PlantComputers)
                    .HasForeignKey(d => d.DivisionID)
                    .HasConstraintName("FK_PlantComputer_Division");

                entity.HasOne(d => d.PlantComputerNavigation)
                    .WithOne(p => p.PlantComputer)
                    .HasForeignKey<PlantComputer>(d => d.PlantComputerID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlantComputer_User");
            });

            modelBuilder.Entity<PricingCondition>(entity =>
            {
                entity.Property(e => e.Sapcode2).IsFixedLength();
            });

            modelBuilder.Entity<RandomLengthCustomer>(entity =>
            {
                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.RandomLengthCustomers)
                    .HasForeignKey(d => d.CustomerID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RandomLengthCustomer_Customer");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.RandomLengthCustomers)
                    .HasForeignKey(d => d.LocationID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RandomLengthCustomer_Location");
            });

            modelBuilder.Entity<RandomLengthSAPSoldTo>(entity =>
            {
                entity.HasOne(d => d.Location)
                    .WithMany(p => p.RandomLengthSAPSoldTos)
                    .HasForeignKey(d => d.LocationID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RandomLengthSAPSoldTo_Location");

                entity.HasOne(d => d.SAPSoldTo)
                    .WithMany(p => p.RandomLengthSAPSoldTos)
                    .HasForeignKey(d => d.SAPSoldToID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RandomLengthSAPSoldTo_SAPSoldTo");
            });

            modelBuilder.Entity<SalesOrg>(entity =>
            {
                entity.Property(e => e.Name).IsFixedLength();
            });

            modelBuilder.Entity<SAPCustomerGroup>(entity =>
            {
                entity.HasOne(d => d.RegionalManagerUser)
                    .WithMany(p => p.SAPCustomerGroups)
                    .HasForeignKey(d => d.RegionalManagerUserID)
                    .HasConstraintName("FK_SAPCustomerGroup_User");
            });

            modelBuilder.Entity<SAPCustomerGroupUser>(entity =>
            {
                entity.HasOne(d => d.SAPCustomerGroup)
                    .WithMany(p => p.SAPCustomerGroupUsers)
                    .HasForeignKey(d => d.SAPCustomerGroupID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPCustomerGroupUser_SAPCustomerGroup");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SAPCustomerGroupUsers)
                    .HasForeignKey(d => d.UserID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPCustomerGroupUser_User");
            });

            modelBuilder.Entity<SAPMaterial>(entity =>
            {
                entity.HasIndex(e => e.Number, "IX_SAPMaterial")
                    .IsUnique()
                    .HasFillFactor(100);

                entity.HasOne(d => d.SAPAlternateCoilIndicator)
                    .WithMany(p => p.SAPMaterials)
                    .HasForeignKey(d => d.SAPAlternateCoilIndicatorID)
                    .HasConstraintName("FK_SAPMaterial_SAPAlternateCoilIndicator");

                entity.HasOne(d => d.SAPEpoxycoatColor)
                    .WithMany(p => p.SAPMaterials)
                    .HasForeignKey(d => d.SAPEpoxycoatColorID)
                    .HasConstraintName("FK_SAPMaterial_SAPEpoxycoatColor");

                entity.HasOne(d => d.SAPKleenkoteColor)
                    .WithMany(p => p.SAPMaterials)
                    .HasForeignKey(d => d.SAPKleenkoteColorID)
                    .HasConstraintName("FK_SAPMaterial_SAPKleenkoteColor");

                entity.HasOne(d => d.SAPMaterialGroup)
                    .WithMany(p => p.SAPMaterials)
                    .HasForeignKey(d => d.SAPMaterialGroupID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPMaterial_SAPMaterialGroup");

                entity.HasOne(d => d.SAPMaterialPricingGroup)
                    .WithMany(p => p.SAPMaterials)
                    .HasForeignKey(d => d.SAPMaterialPricingGroupID)
                    .HasConstraintName("FK_SAPMaterial_SAPMaterialPricingGroup");

                entity.HasOne(d => d.SAPMaterialType)
                    .WithMany(p => p.SAPMaterials)
                    .HasForeignKey(d => d.SAPMaterialTypeID)
                    .HasConstraintName("FK_SAPMaterial_SAPMaterialType");

                entity.HasOne(d => d.SAPPricingGroup)
                    .WithMany(p => p.SAPMaterials)
                    .HasForeignKey(d => d.SAPPricingGroupID)
                    .HasConstraintName("FK_SAPMaterial_SAPPricingGroup");

                entity.HasOne(d => d.SAPSalesInstruction)
                    .WithMany(p => p.SAPMaterials)
                    .HasForeignKey(d => d.SAPSalesInstructionID)
                    .HasConstraintName("FK_SAPMaterial_SAPSalesInstruction");

                entity.HasOne(d => d.SAPSpecification)
                    .WithMany(p => p.SAPMaterials)
                    .HasForeignKey(d => d.SAPSpecificationID)
                    .HasConstraintName("FK_SAPMaterial_SAPTubeSpecification");

                entity.HasOne(d => d.SAPTubeShape)
                    .WithMany(p => p.SAPMaterials)
                    .HasForeignKey(d => d.SAPTubeShapeID)
                    .HasConstraintName("FK_SAPMaterial_SAPTubeShape");

                entity.HasOne(d => d.SAPTubeStandard)
                    .WithMany(p => p.SAPMaterials)
                    .HasForeignKey(d => d.SAPTubeStandardID)
                    .HasConstraintName("FK_SAPMaterial_SAPTubeStandard");
            });

            modelBuilder.Entity<SAPMaterialSAPBundlingOption>(entity =>
            {
                entity.HasIndex(e => new { e.SAPMaterialID, e.SAPBundlingOptionID }, "IX_SAPMaterialSAPBundlingOption")
                    .IsUnique()
                    .HasFillFactor(100);
            });

            modelBuilder.Entity<SAPShipTo>(entity =>
            {
                entity.HasIndex(e => e.CityID, "ix_CityID")
                    .HasFillFactor(100);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.SAPShipTos)
                    .HasForeignKey(d => d.CityID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPShipTo_City");

                entity.HasOne(d => d.FreightIndicatorSAPConditionGroup)
                    .WithMany(p => p.SAPShipToFreightIndicatorSAPConditionGroups)
                    .HasForeignKey(d => d.FreightIndicatorSAPConditionGroupID)
                    .HasConstraintName("FK_SAPShipTo_SAPConditionGroup");

                entity.HasOne(d => d.FuelSurchargeSAPConditionGroup)
                    .WithMany(p => p.SAPShipToFuelSurchargeSAPConditionGroups)
                    .HasForeignKey(d => d.FuelSurchargeSAPConditionGroupID)
                    .HasConstraintName("FK_SAPShipTo_SAPConditionGroup1");
            });

            modelBuilder.Entity<SAPSoldTo>(entity =>
            {
                entity.Property(e => e.SAPSoldToID).ValueGeneratedNever();

                entity.Property(e => e.PricingProcedure).IsFixedLength();

                entity.HasOne(d => d.HomeMillSAPConditionGroup)
                    .WithMany(p => p.SAPSoldToHomeMillSAPConditionGroups)
                    .HasForeignKey(d => d.HomeMillSAPConditionGroupID)
                    .HasConstraintName("FK_SAPSoldTo_SAPConditionGroup2");

                entity.HasOne(d => d.RegionSAPConditionGroup)
                    .WithMany(p => p.SAPSoldToRegionSAPConditionGroups)
                    .HasForeignKey(d => d.RegionSAPConditionGroupID)
                    .HasConstraintName("FK_SAPSoldTo_SAPConditionGroup");

                entity.HasOne(d => d.SAPCustomerGroup)
                    .WithMany(p => p.SAPSoldTos)
                    .HasForeignKey(d => d.SAPCustomerGroupID)
                    .HasConstraintName("FK_SAPSoldTo_SAPCustomerGroup");

                entity.HasOne(d => d.SAPSalesGroup)
                    .WithMany(p => p.SAPSoldTos)
                    .HasForeignKey(d => d.SAPSalesGroupID)
                    .HasConstraintName("FK_SAPSoldTo_SAPSalesGroup");

                entity.HasOne(d => d.SAPSoldToNavigation)
                    .WithOne(p => p.SAPSoldTo)
                    .HasForeignKey<SAPSoldTo>(d => d.SAPSoldToID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPSoldTo_SAPShipTo");

                entity.HasOne(d => d.TierSAPConditionGroup)
                    .WithMany(p => p.SAPSoldToTierSAPConditionGroups)
                    .HasForeignKey(d => d.TierSAPConditionGroupId)
                    .HasConstraintName("FK_SAPSoldTo_SAPConditionGroup1");
            });

            modelBuilder.Entity<SAPSoldToSAPShipTo>(entity =>
            {
                entity.HasOne(d => d.SAPShipTo)
                    .WithMany(p => p.SAPSoldToSAPShipTos)
                    .HasForeignKey(d => d.SAPShipToID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPSoldToSAPShipTo_SAPShipTo1");

                entity.HasOne(d => d.SAPSoldTo)
                    .WithMany(p => p.SAPSoldToSAPShipTos)
                    .HasForeignKey(d => d.SAPSoldToID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPSoldToSAPShipTo_SAPSoldTo1");
            });

            modelBuilder.Entity<SAPDelivery>(entity =>
            {
                entity.HasIndex(e => e.Number, "IX_SAPDelivery")
                    .IsUnique()
                    .HasFillFactor(100);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.SAPDeliverys)
                    .HasForeignKey(d => d.CityID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAPDelivery_City");
            });

            modelBuilder.Entity<ScrapCustomer>(entity =>
            {
                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.ScrapCustomers)
                    .HasForeignKey(d => d.CustomerID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScrapCustomer_Customer");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.ScrapCustomers)
                    .HasForeignKey(d => d.LocationID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScrapCustomer_Location");
            });

            modelBuilder.Entity<ScrapSAPSoldTo>(entity =>
            {
                entity.HasOne(d => d.Location)
                    .WithMany(p => p.ScrapSAPSoldTos)
                    .HasForeignKey(d => d.LocationID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScrapSAPSoldTo_Location");

                entity.HasOne(d => d.SAPSoldTo)
                    .WithMany(p => p.ScrapSAPSoldTos)
                    .HasForeignKey(d => d.SAPSoldToID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScrapSAPSoldTo_SAPSoldTo");
            });

            modelBuilder.Entity<ShipTo>(entity =>
            {
                entity.Property(e => e.SAPShipToID).IsFixedLength();

                entity.HasOne(d => d.City)
                    .WithMany(p => p.ShipTos)
                    .HasForeignKey(d => d.CityID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShipTo_City");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.ShipTos)
                    .HasForeignKey(d => d.CustomerID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShipTo_Customer");
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.HasOne(d => d.Country)
                    .WithMany(p => p.States)
                    .HasForeignKey(d => d.CountryID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_State_Country");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email, "IX_User_Email")
                    .IsUnique()
                    .HasFillFactor(100);

                entity.HasIndex(e => e.UserName, "IX_User_UserName")
                    .IsUnique()
                    .HasFillFactor(100);
            });

            modelBuilder.Entity<UserApplicationRole>(entity =>
            {
                entity.HasIndex(e => e.UserID, "IX_UserApplicationRole")
                    .IsClustered();

                entity.HasOne(d => d.ApplicationRole)
                    .WithMany()
                    .HasForeignKey(d => d.ApplicationRoleID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IntranetUserApplicationRole_IntranetApplicationRole");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IntranetUserApplicationRole_IntranetUser");
            });

            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserProfiles)
                    .HasForeignKey(d => d.UserID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserProfile_User");
            });

            modelBuilder.Entity<Vendor>(entity =>
            {
                entity.HasOne(d => d.City)
                    .WithMany(p => p.Vendors)
                    .HasForeignKey(d => d.CityID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vendor_City");
            });

            modelBuilder.Entity<VwContact>(entity =>
            {
                entity.ToView("vwContact");
            });

            modelBuilder.Entity<VwEmployee>(entity =>
            {
                entity.ToView("vwEmployee");
            });

            modelBuilder.Entity<VwMaterial>(entity =>
            {
                entity.ToView("vwMaterial");
            });

            modelBuilder.Entity<VwPhoneNumber>(entity =>
            {
                entity.ToView("vwPhoneNumber");
            });

            modelBuilder.Entity<VwShipTo>(entity =>
            {
                entity.ToView("vwShipTo");

                entity.Property(e => e.SapcustomerId).IsFixedLength();

                entity.Property(e => e.SAPShipToId).IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
