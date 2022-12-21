using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business
{
    [Table("RiskAttachment")]
    public partial class RiskAttachment
    {
        [Key]
        [Column("RiskAttachmentID")]
        public int RiskAttachmentId { get; set; }
        [StringLength(250)]
        [Unicode(false)]
        public string? Name { get; set; }
        [Column("RiskAssessmentID")]
        public int? RiskAssessmentId { get; set; }
        [StringLength(500)]
        [Unicode(false)]
        public string? Path { get; set; }
        [Column("UserID")]
        public int? UserId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Date { get; set; }
    }
}
