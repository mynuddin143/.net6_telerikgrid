using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.SmartGlanceModel
{
    [Keyless]
    public partial class VwZekDashInfoRpt
    {
        [Column("svr")]
        [StringLength(4000)]
        public string? Svr { get; set; }
        [Column("cat_id")]
        public int? CatId { get; set; }
        [Column("cat")]
        [StringLength(100)]
        public string? Cat { get; set; }
        [Column("cnt")]
        public int Cnt { get; set; }
        [Column("hid")]
        public int Hid { get; set; }
        [Column("rpts")]
        public string? Rpts { get; set; }
        [Column("hidden")]
        public string? Hidden { get; set; }
        [Column("col")]
        public int? Col { get; set; }
        [Column("row")]
        public int? Row { get; set; }
    }
}
