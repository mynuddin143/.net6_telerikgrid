using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business
{
    [Keyless]
    public partial class VwKpimonitering
    {
        [Column("KPIMoniteringID")]
        public int KpimoniteringId { get; set; }
        public int? FiscalYear { get; set; }
        [Column("PlantID")]
        public int? PlantId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string LocationName { get; set; } = null!;
        [Column("DepartmentID")]
        public int? DepartmentId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Department { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Quarter { get; set; }
        public int? Month { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DueDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CompletedDate { get; set; }
        [StringLength(51)]
        [Unicode(false)]
        public string? AssignedTo { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Number { get; set; }
        [StringLength(2000)]
        [Unicode(false)]
        public string? Description { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? Target { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? ActualPerformace { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? Variance { get; set; }
        [StringLength(2000)]
        [Unicode(false)]
        public string? Investigation { get; set; }
        [StringLength(2000)]
        [Unicode(false)]
        public string? ActionsImplemented { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Date { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? MetricName { get; set; }
    }
}
