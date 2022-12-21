using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("ScrapSAPSoldTo")]
    public partial class ScrapSAPSoldTo
    {
        [Key]
        [Column("ScrapSAPSoldToID")]
        public int ScrapSAPSoldToID { get; set; }
        [Column("SAPSoldToID")]
        public int SAPSoldToID { get; set; }
        [Column("LocationID")]
        public int LocationID { get; set; }
        public bool Active { get; set; }

        [ForeignKey("LocationId")]
        [InverseProperty("ScrapSAPSoldTos")]
        public virtual Location Location { get; set; } = null!;
        [ForeignKey("SAPSoldToId")]
        [InverseProperty("ScrapSAPSoldTos")]
        public virtual SAPSoldTo SAPSoldTo { get; set; } = null!;
    }
}
