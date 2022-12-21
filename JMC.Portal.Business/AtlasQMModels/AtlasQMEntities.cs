using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace JMC.Portal.Business
{
    public partial class AtlasQMEntities : DbContext
    {
        public AtlasQMEntities()
        {
        }

        public AtlasQMEntities(DbContextOptions<AtlasQMEntities> options)
            : base(options)
        {
        }

        public virtual DbSet<ActionForm> ActionForms { get; set; } = null!;
        public virtual DbSet<ActionFormScope> ActionFormScopes { get; set; } = null!;
        public virtual DbSet<ActionFormType> ActionFormTypes { get; set; } = null!;
        public virtual DbSet<AlertAttachment> AlertAttachments { get; set; } = null!;
        public virtual DbSet<Attachment> Attachments { get; set; } = null!;
        public virtual DbSet<Billing> Billings { get; set; } = null!;
        public virtual DbSet<ChangeApproval> ChangeApprovals { get; set; } = null!;
        public virtual DbSet<ChangeApprovalLog> ChangeApprovalLogs { get; set; } = null!;
        public virtual DbSet<ChangeApprover> ChangeApprovers { get; set; } = null!;
        public virtual DbSet<ChangeAttachment> ChangeAttachments { get; set; } = null!;
        public virtual DbSet<ChangeManagement> ChangeManagements { get; set; } = null!;
        public virtual DbSet<ChangeStatus> ChangeStatuses { get; set; } = null!;
        public virtual DbSet<ChangeType> ChangeTypes { get; set; } = null!;
        public virtual DbSet<Classification> Classifications { get; set; } = null!;
        public virtual DbSet<Complaint> Complaints { get; set; } = null!;
        public virtual DbSet<CustomerClaimStatus> CustomerClaimStatuses { get; set; } = null!;
        public virtual DbSet<CustomerComplaint> CustomerComplaints { get; set; } = null!;
        public virtual DbSet<CustomerServiceComplaint> CustomerServiceComplaints { get; set; } = null!;
        public virtual DbSet<CustomerServiceProblem> CustomerServiceProblems { get; set; } = null!;
        public virtual DbSet<Delivery> Deliveries { get; set; } = null!;
        public virtual DbSet<InternalProcess> InternalProcesses { get; set; } = null!;
        public virtual DbSet<Kpimonitering> Kpimoniterings { get; set; } = null!;
        public virtual DbSet<NonConformanceType> NonConformanceTypes { get; set; } = null!;
        public virtual DbSet<NonConformingMaterialComplaint> NonConformingMaterialComplaints { get; set; } = null!;
        public virtual DbSet<NonStandardInquiry> NonStandardInquiries { get; set; } = null!;
        public virtual DbSet<NonStandardInquiryMillCapability> NonStandardInquiryMillCapabilities { get; set; } = null!;
        public virtual DbSet<NonStandardInquiryProduct> NonStandardInquiryProducts { get; set; } = null!;
        public virtual DbSet<NonStandardInquiryProductMillCapability> NonStandardInquiryProductMillCapabilities { get; set; } = null!;
        public virtual DbSet<NonStandardInquiryProductPurchasing> NonStandardInquiryProductPurchasings { get; set; } = null!;
        public virtual DbSet<NonStandardInquiryPurchasing> NonStandardInquiryPurchasings { get; set; } = null!;
        public virtual DbSet<Paperwork> Paperworks { get; set; } = null!;
        public virtual DbSet<ProductQuantity> ProductQuantities { get; set; } = null!;
        public virtual DbSet<ProductType> ProductTypes { get; set; } = null!;
        public virtual DbSet<QualityAlert> QualityAlerts { get; set; } = null!;
        public virtual DbSet<ReasonCode> ReasonCodes { get; set; } = null!;
        public virtual DbSet<RiskApproval> RiskApprovals { get; set; } = null!;
        public virtual DbSet<RiskAssessment> RiskAssessments { get; set; } = null!;
        public virtual DbSet<RiskAttachment> RiskAttachments { get; set; } = null!;
        public virtual DbSet<RiskDepartment> RiskDepartments { get; set; } = null!;
        public virtual DbSet<RiskStatus> RiskStatuses { get; set; } = null!;
        public virtual DbSet<RiskType> RiskTypes { get; set; } = null!;
        public virtual DbSet<SAPInvoiceDetail> SapinvoiceDetails { get; set; } = null!;
        public virtual DbSet<SupplierComplaint> SupplierComplaints { get; set; } = null!;
        public virtual DbSet<TypeOfChange> TypeOfChanges { get; set; } = null!;
        public virtual DbSet<VwActionForm> VwActionForms { get; set; } = null!;
        public virtual DbSet<VwChangeManagement> VwChangeManagements { get; set; } = null!;
        public virtual DbSet<VwCustomerComplaint> VwCustomerComplaints { get; set; } = null!;
        public virtual DbSet<VwCustomerServiceComplaint> VwCustomerServiceComplaints { get; set; } = null!;
        public virtual DbSet<VwKpimonitering> VwKpimoniterings { get; set; } = null!;
        public virtual DbSet<VwNonConformingMaterialComplaint> VwNonConformingMaterialComplaints { get; set; } = null!;
        public virtual DbSet<VwNonStandardInquiry> VwNonStandardInquiries { get; set; } = null!;
        public virtual DbSet<VwQualityAlert> VwQualityAlerts { get; set; } = null!;
        public virtual DbSet<VwRiskAssessment> VwRiskAssessments { get; set; } = null!;
        public virtual DbSet<VwSupplierComplaint> VwSupplierComplaints { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=JMCWEBDEV\\DEV;Initial Catalog=AtlasQM;User ID=WebsiteUser;Password=p@ssw0rd;MultipleActiveResultSets=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AlertAttachment>(entity =>
            {
                entity.HasOne(d => d.Alert)
                    .WithMany(p => p.AlertAttachments)
                    .HasForeignKey(d => d.AlertID)
                    .HasConstraintName("FK_AlertAttachment_QualityAlert");
            });

            modelBuilder.Entity<Attachment>(entity =>
            {
                entity.HasOne(d => d.ActionForm)
                    .WithMany(p => p.Attachments)
                    .HasForeignKey(d => d.ActionFormID)
                    .HasConstraintName("FK_Attachment_ActionForm");

                entity.HasOne(d => d.Complaint)
                    .WithMany(p => p.Attachments)
                    .HasForeignKey(d => d.ComplaintID)
                    .HasConstraintName("FK_Attachment_Complaint");
            });

            modelBuilder.Entity<Complaint>(entity =>
            {
                entity.HasOne(d => d.Classification)
                    .WithMany(p => p.Complaints)
                    .HasForeignKey(d => d.ClassificationID)
                    .HasConstraintName("FK_Complaint_Classification");

                entity.HasOne(d => d.NonConformanceType)
                    .WithMany(p => p.Complaints)
                    .HasForeignKey(d => d.NonConformanceTypeID)
                    .HasConstraintName("FK_Complaint_NonConformanceType");

                entity.HasOne(d => d.ReasonCode)
                    .WithMany(p => p.Complaints)
                    .HasForeignKey(d => d.ReasonCodeID)
                    .HasConstraintName("FK_Complaint_ReasonCode");
            });

            modelBuilder.Entity<CustomerComplaint>(entity =>
            {
                entity.Property(e => e.CustomerComplaintID).ValueGeneratedNever();

                entity.HasOne(d => d.CustomerComplaintNavigation)
                    .WithOne(p => p.CustomerComplaint)
                    .HasForeignKey<CustomerComplaint>(d => d.CustomerComplaintID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerComplaint_Complaint");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.CustomerComplaints)
                    .HasForeignKey(d => d.StatusID)
                    .HasConstraintName("FK_CustomerComplaint_CustomerClaimStatus");
            });

            modelBuilder.Entity<CustomerServiceComplaint>(entity =>
            {
                entity.Property(e => e.CustomerServiceComplaintId).ValueGeneratedNever();

                entity.HasOne(d => d.Billing)
                    .WithMany(p => p.CustomerServiceComplaints)
                    .HasForeignKey(d => d.BillingId)
                    .HasConstraintName("FK_CustomerServiceComplaint_BillingPrice");

                entity.HasOne(d => d.CustomerServiceComplaintNavigation)
                    .WithOne(p => p.CustomerServiceComplaint)
                    .HasForeignKey<CustomerServiceComplaint>(d => d.CustomerServiceComplaintId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerServiceComplaint_Complaint");

                entity.HasOne(d => d.CustomerServiceProblem)
                    .WithMany(p => p.CustomerServiceComplaints)
                    .HasForeignKey(d => d.CustomerServiceProblemId)
                    .HasConstraintName("FK_CustomerServiceComplaint_CustomerServiceProblem");

                entity.HasOne(d => d.Delivery)
                    .WithMany(p => p.CustomerServiceComplaints)
                    .HasForeignKey(d => d.DeliveryId)
                    .HasConstraintName("FK_CustomerServiceComplaint_DeliveryMissed");

                entity.HasOne(d => d.IncorrectPaperwork)
                    .WithMany(p => p.CustomerServiceComplaintIncorrectPaperworks)
                    .HasForeignKey(d => d.IncorrectPaperworkId)
                    .HasConstraintName("FK_CustomerServiceComplaint_Paperwork1");

                entity.HasOne(d => d.MissingPaperwork)
                    .WithMany(p => p.CustomerServiceComplaintMissingPaperworks)
                    .HasForeignKey(d => d.MissingPaperworkId)
                    .HasConstraintName("FK_CustomerServiceComplaint_Paperwork");

                entity.HasOne(d => d.ProductQuantity)
                    .WithMany(p => p.CustomerServiceComplaints)
                    .HasForeignKey(d => d.ProductQuantityId)
                    .HasConstraintName("FK_CustomerServiceComplaint_ProductQuantity");
            });

            modelBuilder.Entity<NonConformingMaterialComplaint>(entity =>
            {
                entity.Property(e => e.NonConformingMaterialComplaintID).ValueGeneratedNever();

                entity.HasOne(d => d.NonConformingMaterialComplaintNavigation)
                    .WithOne(p => p.NonConformingMaterialComplaint)
                    .HasForeignKey<NonConformingMaterialComplaint>(d => d.NonConformingMaterialComplaintID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NonConformingMaterialComplaint_Complaint");
            });

            modelBuilder.Entity<NonStandardInquiryMillCapability>(entity =>
            {
                entity.HasOne(d => d.NonStandardInquiry)
                    .WithMany(p => p.NonStandardInquiryMillCapabilities)
                    .HasForeignKey(d => d.NonStandardInquiryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NonStandardInquiryMillCapabilities_NonStandardInquiry");
            });

            modelBuilder.Entity<NonStandardInquiryProduct>(entity =>
            {
                entity.HasOne(d => d.NonStandardInquiry)
                    .WithMany(p => p.NonStandardInquiryProducts)
                    .HasForeignKey(d => d.NonStandardInquiryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NonStandardInquiryProduct_NonStandardInquiry");
            });

            modelBuilder.Entity<NonStandardInquiryProductMillCapability>(entity =>
            {
                entity.HasOne(d => d.NonStandardInquiryProduct)
                    .WithMany(p => p.NonStandardInquiryProductMillCapabilities)
                    .HasForeignKey(d => d.NonStandardInquiryProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NonStandardInquiryProductMillCapabilities_NonStandardInquiryProduct");
            });

            modelBuilder.Entity<NonStandardInquiryProductPurchasing>(entity =>
            {
                entity.HasOne(d => d.NonStandardInquiryProduct)
                    .WithMany(p => p.NonStandardInquiryProductPurchasings)
                    .HasForeignKey(d => d.NonStandardInquiryProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NonStandardInquiryProductPurchasing_NonStandardInquiryProduct");
            });

            modelBuilder.Entity<NonStandardInquiryPurchasing>(entity =>
            {
                entity.HasOne(d => d.NonStandardInquiry)
                    .WithMany(p => p.NonStandardInquiryPurchasings)
                    .HasForeignKey(d => d.NonStandardInquiryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NonStandardInquiryPurchasing_NonStandardInquiry");
            });

            modelBuilder.Entity<RiskDepartment>(entity =>
            {
                entity.Property(e => e.Name).IsFixedLength();
            });

            modelBuilder.Entity<RiskStatus>(entity =>
            {
                entity.Property(e => e.Name).IsFixedLength();
            });

            modelBuilder.Entity<RiskType>(entity =>
            {
                entity.Property(e => e.Name).IsFixedLength();
            });

            modelBuilder.Entity<SupplierComplaint>(entity =>
            {
                entity.Property(e => e.SupplierComplaintId).ValueGeneratedNever();

                entity.HasOne(d => d.SupplierComplaintNavigation)
                    .WithOne(p => p.SupplierComplaint)
                    .HasForeignKey<SupplierComplaint>(d => d.SupplierComplaintId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SupplierComplaint_Complaint");
            });

            modelBuilder.Entity<VwActionForm>(entity =>
            {
                entity.ToView("vwActionForm");
            });

            modelBuilder.Entity<VwChangeManagement>(entity =>
            {
                entity.ToView("vwChangeManagement");
            });

            modelBuilder.Entity<VwCustomerComplaint>(entity =>
            {
                entity.ToView("vwCustomerComplaint");

                entity.Property(e => e.SapcustomerId).IsFixedLength();
            });

            modelBuilder.Entity<VwCustomerServiceComplaint>(entity =>
            {
                entity.ToView("vwCustomerServiceComplaint");
            });

            modelBuilder.Entity<VwKpimonitering>(entity =>
            {
                entity.ToView("vwKPIMonitering");
            });

            modelBuilder.Entity<VwNonConformingMaterialComplaint>(entity =>
            {
                entity.ToView("vwNonConformingMaterialComplaint");
            });

            modelBuilder.Entity<VwNonStandardInquiry>(entity =>
            {
                entity.ToView("vwNonStandardInquiry");
            });

            modelBuilder.Entity<VwQualityAlert>(entity =>
            {
                entity.ToView("vwQualityAlert");
            });

            modelBuilder.Entity<VwRiskAssessment>(entity =>
            {
                entity.ToView("vwRiskAssessment");

                entity.Property(e => e.RiskDepartmentName).IsFixedLength();
            });

            modelBuilder.Entity<VwSupplierComplaint>(entity =>
            {
                entity.ToView("vwSupplierComplaint");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
