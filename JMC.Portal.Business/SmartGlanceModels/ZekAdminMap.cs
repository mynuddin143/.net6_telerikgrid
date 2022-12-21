using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.SmartGlanceModel
{
    [Keyless]
    [Table("ZEK_AdminMap")]
    public partial class ZekAdminMap
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("grp")]
        public int Grp { get; set; }
        [Column("ord")]
        public int Ord { get; set; }
        [Column("url")]
        [StringLength(200)]
        public string Url { get; set; } = null!;
        [Column("url_pars")]
        [StringLength(100)]
        public string? UrlPars { get; set; }
        [Column("title")]
        [StringLength(100)]
        public string? Title { get; set; }
        [Column("tip")]
        [StringLength(200)]
        public string? Tip { get; set; }
        [Column("hdr_txt")]
        [StringLength(200)]
        public string? HdrTxt { get; set; }
        [Column("icon")]
        [StringLength(200)]
        public string? Icon { get; set; }
        [Column("btn_cls")]
        [StringLength(100)]
        public string? BtnCls { get; set; }
        [Column("btn_txt_cls")]
        [StringLength(100)]
        public string? BtnTxtCls { get; set; }
        [Column("home")]
        public int Home { get; set; }
        [Column("signin")]
        public int Signin { get; set; }
        [Column("hidden")]
        public int Hidden { get; set; }
        [Column("adm_only")]
        public int AdmOnly { get; set; }
        [Column("mes_only")]
        public int MesOnly { get; set; }
        [Column("new_tab")]
        public int NewTab { get; set; }
        [Column("nav_hide")]
        public int NavHide { get; set; }
        [Column("act_svrs")]
        [StringLength(100)]
        public string? ActSvrs { get; set; }
        [Column("gvw_pg_sz")]
        public int? GvwPgSz { get; set; }
        [Column("pg_vws")]
        public int? PgVws { get; set; }
        [Column("lst_vw", TypeName = "datetime")]
        public DateTime? LstVw { get; set; }
        [Column("lst_vw_by")]
        [StringLength(100)]
        public string? LstVwBy { get; set; }
        [Column("add_dtm", TypeName = "datetime")]
        public DateTime? AddDtm { get; set; }
    }
}
