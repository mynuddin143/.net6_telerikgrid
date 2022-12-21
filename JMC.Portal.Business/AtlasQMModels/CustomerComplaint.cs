using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business
{
    [Table("CustomerComplaint")]
    public partial class CustomerComplaint
    {
        [Key]
        [Column("CustomerComplaintID")]
        public int CustomerComplaintID { get; set; }
        [Column("PlantID")]
        public int? PlantID { get; set; }
        [Column("CustomerID")]
        public int? CustomerID { get; set; }
        [StringLength(250)]
        [Unicode(false)]
        public string? Address { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Contact { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? PhoneNumber { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? FaxNumber { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? PartNumber { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? TrackingNumber { get; set; }
        [StringLength(5000)]
        [Unicode(false)]
        public string? BillOfLading { get; set; }
        [Column("CustomerPONumber")]
        [StringLength(50)]
        [Unicode(false)]
        public string? CustomerPONumber { get; set; }
        [Column("AtlasSONumber")]
        [StringLength(5000)]
        [Unicode(false)]
        public string? AtlasSONumber { get; set; }
        [Column("AtlasPONumber")]
        [StringLength(5000)]
        [Unicode(false)]
        public string? AtlasPONumber { get; set; }
        [StringLength(5000)]
        [Unicode(false)]
        public string? TubeSize { get; set; }
        [StringLength(5000)]
        [Unicode(false)]
        public string? Gauge { get; set; }
        [StringLength(5000)]
        [Unicode(false)]
        public string? Length { get; set; }
        [Column("ShippedFromPlantID")]
        public int? ShippedFromPlantID { get; set; }
        public bool SupplierRelated { get; set; }
        public bool MaterialToBeReturned { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? ComplaintFieldedBy { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? SupplierComplaintNumber { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? ReturnApprovedBy { get; set; }
        [StringLength(2000)]
        [Unicode(false)]
        public string? Condition { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? CheckedBy { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? InspectionDate { get; set; }
        [Column(TypeName = "money")]
        public decimal? TruckingCharge { get; set; }
        [Column(TypeName = "money")]
        public decimal? CreditToCustomer { get; set; }
        [Column(TypeName = "money")]
        public decimal? CostOfQuality { get; set; }
        [StringLength(5000)]
        [Unicode(false)]
        public string? InvoiceNumber { get; set; }
        [Column("ProductTypeID")]
        public int? ProductTypeID { get; set; }
        [Column("StatusID")]
        public int? StatusID { get; set; }
        [StringLength(3)]
        [Unicode(false)]
        public string? Source { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? CreatedBy { get; set; }

        [ForeignKey("CustomerComplaintID")]
        [InverseProperty("CustomerComplaint")]
        public virtual Complaint CustomerComplaintNavigation { get; set; } = null!;
        [ForeignKey("StatusID")]
        [InverseProperty("CustomerComplaints")]
        public virtual CustomerClaimStatus? Status { get; set; }

        public virtual CustomerComplaint Complaint { get; set; } = null!;

        public int ComplaintID { get; set; }
        public string? Number { get; set; }
        public string? Description { get; set; }
        public DateTime? ChargeDate { get; set; }
        public string? ActionApprovedBy { get; set; }
        public DateTime? ActionDate { get; set; }
        public string? BatchNumber { get; set; }
        public DateTime Date { get; set; }
        public string? HeatNumber { get; set; }
    }
}
