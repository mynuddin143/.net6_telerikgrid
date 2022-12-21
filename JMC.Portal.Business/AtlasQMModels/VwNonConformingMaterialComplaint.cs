using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business
{
    [Keyless]
    public partial class VwNonConformingMaterialComplaint
    {
        [Column("NonConformingMaterialComplaintID")]
        public int NonConformingMaterialComplaintID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Number { get; set; }
        [Column("LocationID")]
        public int LocationID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string LocationName { get; set; } = null!;
        [StringLength(2000)]
        public string? TubeSize { get; set; }
        [StringLength(2000)]
        public string? Gauge { get; set; }
        [StringLength(2000)]
        public string? Length { get; set; }
        [StringLength(2000)]
        public string? HeatNumber { get; set; }
        [Column("ReasonCodeID")]
        public int? ReasonCodeID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? ReasonCodeName { get; set; }
        [StringLength(2000)]
        [Unicode(false)]
        public string? Description { get; set; }
        [StringLength(2000)]
        public string? BatchNumber { get; set; }
    }
}
