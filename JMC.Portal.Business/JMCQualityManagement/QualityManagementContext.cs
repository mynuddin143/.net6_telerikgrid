using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace JMC.Portal.Business.JMCQualityManagement
{
    public partial class QualityManagementContext : DbContext
    {
        public QualityManagementContext()
        {
        }

        public QualityManagementContext(DbContextOptions<QualityManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CaractionType> CaractionTypes { get; set; } = null!;
        public virtual DbSet<ClaimPlant> ClaimPlants { get; set; } = null!;
        public virtual DbSet<ClaimProductDetail> ClaimProductDetails { get; set; } = null!;
        public virtual DbSet<ClaimRequest> ClaimRequests { get; set; } = null!;
        public virtual DbSet<ClaimRequestDocument> ClaimRequestDocuments { get; set; } = null!;
        public virtual DbSet<ClaimType> ClaimTypes { get; set; } = null!;
        public virtual DbSet<CorrectivePreventiveAction> CorrectivePreventiveActions { get; set; } = null!;
        public virtual DbSet<CorrectivePreventiveActionDepartment> CorrectivePreventiveActionDepartments { get; set; } = null!;
        public virtual DbSet<CorrectivePreventiveActionDocument> CorrectivePreventiveActionDocuments { get; set; } = null!;
        public virtual DbSet<CorrectivePreventiveActionProcess> CorrectivePreventiveActionProcesses { get; set; } = null!;
        public virtual DbSet<CorrectivePreventiveActionScope> CorrectivePreventiveActionScopes { get; set; } = null!;
        public virtual DbSet<CorrectivePreventiveActionSeverity> CorrectivePreventiveActionSeverities { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<DefectClassification> DefectClassifications { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<FiscalMonthDateRange> FiscalMonthDateRanges { get; set; } = null!;
        public virtual DbSet<InvoiceAdjApprover> InvoiceAdjApprovers { get; set; } = null!;
        public virtual DbSet<InvoiceAdjDocument> InvoiceAdjDocuments { get; set; } = null!;
        public virtual DbSet<InvoiceAdjMaterial> InvoiceAdjMaterials { get; set; } = null!;
        public virtual DbSet<InvoiceAdjNature> InvoiceAdjNatures { get; set; } = null!;
        public virtual DbSet<InvoiceAdjPl> InvoiceAdjPls { get; set; } = null!;
        public virtual DbSet<InvoiceAdjRequestType> InvoiceAdjRequestTypes { get; set; } = null!;
        public virtual DbSet<InvoiceAdjustment> InvoiceAdjustments { get; set; } = null!;
        public virtual DbSet<ManagementOfChange> ManagementOfChanges { get; set; } = null!;
        public virtual DbSet<Material> Materials { get; set; } = null!;
        public virtual DbSet<Mocapproval> Mocapprovals { get; set; } = null!;
        public virtual DbSet<Mocapprover> Mocapprovers { get; set; } = null!;
        public virtual DbSet<Mocdecision> Mocdecisions { get; set; } = null!;
        public virtual DbSet<Mocdocument> Mocdocuments { get; set; } = null!;
        public virtual DbSet<MocplantEamilAddress> MocplantEamilAddresses { get; set; } = null!;
        public virtual DbSet<MocriskAnalysis> MocriskAnalyses { get; set; } = null!;
        public virtual DbSet<NonConformity> NonConformities { get; set; } = null!;
        public virtual DbSet<NonConformityType> NonConformityTypes { get; set; } = null!;
        public virtual DbSet<Plant> Plants { get; set; } = null!;
        public virtual DbSet<ProductComplaint> ProductComplaints { get; set; } = null!;
        public virtual DbSet<ProductComplaintCategory> ProductComplaintCategories { get; set; } = null!;
        public virtual DbSet<ProductComplaintClaimStatus> ProductComplaintClaimStatuses { get; set; } = null!;
        public virtual DbSet<ProductComplaintDocument> ProductComplaintDocuments { get; set; } = null!;
        public virtual DbSet<ProductComplaintProductDetail> ProductComplaintProductDetails { get; set; } = null!;
        public virtual DbSet<Qdrform> Qdrforms { get; set; } = null!;
        public virtual DbSet<QdrformsDocument> QdrformsDocuments { get; set; } = null!;
        public virtual DbSet<Rgadocument> Rgadocuments { get; set; } = null!;
        public virtual DbSet<RgaemailConfig> RgaemailConfigs { get; set; } = null!;
        public virtual DbSet<Rgaform> Rgaforms { get; set; } = null!;
        public virtual DbSet<RgaproductDetail> RgaproductDetails { get; set; } = null!;
        public virtual DbSet<SalesAgent> SalesAgents { get; set; } = null!;
        public virtual DbSet<SalesOrg> SalesOrgs { get; set; } = null!;
        public virtual DbSet<SapinvoiceDetail> SapinvoiceDetails { get; set; } = null!;
        public virtual DbSet<SapinvoiceDetailsZr> SapinvoiceDetailsZrs { get; set; } = null!;
        public virtual DbSet<Sapsale> Sapsales { get; set; } = null!;
        public virtual DbSet<Supplier> Suppliers { get; set; } = null!;
        public virtual DbSet<SupplierComplaint> SupplierComplaints { get; set; } = null!;
        public virtual DbSet<SupplierComplaintDocument> SupplierComplaintDocuments { get; set; } = null!;
        public virtual DbSet<Trial> Trials { get; set; } = null!;
        public virtual DbSet<TrialDepartment> TrialDepartments { get; set; } = null!;
        public virtual DbSet<TrialDocument> TrialDocuments { get; set; } = null!;
        public virtual DbSet<TrialResponsibility> TrialResponsibilities { get; set; } = null!;
        public virtual DbSet<TrialUser> TrialUsers { get; set; } = null!;
        public virtual DbSet<UnitOfMeasure> UnitOfMeasures { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=JMCWEBDEV\\DEV\n;Initial Catalog= QualityManagement;User ID=PortalUser;Password=p@ssw0rd\n;MultipleActiveResultSets=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClaimProductDetail>(entity =>
            {
                entity.Property(e => e.Unit).IsFixedLength();
            });

            modelBuilder.Entity<CorrectivePreventiveAction>(entity =>
            {
                entity.HasOne(d => d.CorrectivePreventiveActionProcess)
                    .WithMany(p => p.CorrectivePreventiveActions)
                    .HasForeignKey(d => d.CorrectivePreventiveActionProcessId)
                    .HasConstraintName("FK_CorrectivePreventiveAction_CorrectivePreventiveActionProcess");

                entity.HasOne(d => d.CorrectivePreventiveActionScope)
                    .WithMany(p => p.CorrectivePreventiveActions)
                    .HasForeignKey(d => d.CorrectivePreventiveActionScopeId)
                    .HasConstraintName("FK_CorrectivePreventiveAction_CorrectivePreventiveActionScope");

                entity.HasOne(d => d.CorrectivePreventiveActionSeverity)
                    .WithMany(p => p.CorrectivePreventiveActions)
                    .HasForeignKey(d => d.CorrectivePreventiveActionSeverityId)
                    .HasConstraintName("FK_CorrectivePreventiveAction_CorrectivePreventiveActionSeverity");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.CorrectivePreventiveActions)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CorrectivePreventiveAction_CorrectivePreventiveActionDepartment");

                entity.HasOne(d => d.ProductComplaint)
                    .WithMany(p => p.CorrectivePreventiveActions)
                    .HasForeignKey(d => d.ProductComplaintId)
                    .HasConstraintName("FK_CorrectivePreventiveAction_ProductComplaint");
            });

            modelBuilder.Entity<CorrectivePreventiveActionDocument>(entity =>
            {
                entity.HasOne(d => d.CorrectivePreventiveAction)
                    .WithMany(p => p.CorrectivePreventiveActionDocuments)
                    .HasForeignKey(d => d.CorrectivePreventiveActionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CorrectivePreventiveActionDocument_CorrectivePreventiveAction");
            });

            modelBuilder.Entity<CorrectivePreventiveActionProcess>(entity =>
            {
                entity.Property(e => e.Name).IsFixedLength();
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToView("Customer");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToView("Employee");
            });

            modelBuilder.Entity<InvoiceAdjMaterial>(entity =>
            {
                entity.Property(e => e.Unit).IsFixedLength();
            });

            modelBuilder.Entity<Material>(entity =>
            {
                entity.ToView("Material");
            });

            modelBuilder.Entity<Mocapproval>(entity =>
            {
                entity.HasOne(d => d.Approver)
                    .WithMany(p => p.Mocapprovals)
                    .HasForeignKey(d => d.ApproverId)
                    .HasConstraintName("FK_MOCApprovals_Approver");

                entity.HasOne(d => d.Moc)
                    .WithMany(p => p.Mocapprovals)
                    .HasForeignKey(d => d.Mocid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MOCApprovals_MOC");
            });

            modelBuilder.Entity<Mocdocument>(entity =>
            {
                entity.HasOne(d => d.Moc)
                    .WithMany(p => p.Mocdocuments)
                    .HasForeignKey(d => d.Mocid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MOCDocument_MOC");
            });

            modelBuilder.Entity<MocriskAnalysis>(entity =>
            {
                entity.HasOne(d => d.Moc)
                    .WithMany(p => p.MocriskAnalyses)
                    .HasForeignKey(d => d.Mocid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MOCRiskAnalysis_MOC");
            });

            modelBuilder.Entity<NonConformity>(entity =>
            {
                entity.HasOne(d => d.NonConformityType)
                    .WithMany(p => p.NonConformities)
                    .HasForeignKey(d => d.NonConformityTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NonConformity_NonConformityType");
            });

            modelBuilder.Entity<Plant>(entity =>
            {
                entity.ToView("Plant");
            });

            modelBuilder.Entity<ProductComplaintCategory>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.SalesAgentType).IsFixedLength();
            });

            modelBuilder.Entity<ProductComplaintClaimStatus>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<ProductComplaintDocument>(entity =>
            {
                entity.HasOne(d => d.ProductComplaint)
                    .WithMany(p => p.ProductComplaintDocuments)
                    .HasForeignKey(d => d.ProductComplaintId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductComplaintDocument_ProductComplaint");
            });

            modelBuilder.Entity<ProductComplaintProductDetail>(entity =>
            {
                entity.Property(e => e.Unit).IsFixedLength();
            });

            modelBuilder.Entity<Qdrform>(entity =>
            {
                entity.Property(e => e.ApprovedWtc).IsFixedLength();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<RgaemailConfig>(entity =>
            {
                entity.Property(e => e.EmailType).IsFixedLength();
            });

            modelBuilder.Entity<RgaproductDetail>(entity =>
            {
                entity.Property(e => e.Unit).IsFixedLength();
            });

            modelBuilder.Entity<SalesAgent>(entity =>
            {
                entity.Property(e => e.AgentType).IsFixedLength();

                entity.Property(e => e.SaprepNumber).IsFixedLength();
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.ToView("Supplier");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<SupplierComplaint>(entity =>
            {
                entity.HasOne(d => d.CorrectivePreventiveAction)
                    .WithMany(p => p.SupplierComplaints)
                    .HasForeignKey(d => d.CorrectivePreventiveActionId)
                    .HasConstraintName("FK_SupplierComplaint_CorrectivePreventiveAction");

                entity.HasOne(d => d.MaterialUnitOfMeasure)
                    .WithMany(p => p.SupplierComplaintMaterialUnitOfMeasures)
                    .HasForeignKey(d => d.MaterialUnitOfMeasureId)
                    .HasConstraintName("FK_SupplierComplaint_UnitOfMeasure");

                entity.HasOne(d => d.NonConformity)
                    .WithMany(p => p.SupplierComplaints)
                    .HasForeignKey(d => d.NonConformityId)
                    .HasConstraintName("FK_SupplierComplaint_NonConformity");

                entity.HasOne(d => d.NonConformityType)
                    .WithMany(p => p.SupplierComplaints)
                    .HasForeignKey(d => d.NonConformityTypeId)
                    .HasConstraintName("FK_SupplierComplaint_NonConformityType");

                entity.HasOne(d => d.UnitOfMeasure)
                    .WithMany(p => p.SupplierComplaintUnitOfMeasures)
                    .HasForeignKey(d => d.UnitOfMeasureId)
                    .HasConstraintName("FK_SupplierComplaint_UnitOfMeasure1");
            });

            modelBuilder.Entity<SupplierComplaintDocument>(entity =>
            {
                entity.HasOne(d => d.SupplierComplaint)
                    .WithMany(p => p.SupplierComplaintDocuments)
                    .HasForeignKey(d => d.SupplierComplaintId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SupplierComplaintDocument_SupplierComplaint");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
