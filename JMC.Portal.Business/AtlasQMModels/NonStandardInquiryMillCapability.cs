using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business
{
    public partial class NonStandardInquiryMillCapability
    {
        [Key]
        [Column("NonStandardInquiryMillCapabilitiesID")]
        public int NonStandardInquiryMillCapabilitiesId { get; set; }
        [Column("NonStandardInquiryID")]
        public int NonStandardInquiryId { get; set; }
        [Column("PlantID")]
        public int PlantId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? ReviewedBy { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Date { get; set; }
        [Column("NA")]
        public bool Na { get; set; }
        public bool Complete { get; set; }
        [StringLength(2000)]
        [Unicode(false)]
        public string? Comments { get; set; }

        [ForeignKey("NonStandardInquiryId")]
        [InverseProperty("NonStandardInquiryMillCapabilities")]
        public virtual NonStandardInquiry NonStandardInquiry { get; set; } = null!;
    }
}
