using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace JMC.Portal.Business.SmartGlanceModel
{
    [Table("ZEK_DashCat")]
    [Index("Svr", "SvrCatId", Name = "SVR_ZEK_DashCat", IsUnique = true)]
    public partial class ZekDashCat
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("svr")]
        [StringLength(40)]
        public string Svr { get; set; } = null!;
        [Column("svr_cat_id")]
        public int SvrCatId { get; set; }
        [Column("row")]
        public int? Row { get; set; }
        [Column("col")]
        public int? Col { get; set; }
        [Column("ord")]
        public int? Ord { get; set; }
        [Column("desc")]
        [StringLength(100)]
        public string? Desc { get; set; }
        [Column("tip")]
        [StringLength(50)]
        public string? Tip { get; set; }
        [Column("base_fld")]
        [StringLength(100)]
        public string? BaseFld { get; set; }
        [Column("root_fld")]
        [StringLength(100)]
        public string? RootFld { get; set; }
        [Column("color_bg")]
        [StringLength(10)]
        public string? ColorBg { get; set; }
        [Column("color_fg")]
        [StringLength(10)]
        public string? ColorFg { get; set; }
        [Column("font")]
        [StringLength(100)]
        public string? Font { get; set; }
        [Column("bold")]
        public int? Bold { get; set; }
        [Column("hide")]
        public int Hide { get; set; }
    }
}
