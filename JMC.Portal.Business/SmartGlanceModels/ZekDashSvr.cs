using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.SmartGlanceModel
{
    [Table("ZEK_DashSvr")]
    public partial class ZekDashSvr
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("ord")]
        public int? Ord { get; set; }
        [Column("div_id")]
        public int? DivId { get; set; }
        [Column("name")]
        [StringLength(50)]
        public string Name { get; set; } = null!;
        [Column("alias")]
        [StringLength(50)]
        public string? Alias { get; set; }
        [Column("logo")]
        [StringLength(50)]
        public string? Logo { get; set; }
        [Column("logo_admin_pxvw")]
        [StringLength(50)]
        public string? LogoAdminPxvw { get; set; }
        [Column("logo_white")]
        [StringLength(50)]
        public string? LogoWhite { get; set; }
        [Column("icon")]
        [StringLength(50)]
        public string? Icon { get; set; }
        [Column("color")]
        [StringLength(50)]
        public string? Color { get; set; }
        [Column("show_rpt")]
        public int? ShowRpt { get; set; }
        [Column("show_ovw")]
        public int? ShowOvw { get; set; }
        [Column("tmr_ovw_plt")]
        public int? TmrOvwPlt { get; set; }
        [Column("tmr_ovw_ent")]
        public int? TmrOvwEnt { get; set; }
        [Column("mes_svr")]
        [StringLength(50)]
        public string? MesSvr { get; set; }
        [Column("mes_db")]
        [StringLength(50)]
        public string? MesDb { get; set; }
        [Column("jmad_lnk")]
        public int? JmadLnk { get; set; }
    }
}
