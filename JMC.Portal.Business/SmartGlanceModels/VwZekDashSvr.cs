using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.SmartGlanceModel
{
    [Keyless]
    public partial class VwZekDashSvr
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("ord")]
        public int? Ord { get; set; }
        [Column("div_id")]
        public int? DivId { get; set; }
        [Column("div_nm")]
        [StringLength(9)]
        [Unicode(false)]
        public string? DivNm { get; set; }
        [Column("div_sh")]
        [StringLength(5)]
        [Unicode(false)]
        public string? DivSh { get; set; }
        [Column("div_abb")]
        [StringLength(2)]
        [Unicode(false)]
        public string? DivAbb { get; set; }
        [Column("alias")]
        [StringLength(50)]
        public string? Alias { get; set; }
        [Column("name")]
        [StringLength(50)]
        public string? Name { get; set; }
        [Column("pfx")]
        [StringLength(4000)]
        public string? Pfx { get; set; }
        [Column("mid")]
        [StringLength(4000)]
        public string? Mid { get; set; }
        [Column("sfx")]
        [StringLength(4000)]
        public string? Sfx { get; set; }
        [Column("typ")]
        [StringLength(1)]
        [Unicode(false)]
        public string? Typ { get; set; }
        [Column("mes_svr")]
        [StringLength(4000)]
        public string? MesSvr { get; set; }
        [Column("mes_db")]
        [StringLength(50)]
        public string? MesDb { get; set; }
        [Column("jmad_lnk")]
        public int JmadLnk { get; set; }
        [Column("logo")]
        [StringLength(80)]
        public string? Logo { get; set; }
        [Column("logo_admin_pxvw")]
        [StringLength(50)]
        public string LogoAdminPxvw { get; set; } = null!;
        [Column("logo_css_admin")]
        [StringLength(62)]
        public string LogoCssAdmin { get; set; } = null!;
        [Column("logo_white")]
        [StringLength(80)]
        public string? LogoWhite { get; set; }
        [Column("icon")]
        [StringLength(80)]
        public string? Icon { get; set; }
        [Column("color")]
        [StringLength(80)]
        public string? Color { get; set; }
        [Column("show_rpt")]
        public int? ShowRpt { get; set; }
        [Column("show_ovw")]
        public int? ShowOvw { get; set; }
        [Column("tmr_ovw_plt")]
        public int? TmrOvwPlt { get; set; }
        [Column("tmr_ovw_ent")]
        public int? TmrOvwEnt { get; set; }
        [Column("rpt_lnk")]
        [StringLength(68)]
        public string? RptLnk { get; set; }
    }
}
