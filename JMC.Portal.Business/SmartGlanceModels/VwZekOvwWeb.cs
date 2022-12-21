using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.SmartGlanceModel
{
    [Keyless]
    public partial class VwZekOvwWeb
    {
        [Column("sys")]
        [StringLength(80)]
        public string Sys { get; set; } = null!;
        [Column("plt_dsc")]
        [StringLength(80)]
        public string? PltDsc { get; set; }
        [Column("svr_pfx")]
        [StringLength(4)]
        public string? SvrPfx { get; set; }
        [Column("svr_sfx")]
        [StringLength(1)]
        [Unicode(false)]
        public string SvrSfx { get; set; } = null!;
        [Column("ent_id")]
        public int EntId { get; set; }
        [Column("ent_dsc")]
        [StringLength(80)]
        public string? EntDsc { get; set; }
        [Column("ent_typ")]
        [StringLength(80)]
        public string? EntTyp { get; set; }
        [Column("srt_ord")]
        public int? SrtOrd { get; set; }
        [Column("ord")]
        [StringLength(40)]
        public string Ord { get; set; } = null!;
        [Column("ord_dsc")]
        [StringLength(80)]
        public string OrdDsc { get; set; } = null!;
        [Column("mat")]
        [StringLength(40)]
        public string Mat { get; set; } = null!;
        [Column("qty_tub")]
        public int QtyTub { get; set; }
        [Column("req_tub")]
        public int ReqTub { get; set; }
        [Column("qty_bnd")]
        public int QtyBnd { get; set; }
        [Column("req_bnd")]
        public int ReqBnd { get; set; }
        [Column("qty_rec")]
        public int QtyRec { get; set; }
        [Column("rec_lbs")]
        public long RecLbs { get; set; }
        [Column("stat_dsc")]
        [StringLength(40)]
        public string? StatDsc { get; set; }
        [Column("stat_abb")]
        [StringLength(3)]
        public string? StatAbb { get; set; }
        [Column("stat_clr")]
        [StringLength(3)]
        [Unicode(false)]
        public string StatClr { get; set; } = null!;
        [Column("reas_dsc")]
        [StringLength(80)]
        public string ReasDsc { get; set; } = null!;
        [Column("sh0_chg_cnt")]
        public int Sh0ChgCnt { get; set; }
        [Column("sh0_qty_coil")]
        public int Sh0QtyCoil { get; set; }
        [Column("wk_qty_coil")]
        public int WkQtyCoil { get; set; }
        [Column("sh0_dt")]
        [StringLength(40)]
        public string Sh0Dt { get; set; } = null!;
        [Column("sh0_dsc")]
        [StringLength(40)]
        public string Sh0Dsc { get; set; } = null!;
        [Column("cs_start")]
        [StringLength(40)]
        public string? CsStart { get; set; }
        [Column("cs_start_dt", TypeName = "datetime")]
        public DateTime? CsStartDt { get; set; }
        [Column("cs_end")]
        [StringLength(40)]
        public string? CsEnd { get; set; }
        [Column("cs_end_dt", TypeName = "datetime")]
        public DateTime? CsEndDt { get; set; }
        [Column("cs_id")]
        public int CsId { get; set; }
        [Column("sh1_dt")]
        [StringLength(40)]
        public string Sh1Dt { get; set; } = null!;
        [Column("sh1_dsc")]
        [StringLength(40)]
        public string Sh1Dsc { get; set; } = null!;
        [Column("sh2_dt")]
        [StringLength(40)]
        public string Sh2Dt { get; set; } = null!;
        [Column("sh2_dsc")]
        [StringLength(40)]
        public string Sh2Dsc { get; set; } = null!;
        [Column("sh3_dt")]
        [StringLength(40)]
        public string Sh3Dt { get; set; } = null!;
        [Column("sh3_dsc")]
        [StringLength(40)]
        public string Sh3Dsc { get; set; } = null!;
        [Column("spd_mlt", TypeName = "decimal(18, 5)")]
        public decimal? SpdMlt { get; set; }
        [Column("spd_raw", TypeName = "decimal(18, 2)")]
        public decimal? SpdRaw { get; set; }
        [Column("spd_wld", TypeName = "decimal(18, 2)")]
        public decimal? SpdWld { get; set; }
        [Column("qty_ft")]
        public double QtyFt { get; set; }
        [Column("req_ft")]
        public double ReqFt { get; set; }
        [Column("qty_tns")]
        public double QtyTns { get; set; }
        [Column("req_tns")]
        public double ReqTns { get; set; }
        [Column("qty_lbs")]
        public double QtyLbs { get; set; }
        [Column("req_lbs")]
        public double ReqLbs { get; set; }
        [Column("reas_dur_mns")]
        public int? ReasDurMns { get; set; }
        [Column("sh0_coil_hr", TypeName = "decimal(18, 2)")]
        public decimal? Sh0CoilHr { get; set; }
        [Column("sh0_mns_dwn")]
        public double? Sh0MnsDwn { get; set; }
        [Column("sh0_mns_run")]
        public double? Sh0MnsRun { get; set; }
        [Column("sh0_mns_chg")]
        public double? Sh0MnsChg { get; set; }
        [Column("sh0_mns_unk")]
        public double? Sh0MnsUnk { get; set; }
        [Column("sh0_mns_uns")]
        public double? Sh0MnsUns { get; set; }
        [Column("sh0_mns_sch")]
        public double? Sh0MnsSch { get; set; }
        [Column("sh0_mns_pln")]
        public double? Sh0MnsPln { get; set; }
        [Column("sh0_tns_prd", TypeName = "decimal(18, 2)")]
        public decimal? Sh0TnsPrd { get; set; }
        [Column("sh0_tns_cns", TypeName = "decimal(18, 2)")]
        public decimal? Sh0TnsCns { get; set; }
        [Column("sh0_up_pct", TypeName = "decimal(18, 2)")]
        public decimal? Sh0UpPct { get; set; }
        [Column("sh0_oee", TypeName = "decimal(18, 2)")]
        public decimal? Sh0Oee { get; set; }
        [Column("sh1_tns_prd", TypeName = "decimal(18, 2)")]
        public decimal? Sh1TnsPrd { get; set; }
        [Column("sh1_tns_cns", TypeName = "decimal(18, 2)")]
        public decimal? Sh1TnsCns { get; set; }
        [Column("sh1_up_pct", TypeName = "decimal(18, 2)")]
        public decimal? Sh1UpPct { get; set; }
        [Column("sh1_oee", TypeName = "decimal(18, 2)")]
        public decimal? Sh1Oee { get; set; }
        [Column("sh2_tns_prd", TypeName = "decimal(18, 2)")]
        public decimal? Sh2TnsPrd { get; set; }
        [Column("sh2_tns_cns", TypeName = "decimal(18, 2)")]
        public decimal? Sh2TnsCns { get; set; }
        [Column("sh2_up_pct", TypeName = "decimal(18, 2)")]
        public decimal? Sh2UpPct { get; set; }
        [Column("sh2_oee", TypeName = "decimal(18, 2)")]
        public decimal? Sh2Oee { get; set; }
        [Column("sh3_tns_prd", TypeName = "decimal(18, 2)")]
        public decimal? Sh3TnsPrd { get; set; }
        [Column("sh3_tns_cns", TypeName = "decimal(18, 2)")]
        public decimal? Sh3TnsCns { get; set; }
        [Column("sh3_up_pct", TypeName = "decimal(18, 2)")]
        public decimal? Sh3UpPct { get; set; }
        [Column("sh3_oee", TypeName = "decimal(18, 2)")]
        public decimal? Sh3Oee { get; set; }
        [Column("tw_prd", TypeName = "decimal(18, 1)")]
        public decimal? TwPrd { get; set; }
        [Column("tw_cns", TypeName = "decimal(18, 1)")]
        public decimal? TwCns { get; set; }
        [Column("tw_oee", TypeName = "decimal(18, 1)")]
        public decimal? TwOee { get; set; }
        [Column("tw_up", TypeName = "decimal(18, 1)")]
        public decimal? TwUp { get; set; }
        [Column("tw_yld", TypeName = "decimal(18, 1)")]
        public decimal? TwYld { get; set; }
        [Column("cstk00")]
        [StringLength(40)]
        public string Cstk00 { get; set; } = null!;
        [Column("cstk01")]
        [StringLength(40)]
        public string Cstk01 { get; set; } = null!;
        [Column("cstk02")]
        [StringLength(40)]
        public string Cstk02 { get; set; } = null!;
        [Column("cstk03")]
        [StringLength(40)]
        public string Cstk03 { get; set; } = null!;
        [Column("cstk04")]
        [StringLength(40)]
        public string Cstk04 { get; set; } = null!;
        [Column("cstk05")]
        [StringLength(40)]
        public string Cstk05 { get; set; } = null!;
        [Column("cstk06")]
        [StringLength(40)]
        public string Cstk06 { get; set; } = null!;
        [Column("cstk07")]
        [StringLength(40)]
        public string Cstk07 { get; set; } = null!;
        [Column("cstk08")]
        [StringLength(40)]
        public string Cstk08 { get; set; } = null!;
        [Column("cstk09")]
        [StringLength(40)]
        public string Cstk09 { get; set; } = null!;
        [Column("hstk00")]
        [StringLength(12)]
        public string Hstk00 { get; set; } = null!;
        [Column("hstk01")]
        [StringLength(12)]
        public string Hstk01 { get; set; } = null!;
        [Column("hstk02")]
        [StringLength(12)]
        public string Hstk02 { get; set; } = null!;
        [Column("hstk03")]
        [StringLength(12)]
        public string Hstk03 { get; set; } = null!;
        [Column("hstk04")]
        [StringLength(12)]
        public string Hstk04 { get; set; } = null!;
        [Column("hstk05")]
        [StringLength(12)]
        public string Hstk05 { get; set; } = null!;
        [Column("hstk06")]
        [StringLength(12)]
        public string Hstk06 { get; set; } = null!;
        [Column("hstk07")]
        [StringLength(12)]
        public string Hstk07 { get; set; } = null!;
        [Column("hstk08")]
        [StringLength(12)]
        public string Hstk08 { get; set; } = null!;
        [Column("hstk09")]
        [StringLength(12)]
        public string Hstk09 { get; set; } = null!;
        [Column("mstk00")]
        [StringLength(40)]
        public string Mstk00 { get; set; } = null!;
        [Column("mstk01")]
        [StringLength(40)]
        public string Mstk01 { get; set; } = null!;
        [Column("mstk02")]
        [StringLength(40)]
        public string Mstk02 { get; set; } = null!;
        [Column("mstk03")]
        [StringLength(40)]
        public string Mstk03 { get; set; } = null!;
        [Column("mstk04")]
        [StringLength(40)]
        public string Mstk04 { get; set; } = null!;
        [Column("mstk05")]
        [StringLength(40)]
        public string Mstk05 { get; set; } = null!;
        [Column("mstk06")]
        [StringLength(40)]
        public string Mstk06 { get; set; } = null!;
        [Column("mstk07")]
        [StringLength(40)]
        public string Mstk07 { get; set; } = null!;
        [Column("mstk08")]
        [StringLength(40)]
        public string Mstk08 { get; set; } = null!;
        [Column("mstk09")]
        [StringLength(40)]
        public string Mstk09 { get; set; } = null!;
        [Column("lst_uptm")]
        public DateTime? LstUptm { get; set; }
        [Column("row_id")]
        public int RowId { get; set; }
    }
}
