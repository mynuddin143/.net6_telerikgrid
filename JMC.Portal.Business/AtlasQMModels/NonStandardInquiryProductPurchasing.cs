using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business
{
    [Table("NonStandardInquiryProductPurchasing")]
    public partial class NonStandardInquiryProductPurchasing
    {
        [Key]
        [Column("NonStandardInquiryProductPurchasingID")]
        public int NonStandardInquiryProductPurchasingId { get; set; }
        [Column("NonStandardInquiryProductID")]
        public int NonStandardInquiryProductId { get; set; }
        [Column("PlantID")]
        public int PlantId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? GradeAvailable { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? MinimumRequirement { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? GradeExtraCost { get; set; }

        [ForeignKey("NonStandardInquiryProductId")]
        [InverseProperty("NonStandardInquiryProductPurchasings")]
        public virtual NonStandardInquiryProduct NonStandardInquiryProduct { get; set; } = null!;
    }
}
