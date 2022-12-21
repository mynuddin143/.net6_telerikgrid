using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business
{
    [Table("NonStandardInquiry")]
    public partial class NonStandardInquiry
    {
        public NonStandardInquiry()
        {
            NonStandardInquiryMillCapabilities = new HashSet<NonStandardInquiryMillCapability>();
            NonStandardInquiryProducts = new HashSet<NonStandardInquiryProduct>();
            NonStandardInquiryPurchasings = new HashSet<NonStandardInquiryPurchasing>();
        }

        [Key]
        [Column("NonStandardInquiryID")]
        public int NonStandardInquiryId { get; set; }
        [Column("UserID")]
        public int UserId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; }
        [Column("InsideSalesRepID")]
        public int? InsideSalesRepId { get; set; }
        [Column("OutsideSalesRepID")]
        public int? OutsideSalesRepId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? QuoteDueDate { get; set; }
        [StringLength(500)]
        [Unicode(false)]
        public string? EndUse { get; set; }
        [Column("CustomerID")]
        public int? CustomerId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? CustomerContact { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? PhoneNumber { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? FaxNumber { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Email { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Address { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? ShippingAddress { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? QualityReviewedBy { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? QualityDate { get; set; }
        [Column("QualityNA")]
        public bool QualityNa { get; set; }
        public bool QualityComplete { get; set; }
        [StringLength(2000)]
        [Unicode(false)]
        public string? QualityComments { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? AssessmentReviewedBy { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? AssessmentDate { get; set; }
        public bool AssessmentQuoteIssued { get; set; }
        public bool AssessmentRegret { get; set; }
        [StringLength(2000)]
        [Unicode(false)]
        public string? AssessmentComments { get; set; }
        [StringLength(2000)]
        [Unicode(false)]
        public string? AdditionalInformation { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? PotentialCustomer { get; set; }

        [InverseProperty("NonStandardInquiry")]
        public virtual ICollection<NonStandardInquiryMillCapability> NonStandardInquiryMillCapabilities { get; set; }
        [InverseProperty("NonStandardInquiry")]
        public virtual ICollection<NonStandardInquiryProduct> NonStandardInquiryProducts { get; set; }
        [InverseProperty("NonStandardInquiry")]
        public virtual ICollection<NonStandardInquiryPurchasing> NonStandardInquiryPurchasings { get; set; }
    }
}
