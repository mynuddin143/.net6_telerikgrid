using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.SmartGlanceModel
{
    [Table("ZEK_DashAtr")]
    public partial class ZekDashAtr
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("desc")]
        [StringLength(80)]
        public string? Desc { get; set; }
        [Column("value")]
        [StringLength(254)]
        public string? Value { get; set; }
    }
}
