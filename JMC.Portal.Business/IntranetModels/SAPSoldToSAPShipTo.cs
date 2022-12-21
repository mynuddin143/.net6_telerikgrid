using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("SAPSoldToSAPShipTo")]
    [Index("SAPSoldToID", "SAPShipToID", Name = "IX_SAPSoldToSAPShipTo", IsUnique = true)]
    public partial class SAPSoldToSAPShipTo
    {
        [Key]
        [Column("SAPSoldToSAPShipToID")]
        public int SAPSoldToSAPShipToID { get; set; }
        [Column("SAPSoldToID")]
        public int SAPSoldToID { get; set; }
        [Column("SAPShipToID")]
        public int SAPShipToID { get; set; }

        [ForeignKey("SAPShipToID")]
        [InverseProperty("SAPSoldToSAPShipTos")]
        public virtual SAPShipTo SAPShipTo { get; set; } = null!;
        [ForeignKey("SAPSoldToID")]
        [InverseProperty("SAPSoldToSAPShipTos")]
        public virtual SAPSoldTo SAPSoldTo { get; set; } = null!;
    }
}
