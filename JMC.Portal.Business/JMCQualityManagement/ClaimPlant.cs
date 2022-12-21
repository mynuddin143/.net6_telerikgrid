using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.JMCQualityManagement
{
    [Table("ClaimPlant")]
    public partial class ClaimPlant
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("LocationID")]
        public long LocationId { get; set; }
        public int? PlantType { get; set; }
    }
}
