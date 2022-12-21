using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("SAPPricingCondition")]
    public partial class SAPPricingCondition
    {
        [Key]
        [Column("SAPPricingConditionID")]
        public int SAPPricingConditionID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [Column("SAPCode")]
        [StringLength(4)]
        [Unicode(false)]
        public string SAPCode { get; set; } = null!;
        [StringLength(5)]
        [Unicode(false)]
        public string Application { get; set; } = null!;
        public bool? Override { get; set; }
        public bool? Extra { get; set; }
    }
}
