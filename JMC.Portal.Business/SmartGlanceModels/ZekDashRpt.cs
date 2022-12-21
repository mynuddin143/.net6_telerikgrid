using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace JMC.Portal.Business.SmartGlanceModel
{
    [Table("ZEK_DashRpt")]
    [Index("Svr", "CatId", "Desc", Name = "SVR_CAT_DESC_ZEK_DashRpt", IsUnique = true)]
    public partial class ZekDashRpt
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("svr")]
        [StringLength(40)]
        public string Svr { get; set; } = null!;
        [Column("cat_id")]
        public int CatId { get; set; }
        [Column("ord")]
        public int Ord { get; set; }
        [Column("desc")]
        [StringLength(100)]
        public string Desc { get; set; } = null!;
        [Column("alias")]
        [StringLength(100)]
        public string? Alias { get; set; }
        [Column("tip")]
        [StringLength(100)]
        public string? Tip { get; set; }
        [Column("type")]
        public int? Type { get; set; }
        [Column("base_fld")]
        [StringLength(100)]
        public string? BaseFld { get; set; }
        [Column("root_fld")]
        [StringLength(100)]
        public string? RootFld { get; set; }
        [Column("sub_fld")]
        [StringLength(100)]
        public string? SubFld { get; set; }
        [Column("csv_pars")]
        [StringLength(200)]
        public string? CsvPars { get; set; }
        [Column("fg_color")]
        [StringLength(10)]
        public string? FgColor { get; set; }
        [Column("font")]
        [StringLength(100)]
        public string? Font { get; set; }
        [Column("bold")]
        public int? Bold { get; set; }
        [Column("hide")]
        public int? Hide { get; set; }
        [Column("created", TypeName = "datetime")]
        public DateTime? Created { get; set; }
        [Column("legacy_id")]
        public int? LegacyId { get; set; }
    }
}
