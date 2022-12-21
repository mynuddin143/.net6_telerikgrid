using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business
{
    public partial class NonStandardInquiryProductMillCapability
    {
        [Key]
        [Column("NonStandardInquiryProductMillCapabilitiesID")]
        public int NonStandardInquiryProductMillCapabilitiesId { get; set; }
        [Column("NonStandardInquiryProductID")]
        public int NonStandardInquiryProductId { get; set; }
        [Column("PlantID")]
        public int PlantId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? CanMeetRequirements { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? AvailableCapacity { get; set; }

        [ForeignKey("NonStandardInquiryProductId")]
        [InverseProperty("NonStandardInquiryProductMillCapabilities")]
        public virtual NonStandardInquiryProduct NonStandardInquiryProduct { get; set; } = null!;
    }
}
