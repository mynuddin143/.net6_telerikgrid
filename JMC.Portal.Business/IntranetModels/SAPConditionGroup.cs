using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("SAPConditionGroup")]
    [Index("Sapcode", Name = "IX_SAPConditionGroup", IsUnique = true)]
    public partial class SAPConditionGroup
    {
        public SAPConditionGroup()
        {
            SAPShipToFreightIndicatorSAPConditionGroups = new HashSet<SAPShipTo>();
            SAPShipToFuelSurchargeSAPConditionGroups = new HashSet<SAPShipTo>();
            SAPSoldToHomeMillSAPConditionGroups = new HashSet<SAPSoldTo>();
            SAPSoldToRegionSAPConditionGroups = new HashSet<SAPSoldTo>();
            SAPSoldToTierSAPConditionGroups = new HashSet<SAPSoldTo>();
        }

        [Key]
        [Column("SAPConditionGroupID")]
        public int SAPConditionGroupID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [Column("SAPCode")]
        [StringLength(50)]
        [Unicode(false)]
        public string SAPCode { get; set; } = null!;

        [InverseProperty("FreightIndicatorSAPConditionGroup")]
        public virtual ICollection<SAPShipTo> SAPShipToFreightIndicatorSAPConditionGroups { get; set; }
        [InverseProperty("FuelSurchargeSAPConditionGroup")]
        public virtual ICollection<SAPShipTo> SAPShipToFuelSurchargeSAPConditionGroups { get; set; }
        [InverseProperty("HomeMillSAPConditionGroup")]
        public virtual ICollection<SAPSoldTo> SAPSoldToHomeMillSAPConditionGroups { get; set; }
        [InverseProperty("RegionSAPConditionGroup")]
        public virtual ICollection<SAPSoldTo> SAPSoldToRegionSAPConditionGroups { get; set; }
        [InverseProperty("TierSAPConditionGroup")]
        public virtual ICollection<SAPSoldTo> SAPSoldToTierSAPConditionGroups { get; set; }
    }
}
