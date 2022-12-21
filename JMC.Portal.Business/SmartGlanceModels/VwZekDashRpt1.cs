using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.SmartGlanceModel
{
    [Keyless]
    public partial class VwZekDashRpt1
    {
        [Column("rnk")]
        public long? Rnk { get; set; }
        [Column("cat_rnk")]
        public long? CatRnk { get; set; }
        [Column("id")]
        public int Id { get; set; }
        [Column("svr")]
        [StringLength(40)]
        public string Svr { get; set; } = null!;
        [Column("cat_id")]
        public int CatId { get; set; }
        [Column("ord_id")]
        public int OrdId { get; set; }
        [Column("cat_desc")]
        [StringLength(100)]
        public string? CatDesc { get; set; }
        [Column("cat_base_fld")]
        [StringLength(100)]
        public string? CatBaseFld { get; set; }
        [Column("cat_root_fld")]
        [StringLength(100)]
        public string? CatRootFld { get; set; }
        [Column("desc")]
        [StringLength(100)]
        public string? Desc { get; set; }
        [Column("alias")]
        [StringLength(100)]
        public string? Alias { get; set; }
        [Column("tip")]
        [StringLength(100)]
        public string? Tip { get; set; }
        [Column("type")]
        public int? Type { get; set; }
        [Column("type_dsc")]
        [StringLength(4)]
        [Unicode(false)]
        public string TypeDsc { get; set; } = null!;
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
        [StringLength(4000)]
        public string? CsvPars { get; set; }
        [Column("fld_find_pth")]
        [StringLength(4000)]
        public string? FldFindPth { get; set; }
        [Column("fld_pth")]
        [StringLength(4000)]
        public string? FldPth { get; set; }
        [Column("link_pth")]
        [StringLength(4000)]
        public string? LinkPth { get; set; }
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
        [Column("create")]
        [StringLength(40)]
        public string? Create { get; set; }
        [Column("vws")]
        public int Vws { get; set; }
        [Column("vw_lst")]
        [StringLength(3)]
        [Unicode(false)]
        public string VwLst { get; set; } = null!;
        [Column("rpt_dys")]
        public int RptDys { get; set; }
        [Column("vws_dys")]
        public int VwsDys { get; set; }
    }
}
