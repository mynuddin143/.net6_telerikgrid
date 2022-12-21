using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business
{
    [Table("QualityAlert")]
    public partial class QualityAlert
    {
        public QualityAlert()
        {
            AlertAttachments = new HashSet<AlertAttachment>();
        }

        [Key]
        [Column("AlertID")]
        public int AlertId { get; set; }
        [Column("CCRNumber")]
        [StringLength(50)]
        [Unicode(false)]
        public string Ccrnumber { get; set; } = null!;
        [Column("LocationID")]
        public int? LocationId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Date { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? TubeSize { get; set; }
        [StringLength(200)]
        [Unicode(false)]
        public string? Customer { get; set; }
        [Column("BLNumber")]
        [StringLength(50)]
        [Unicode(false)]
        public string? Blnumber { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? BatchNumber { get; set; }
        [StringLength(2000)]
        [Unicode(false)]
        public string? Issue { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? QuantityDefectivePieces { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? QuantityDefectiveWeight { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? AuthorizedBy { get; set; }

        [InverseProperty("Alert")]
        public virtual ICollection<AlertAttachment> AlertAttachments { get; set; }
    }
}
