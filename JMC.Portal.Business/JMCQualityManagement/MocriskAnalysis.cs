using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.JMCQualityManagement
{
    [Table("MOCRiskAnalysis")]
    public partial class MocriskAnalysis
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("MOCID")]
        public int Mocid { get; set; }
        [StringLength(50)]
        public string Risk { get; set; } = null!;
        [StringLength(250)]
        public string? Severity { get; set; }
        [StringLength(250)]
        public string? Occurrence { get; set; }
        [StringLength(250)]
        public string? Detection { get; set; }
        [StringLength(250)]
        public string? RiskNumber { get; set; }
        [StringLength(250)]
        public string? CurrentControls { get; set; }
        [Column(TypeName = "text")]
        public string? Adequate { get; set; }
        [Column(TypeName = "text")]
        public string? ContingencyPlan { get; set; }
        [StringLength(250)]
        public string? InternalCommunication { get; set; }
        [StringLength(250)]
        public string? ExternalCommunication { get; set; }
        [StringLength(50)]
        public string? ResponsiblePerson { get; set; }

        [ForeignKey("Mocid")]
        [InverseProperty("MocriskAnalyses")]
        public virtual ManagementOfChange Moc { get; set; } = null!;
    }
}
