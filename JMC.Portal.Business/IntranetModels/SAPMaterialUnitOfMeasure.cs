using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("SAPMaterialUnitOfMeasure")]
    public partial class SAPMaterialUnitOfMeasure
    {
        [Key]
        [Column("SAPMaterialUnitOfMeasureID")]
        public int SAPMaterialUnitOfMeasureID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
    }
}
