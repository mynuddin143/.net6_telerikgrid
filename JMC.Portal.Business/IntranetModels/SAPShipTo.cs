using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("SAPShipTo")]
    [Index("Number", Name = "IX_SAPShipTo", IsUnique = true)]
    public partial class SAPShipTo
    {
        public SAPShipTo()
        {
            SAPSoldToSAPShipTos = new HashSet<SAPSoldToSAPShipTo>();
        }

        [Key]
        [Column("SAPShipToID")]
        public int SAPShipToID { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string Number { get; set; } = null!;
        [StringLength(30)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [StringLength(30)]
        [Unicode(false)]
        public string Address { get; set; } = null!;
        [Column("CityID")]
        public int CityID { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string PostalCode { get; set; } = null!;
        [StringLength(16)]
        [Unicode(false)]
        public string Phone { get; set; } = null!;
        [StringLength(31)]
        [Unicode(false)]
        public string Fax { get; set; } = null!;
        [StringLength(28)]
        [Unicode(false)]
        public string IncoTerms2 { get; set; } = null!;
        [StringLength(50)]
        [Unicode(false)]
        public string Currency { get; set; } = null!;
        [Column("FreightIndicatorSAPConditionGroupID")]
        public int? FreightIndicatorSAPConditionGroupID { get; set; }
        [Column("FuelSurchargeSAPConditionGroupID")]
        public int? FuelSurchargeSAPConditionGroupID { get; set; }
        public bool Active { get; set; }

        [ForeignKey("CityID")]
        [InverseProperty("SAPShipTos")]
        public virtual City City { get; set; } = null!;
        [ForeignKey("FreightIndicatorSAPConditionGroupID")]
        [InverseProperty("SAPShipToFreightIndicatorSAPConditionGroups")]
        public virtual SAPConditionGroup? FreightIndicatorSAPConditionGroup { get; set; }
        [ForeignKey("FuelSurchargeSAPConditionGroupId")]
        [InverseProperty("SAPShipToFuelSurchargeSAPConditionGroups")]
        public virtual SAPConditionGroup? FuelSurchargeSAPConditionGroup { get; set; }
        [InverseProperty("SAPSoldToNavigation")]
        public virtual SAPSoldTo SAPSoldTo { get; set; } = null!;
        [InverseProperty("SAPShipTo")]
        public virtual ICollection<SAPSoldToSAPShipTo> SAPSoldToSAPShipTos { get; set; }
    }
}
