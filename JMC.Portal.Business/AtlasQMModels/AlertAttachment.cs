using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business
{
    [Table("AlertAttachment")]
    public partial class AlertAttachment
    {
        [Key]
        [Column("AttachmentID")]
        public int AttachmentID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [Column("AlertID")]
        public int? AlertID { get; set; }
        [StringLength(500)]
        [Unicode(false)]
        public string Path { get; set; } = null!;
        [Column("UserID")]
        public int UserID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; }

        [ForeignKey("AlertID")]
        [InverseProperty("AlertAttachments")]
        public virtual QualityAlert? Alert { get; set; }
    }
}
