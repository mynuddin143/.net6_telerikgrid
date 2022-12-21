using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business
{
    [Table("NonStandardInquiryProduct")]
    public partial class NonStandardInquiryProduct
    {
        public NonStandardInquiryProduct()
        {
            NonStandardInquiryProductMillCapabilities = new HashSet<NonStandardInquiryProductMillCapability>();
            NonStandardInquiryProductPurchasings = new HashSet<NonStandardInquiryProductPurchasing>();
        }

        [Key]
        [Column("NonStandardInquiryProductID")]
        public int NonStandardInquiryProductId { get; set; }
        [Column("NonStandardInquiryID")]
        public int NonStandardInquiryId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Size { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Gauge { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Tons { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Length { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Tolerance { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? TubeSpecification { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? SteelGrade { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Yield { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Tensile { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Elongation { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? DimensionalRequirements { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? OtherRequirements { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? CharpyTemp { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? CharpyRate { get; set; }
        [Column("IDFlashRemoval")]
        [StringLength(50)]
        [Unicode(false)]
        public string? IdflashRemoval { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? NonDestructiveTesting { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? OutsideProcessing { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? SpecialShippingRequired { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? NewSpecificationRequired { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? TrialRunRequired { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? MeetInspectionTestingRequired { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? MeetMechanicalProperties { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? RecommendedPlant { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Quote { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Price { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? RequireOrderBy { get; set; }

        [ForeignKey("NonStandardInquiryId")]
        [InverseProperty("NonStandardInquiryProducts")]
        public virtual NonStandardInquiry NonStandardInquiry { get; set; } = null!;
        [InverseProperty("NonStandardInquiryProduct")]
        public virtual ICollection<NonStandardInquiryProductMillCapability> NonStandardInquiryProductMillCapabilities { get; set; }
        [InverseProperty("NonStandardInquiryProduct")]
        public virtual ICollection<NonStandardInquiryProductPurchasing> NonStandardInquiryProductPurchasings { get; set; }
    }
}
