using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business
{
    [Table("ChangeApprovalLog")]
    public partial class ChangeApprovalLog
    {
        [Key]
        [Column("ChangeApprovalLogID")]
        public int ChangeApprovalLogId { get; set; }
        [Column("ApproverID")]
        public int? ApproverId { get; set; }
        public bool? Approve { get; set; }
        [StringLength(200)]
        [Unicode(false)]
        public string? Comments { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ActionDate { get; set; }
        [Column("ChangeManagementID")]
        public int? ChangeManagementId { get; set; }
    }
}
