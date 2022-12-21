using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business
{
    [Table("ChangeAttachment")]
    public partial class ChangeAttachment
    {
        [Key]
        [Column("ChangeAttachmentID")]
        public int ChangeAttachmentId { get; set; }
        [StringLength(250)]
        [Unicode(false)]
        public string? Name { get; set; }
        [Column("ChangeManagementID")]
        public int? ChangeManagementId { get; set; }
        [StringLength(500)]
        [Unicode(false)]
        public string? Path { get; set; }
        [Column("UserID")]
        public int? UserId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Date { get; set; }
    }
}
