using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.SmartGlanceModel
{
    [Table("WT_Overview_Web")]
    public partial class WtOverviewWeb
    {
        [Column("ins_tim", TypeName = "datetime")]
        public DateTime? InsTim { get; set; }
        [Key]
        [Column("svr")]
        [StringLength(40)]
        public string Svr { get; set; } = null!;
        [Column("rnk")]
        public int? Rnk { get; set; }
        [Column("rnkid")]
        public int? Rnkid { get; set; }
        [Column("row")]
        public int? Row { get; set; }
        [Column("col")]
        public int? Col { get; set; }
        [Key]
        [Column("ent_id")]
        public int EntId { get; set; }
        [Column("ent_name")]
        [StringLength(40)]
        public string? EntName { get; set; }
        [Column("ent_desc")]
        [StringLength(80)]
        public string? EntDesc { get; set; }
        [Column("ent_alias")]
        [StringLength(40)]
        public string? EntAlias { get; set; }
        [Column("prd_dt")]
        [StringLength(40)]
        public string? PrdDt { get; set; }
        [Column("prd_dt_fmt")]
        [StringLength(40)]
        public string? PrdDtFmt { get; set; }
        [Column("shift_id")]
        public int? ShiftId { get; set; }
        [Column("shift_desc")]
        [StringLength(40)]
        public string? ShiftDesc { get; set; }
        [Column("sch_span")]
        [StringLength(40)]
        public string? SchSpan { get; set; }
        [Column("sch_hrs")]
        public int? SchHrs { get; set; }
        [Column("sch_hrs_fmt")]
        [StringLength(40)]
        public string? SchHrsFmt { get; set; }
        [Column("sch_mins")]
        public int? SchMins { get; set; }
        [Column("run")]
        [StringLength(40)]
        public string? Run { get; set; }
        [Column("ord_full")]
        [StringLength(40)]
        public string? OrdFull { get; set; }
        [Column("ord")]
        [StringLength(40)]
        public string? Ord { get; set; }
        [Column("oper_id")]
        [StringLength(40)]
        public string? OperId { get; set; }
        [Column("itm_full")]
        [StringLength(40)]
        public string? ItmFull { get; set; }
        [Column("itm")]
        [StringLength(40)]
        public string? Itm { get; set; }
        [Column("itm_dsc")]
        [StringLength(254)]
        public string? ItmDsc { get; set; }
        [Column("ord_qty_req", TypeName = "numeric(10, 0)")]
        public decimal? OrdQtyReq { get; set; }
        [Column("ord_qty_prd", TypeName = "numeric(10, 3)")]
        public decimal? OrdQtyPrd { get; set; }
        [Column("ord_pct")]
        [StringLength(40)]
        public string? OrdPct { get; set; }
        [Column("run_sec")]
        [StringLength(40)]
        public string? RunSec { get; set; }
        [Column("ord_full_sec")]
        [StringLength(40)]
        public string? OrdFullSec { get; set; }
        [Column("ord_sec")]
        [StringLength(40)]
        public string? OrdSec { get; set; }
        [Column("itm_full_sec")]
        [StringLength(40)]
        public string? ItmFullSec { get; set; }
        [Column("itm_sec")]
        [StringLength(40)]
        public string? ItmSec { get; set; }
        [Column("itm_dsc_sec")]
        [StringLength(254)]
        public string? ItmDscSec { get; set; }
        [Column("state_cd")]
        public int? StateCd { get; set; }
        [Column("state_dsc")]
        [StringLength(40)]
        public string? StateDsc { get; set; }
        [Column("reas_cd")]
        public int? ReasCd { get; set; }
        [Column("reas_dsc")]
        [StringLength(40)]
        public string? ReasDsc { get; set; }
        [Column("mns_cur")]
        public int? MnsCur { get; set; }
        [Column("mns_all", TypeName = "decimal(10, 1)")]
        public decimal? MnsAll { get; set; }
        [Column("mns_ava", TypeName = "decimal(10, 1)")]
        public decimal? MnsAva { get; set; }
        [Column("mns_run", TypeName = "decimal(10, 1)")]
        public decimal? MnsRun { get; set; }
        [Column("mns_chg", TypeName = "decimal(10, 1)")]
        public decimal? MnsChg { get; set; }
        [Column("mns_dwn", TypeName = "decimal(10, 1)")]
        public decimal? MnsDwn { get; set; }
        [Column("mns_pln", TypeName = "decimal(10, 1)")]
        public decimal? MnsPln { get; set; }
        [Column("mns_mnt", TypeName = "decimal(10, 1)")]
        public decimal? MnsMnt { get; set; }
        [Column("mns_unk", TypeName = "decimal(10, 1)")]
        public decimal? MnsUnk { get; set; }
        [Column("frm_cur")]
        [StringLength(40)]
        public string? FrmCur { get; set; }
        [Column("frm_run")]
        [StringLength(40)]
        public string? FrmRun { get; set; }
        [Column("frm_chg")]
        [StringLength(40)]
        public string? FrmChg { get; set; }
        [Column("frm_unk")]
        [StringLength(40)]
        public string? FrmUnk { get; set; }
        [Column("frm_dwn")]
        [StringLength(40)]
        public string? FrmDwn { get; set; }
        [Column("frm_uns")]
        [StringLength(40)]
        public string? FrmUns { get; set; }
        [Column("mtr_run")]
        public int? MtrRun { get; set; }
        [Column("mtr_chg")]
        public int? MtrChg { get; set; }
        [Column("mtr_unk")]
        public int? MtrUnk { get; set; }
        [Column("mtr_dwn")]
        public int? MtrDwn { get; set; }
        [Column("mtr_uns")]
        public int? MtrUns { get; set; }
        [Column("pct_run")]
        public int? PctRun { get; set; }
        [Column("pct_chg")]
        public int? PctChg { get; set; }
        [Column("pct_dwn")]
        public int? PctDwn { get; set; }
        [Column("pct_uns")]
        public int? PctUns { get; set; }
        [Column("prd_tgs")]
        public int? PrdTgs { get; set; }
        [Column("prd_tgs_gd")]
        public int? PrdTgsGd { get; set; }
        [Column("prd_tgs_bd")]
        public int? PrdTgsBd { get; set; }
        [Column("prd_tns", TypeName = "numeric(10, 1)")]
        public decimal? PrdTns { get; set; }
        [Column("prd_tns_gd", TypeName = "numeric(10, 1)")]
        public decimal? PrdTnsGd { get; set; }
        [Column("prd_tns_bd", TypeName = "numeric(10, 1)")]
        public decimal? PrdTnsBd { get; set; }
        [Column("tns_hr", TypeName = "numeric(10, 1)")]
        public decimal? TnsHr { get; set; }
        [Column("prd_gd_pct", TypeName = "numeric(10, 1)")]
        public decimal? PrdGdPct { get; set; }
        [Column("prd_bd_pct", TypeName = "numeric(10, 1)")]
        public decimal? PrdBdPct { get; set; }
        [Column("prd_pcs")]
        public int? PrdPcs { get; set; }
        [Column("prd_pcs_gd")]
        public int? PrdPcsGd { get; set; }
        [Column("prd_pcs_bd")]
        public int? PrdPcsBd { get; set; }
        [Column("pcs_hr", TypeName = "numeric(10, 1)")]
        public decimal? PcsHr { get; set; }
        [Column("cns_pcs")]
        public int? CnsPcs { get; set; }
        [Column("cns_tgs")]
        public int? CnsTgs { get; set; }
        [Column("cns_tns", TypeName = "numeric(10, 1)")]
        public decimal? CnsTns { get; set; }
        [Column("stat_id")]
        [StringLength(40)]
        public string? StatId { get; set; }
        [Column("mach_speed", TypeName = "numeric(10, 1)")]
        public decimal? MachSpeed { get; set; }
        [Column("mach_state")]
        public int? MachState { get; set; }
        [Column("lst_upd_stats")]
        [StringLength(40)]
        public string? LstUpdStats { get; set; }
        [Column("mach_speeds")]
        public string? MachSpeeds { get; set; }
        [Column("mach_speeds_times")]
        public string? MachSpeedsTimes { get; set; }
        [Column("lst_upd")]
        [StringLength(40)]
        public string? LstUpd { get; set; }
        [Column("rws_cfg")]
        public int? RwsCfg { get; set; }
        [Column("cls_cfg")]
        public int? ClsCfg { get; set; }
    }
}
