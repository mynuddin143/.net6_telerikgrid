using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.JMCQualityManagement
{
    [Table("ClaimRequestDocument")]
    public partial class ClaimRequestDocument
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("ClaimRequestID")]
        public int ClaimRequestId { get; set; }
        [StringLength(255)]
        public string? DocumentName { get; set; }
    }
}
