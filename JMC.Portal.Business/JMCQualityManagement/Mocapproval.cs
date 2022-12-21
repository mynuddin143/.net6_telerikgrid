using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.JMCQualityManagement
{
    [Table("MOCApprovals")]
    public partial class Mocapproval
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("MOCID")]
        public int Mocid { get; set; }
        [Column("ApproverID")]
        public int? ApproverId { get; set; }
        [Column(TypeName = "text")]
        public string? Comments { get; set; }
        public bool? Approved { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        [ForeignKey("ApproverId")]
        [InverseProperty("Mocapprovals")]
        public virtual Mocapprover? Approver { get; set; }
        [ForeignKey("Mocid")]
        [InverseProperty("Mocapprovals")]
        public virtual ManagementOfChange Moc { get; set; } = null!;
    }
}
