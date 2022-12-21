using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("Warehouse")]
    public partial class Warehouse
    {
        [Key]
        [Column("WarehouseID")]
        public int WarehouseId { get; set; }
        [Column("PlantID")]
        public int PlantId { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [StringLength(50)]
        [Unicode(false)]
        public string Description { get; set; } = null!;
    }
}
