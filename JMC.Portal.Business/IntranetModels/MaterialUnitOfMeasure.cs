using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("MaterialUnitOfMeasure")]
    public partial class MaterialUnitOfMeasure
    {
        [Key]
        [Column("MaterialUnitOfMeasureID")]
        public int MaterialUnitOfMeasureId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
    }
}
