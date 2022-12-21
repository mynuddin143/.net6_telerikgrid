using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("SAPSoldTo")]
    public partial class SAPSoldTo
    {
        public SAPSoldTo()
        {
            RandomLengthSAPSoldTos = new HashSet<RandomLengthSAPSoldTo>();
            SAPSoldToSAPShipTos = new HashSet<SAPSoldToSAPShipTo>();
            ScrapSAPSoldTos = new HashSet<ScrapSAPSoldTo>();
        }

        [Key]
        [Column("SAPSoldToID")]
        public int SAPSoldToID { get; set; }
        [Column("DefaultSAPShipToID")]
        public int? DefaultSAPShipToID { get; set; }
        public bool CustomerSpecificPricing { get; set; }
        [Column("RegionSAPConditionGroupID")]
        public int? RegionSAPConditionGroupID { get; set; }
        [Column("TierSAPConditionGroupID")]
        public int? TierSAPConditionGroupId { get; set; }
        [Column("HomeMillSAPConditionGroupID")]
        public int? HomeMillSAPConditionGroupID { get; set; }
        [Column("SAPCustomerGroupID")]
        public int? SAPCustomerGroupID { get; set; }
        [Column("SAPSalesGroupID")]
        public int? SAPSalesGroupID { get; set; }
        [StringLength(2000)]
        [Unicode(false)]
        public string? PricingNotes { get; set; }
        [StringLength(1)]
        [Unicode(false)]
        public string? PricingProcedure { get; set; }

        [ForeignKey("HomeMillSAPConditionGroupID")]
        [InverseProperty("SAPSoldToHomeMillSAPConditionGroups")]
        public virtual SAPConditionGroup? HomeMillSAPConditionGroup { get; set; }
        [ForeignKey("RegionSAPConditionGroupID")]
        [InverseProperty("SAPSoldToRegionSAPConditionGroups")]
        public virtual SAPConditionGroup? RegionSAPConditionGroup { get; set; }
        [ForeignKey("SAPCustomerGroupID")]
        [InverseProperty("SAPSoldTos")]
        public virtual SAPCustomerGroup? SAPCustomerGroup { get; set; }
        [ForeignKey("SAPSalesGroupID")]
        [InverseProperty("SAPSoldTos")]
        public virtual SAPSalesGroup? SAPSalesGroup { get; set; }
        [ForeignKey("SAPSoldToID")]
        [InverseProperty("SAPSoldTo")]
        public virtual SAPShipTo SAPSoldToNavigation { get; set; } = null!;
        [ForeignKey("TierSAPConditionGroupID")]
        [InverseProperty("SAPSoldToTierSAPConditionGroups")]
        public virtual SAPConditionGroup? TierSAPConditionGroup { get; set; }
        [InverseProperty("SAPSoldTo")]
        public virtual ICollection<RandomLengthSAPSoldTo> RandomLengthSAPSoldTos { get; set; }
        [InverseProperty("SAPSoldTo")]
        public virtual ICollection<SAPSoldToSAPShipTo> SAPSoldToSAPShipTos { get; set; }
        [InverseProperty("SAPSoldTo")]
        public virtual ICollection<ScrapSAPSoldTo> ScrapSAPSoldTos { get; set; }
    }
}
