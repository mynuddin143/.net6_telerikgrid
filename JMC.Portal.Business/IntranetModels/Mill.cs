using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("Mill")]
    public partial class Mill
    {
        [Key]
        [Column("MillID")]
        public int MillID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [Column("PlantID")]
        public int PlantID { get; set; }
        public bool Active { get; set; }

        [ForeignKey("PlantID")]
        [InverseProperty("Mills")]
        public virtual Plant Plant { get; set; } = null!;
    }
}
