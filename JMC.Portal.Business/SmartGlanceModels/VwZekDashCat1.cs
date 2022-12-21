using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.SmartGlanceModel
{
    [Keyless]
    public partial class VwZekDashCat1
    {
        [Column("svr")]
        [StringLength(40)]
        public string Svr { get; set; } = null!;
        [Column("rnk")]
        public long? Rnk { get; set; }
        [Column("id")]
        public int Id { get; set; }
        [Column("svr_cat_id")]
        public int SvrCatId { get; set; }
        [Column("rpt_cnt")]
        public int? RptCnt { get; set; }
        [Column("col")]
        public int? Col { get; set; }
        [Column("row")]
        public int? Row { get; set; }
        [Column("desc")]
        [StringLength(100)]
        public string? Desc { get; set; }
        [Column("tip")]
        [StringLength(100)]
        public string? Tip { get; set; }
        [Column("base_fld")]
        [StringLength(100)]
        public string? BaseFld { get; set; }
        [Column("root_fld")]
        [StringLength(100)]
        public string? RootFld { get; set; }
        [Column("bg_clr")]
        [StringLength(80)]
        public string? BgClr { get; set; }
        [Column("fg_clr")]
        [StringLength(80)]
        public string? FgClr { get; set; }
        [Column("font")]
        [StringLength(100)]
        public string? Font { get; set; }
        [Column("bold")]
        public int Bold { get; set; }
        [Column("hide")]
        public int Hide { get; set; }
        [Column("ord")]
        public int? Ord { get; set; }
        [Column("cid")]
        public int Cid { get; set; }
    }
}
