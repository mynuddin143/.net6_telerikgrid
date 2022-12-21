using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business
{
    [Keyless]
    public partial class VwRiskAssessment
    {
        [Column("RiskAssessmentID")]
        public int RiskAssessmentID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Number { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Plant { get; set; } = null!;
        [Column("RiskDepartmentID")]
        public int? RiskDepartmentID { get; set; }
        [StringLength(50)]
        public string? RiskDepartmentName { get; set; }
        [StringLength(2000)]
        [Unicode(false)]
        public string? EventDetails { get; set; }
    }
}
