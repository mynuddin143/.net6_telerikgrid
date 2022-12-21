using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.JMCQualityManagement
{
    public partial class Trial
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(50)]
        public string? TrialNumber { get; set; }
        [Column(TypeName = "date")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(50)]
        public string? EnteredBy { get; set; }
        [StringLength(250)]
        public string? Subject { get; set; }
        [Column("PlantID")]
        public int PlantId { get; set; }
        [StringLength(50)]
        public string? Material { get; set; }
        [StringLength(50)]
        public string? LocationOfMaterial { get; set; }
        [Column(TypeName = "date")]
        public DateTime? CurrentMaterialDate { get; set; }
        [StringLength(50)]
        public string? TrialLead { get; set; }
        [StringLength(250)]
        public string? TrialTeam { get; set; }
        [Column(TypeName = "text")]
        public string? Introduction { get; set; }
        [Column(TypeName = "text")]
        public string? Notes { get; set; }
        [StringLength(50)]
        public string? RunNumber { get; set; }
        [StringLength(50)]
        public string? HeatNumber { get; set; }
        [StringLength(50)]
        public string? LinkedTrial { get; set; }
        [Column(TypeName = "text")]
        public string? TrialResult { get; set; }
        public bool? TrialCompleted { get; set; }
        [Column(TypeName = "date")]
        public DateTime? TrialCompletedDate { get; set; }
    }
}
