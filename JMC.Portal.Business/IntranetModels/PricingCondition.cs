using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("PricingCondition")]
    public partial class PricingCondition
    {
        [Key]
        [Column("PricingConditionID")]
        public int PricingConditionId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [Column("SAPCode")]
        [StringLength(4)]
        [Unicode(false)]
        public string Sapcode { get; set; } = null!;
        [StringLength(5)]
        [Unicode(false)]
        public string Application { get; set; } = null!;
        [Column("SAPCode2")]
        [StringLength(4)]
        [Unicode(false)]
        public string Sapcode2 { get; set; } = null!;
    }
}
