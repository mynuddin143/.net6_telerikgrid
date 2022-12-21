using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.SmartGlanceModel
{
    [Keyless]
    public partial class VwZekOvwIncWeb
    {
        [Column("row_id")]
        public int RowId { get; set; }
        [Column("svr_pfx")]
        [StringLength(4)]
        public string? SvrPfx { get; set; }
        [Column("svr_sfx")]
        [StringLength(1)]
        [Unicode(false)]
        public string SvrSfx { get; set; } = null!;
        [Column("plt")]
        [StringLength(80)]
        public string Plt { get; set; } = null!;
        [Column("perdt", TypeName = "date")]
        public DateTime? Perdt { get; set; }
        [Column("updt", TypeName = "date")]
        public DateTime? Updt { get; set; }
        [Column("cols")]
        [StringLength(4000)]
        public string? Cols { get; set; }
        [Column("strs")]
        [StringLength(1000)]
        public string? Strs { get; set; }
        [Column("updtm", TypeName = "datetime")]
        public DateTime? Updtm { get; set; }
        [Column("svr_dsc")]
        [StringLength(80)]
        public string SvrDsc { get; set; } = null!;
    }
}
