using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("RandomLengthSAPSoldTo")]
    public partial class RandomLengthSAPSoldTo
    {
        [Key]
        [Column("RandomLengthSAPSoldToID")]
        public int RandomLengthSAPSoldToID { get; set; }
        [Column("SAPSoldToID")]
        public int SAPSoldToID { get; set; }
        [Column("LocationID")]
        public int LocationID { get; set; }
        public bool Active { get; set; }

        [ForeignKey("LocationID")]
        [InverseProperty("RandomLengthSAPSoldTos")]
        public virtual Location Location { get; set; } = null!;
        [ForeignKey("SAPSoldToId")]
        [InverseProperty("RandomLengthSAPSoldTos")]
        public virtual SAPSoldTo SAPSoldTo { get; set; } = null!;
    }
}
