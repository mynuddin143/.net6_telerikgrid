using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.JMCQualityManagement
{
    [Table("ManagementOfChange")]
    public partial class ManagementOfChange
    {
        public ManagementOfChange()
        {
            Mocapprovals = new HashSet<Mocapproval>();
            Mocdocuments = new HashSet<Mocdocument>();
            MocriskAnalyses = new HashSet<MocriskAnalysis>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("MOCRef")]
        [StringLength(50)]
        public string Mocref { get; set; } = null!;
        [Column("PlantID")]
        public int PlantId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? RequestedDate { get; set; }
        [StringLength(100)]
        public string RequestedBy { get; set; } = null!;
        [StringLength(200)]
        public string? EnteredByEmail { get; set; }
        [Column(TypeName = "text")]
        public string? ReasonForChange { get; set; }
        [Column(TypeName = "text")]
        public string? ChangeDescription { get; set; }
        [Column(TypeName = "text")]
        public string? PossibleRisks { get; set; }
        [Column(TypeName = "text")]
        public string? ActionToMinimizeRisk { get; set; }
        [StringLength(250)]
        public string? PersonnelTobeTrained { get; set; }
        public bool? MinimizeRiskImplement { get; set; }
        public bool? DocumentRevised { get; set; }
        [StringLength(10)]
        public string? DocumentRevisedc { get; set; }
        public bool? PersonnelNotified { get; set; }
        [StringLength(10)]
        public string? CustomerNotifiedc { get; set; }
        public bool? CustomerNotified { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsCompleted { get; set; }
        [Column(TypeName = "text")]
        public string? VerificationActivity { get; set; }
        [StringLength(250)]
        public string? VerificationActivityVerifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? VerificationActivityVerifiedDate { get; set; }
        [Column("CARStatus")]
        [StringLength(10)]
        public string? Carstatus { get; set; }

        [InverseProperty("Moc")]
        public virtual ICollection<Mocapproval> Mocapprovals { get; set; }
        [InverseProperty("Moc")]
        public virtual ICollection<Mocdocument> Mocdocuments { get; set; }
        [InverseProperty("Moc")]
        public virtual ICollection<MocriskAnalysis> MocriskAnalyses { get; set; }
    }
}
