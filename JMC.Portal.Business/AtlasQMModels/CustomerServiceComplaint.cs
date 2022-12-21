using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business
{
    [Table("CustomerServiceComplaint")]
    public partial class CustomerServiceComplaint
    {
        [Key]
        [Column("CustomerServiceComplaintID")]
        public int CustomerServiceComplaintId { get; set; }
        [Column("CustomerID")]
        public int CustomerId { get; set; }
        [Column("PlantID")]
        public int PlantId { get; set; }
        [Column("ProductQuantityID")]
        public int? ProductQuantityId { get; set; }
        [Column("DeliveryID")]
        public int? DeliveryId { get; set; }
        [Column("MissingPaperworkID")]
        public int? MissingPaperworkId { get; set; }
        [Column("IncorrectPaperworkID")]
        public int? IncorrectPaperworkId { get; set; }
        [Column("BillingID")]
        public int? BillingId { get; set; }
        [Column("CustomerServiceProblemID")]
        public int? CustomerServiceProblemId { get; set; }
        [Column("InsideSalesRepID")]
        public int InsideSalesRepId { get; set; }

        [ForeignKey("BillingId")]
        [InverseProperty("CustomerServiceComplaints")]
        public virtual Billing? Billing { get; set; }
        [ForeignKey("CustomerServiceComplaintId")]
        [InverseProperty("CustomerServiceComplaint")]
        public virtual Complaint CustomerServiceComplaintNavigation { get; set; } = null!;
        [ForeignKey("CustomerServiceProblemId")]
        [InverseProperty("CustomerServiceComplaints")]
        public virtual CustomerServiceProblem? CustomerServiceProblem { get; set; }
        [ForeignKey("DeliveryId")]
        [InverseProperty("CustomerServiceComplaints")]
        public virtual Delivery? Delivery { get; set; }
        [ForeignKey("IncorrectPaperworkId")]
        [InverseProperty("CustomerServiceComplaintIncorrectPaperworks")]
        public virtual Paperwork? IncorrectPaperwork { get; set; }
        [ForeignKey("MissingPaperworkId")]
        [InverseProperty("CustomerServiceComplaintMissingPaperworks")]
        public virtual Paperwork? MissingPaperwork { get; set; }
        [ForeignKey("ProductQuantityId")]
        [InverseProperty("CustomerServiceComplaints")]
        public virtual ProductQuantity? ProductQuantity { get; set; }
    }
}
