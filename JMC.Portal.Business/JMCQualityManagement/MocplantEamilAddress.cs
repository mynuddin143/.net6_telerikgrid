using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.JMCQualityManagement
{
    [Table("MOCPlantEamilAddresses")]
    public partial class MocplantEamilAddress
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("PlantID")]
        public int PlantId { get; set; }
        [StringLength(250)]
        public string? Name { get; set; }
        [StringLength(100)]
        public string? Position { get; set; }
    }
}
