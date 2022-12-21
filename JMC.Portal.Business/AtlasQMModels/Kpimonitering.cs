using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business
{
    [Table("KPIMonitering")]
    public partial class Kpimonitering
    {
        [Column("PlantID")]
        public int? PlantID { get; set; }
        public int? FiscalYear { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Quarter { get; set; }
        [Column("DepartmentID")]
        public int? DepartmentID { get; set; }
        [StringLength(2000)]
        [Unicode(false)]
        public string? Description { get; set; }
        public int? Month { get; set; }
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
        [StringLength(50)]
        [Unicode(false)]
        public string? AssignedTo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DueDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CompletedDate { get; set; }
        [Key]
        [Column("KPIMoniteringID")]
        public int KpimoniteringID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Number { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Date { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? MetricName { get; set; }
    }
}
