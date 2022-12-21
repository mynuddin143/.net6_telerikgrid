using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business
{
    [Table("Attachment")]
    public partial class Attachment
    {
        [Key]
        [Column("AttachmentID")]
        public int AttachmentID { get; set; }
        [StringLength(250)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [Column("ComplaintID")]
        public int? ComplaintID { get; set; }
        [Column("ActionFormID")]
        public int? ActionFormID { get; set; }
        [StringLength(500)]
        [Unicode(false)]
        public string Path { get; set; } = null!;
        [Column("UserID")]
        public int UserID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? UploadedBy { get; set; }

        [ForeignKey("ActionFormID")]
        [InverseProperty("Attachments")]
        public virtual ActionForm? ActionForm { get; set; }
        [ForeignKey("ComplaintID")]
        [InverseProperty("Attachments")]
        public virtual Complaint? Complaint { get; set; }
    }
}
