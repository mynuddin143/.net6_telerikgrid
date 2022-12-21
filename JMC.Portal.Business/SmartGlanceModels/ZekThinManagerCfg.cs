using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.SmartGlanceModel
{
    [Keyless]
    [Table("ZEK_ThinManager_Cfg")]
    public partial class ZekThinManagerCfg
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("pfx")]
        [StringLength(10)]
        public string? Pfx { get; set; }
        [Column("term_type")]
        [StringLength(40)]
        public string? TermType { get; set; }
        [Column("term_name")]
        [StringLength(40)]
        public string? TermName { get; set; }
        [Column("user_acct")]
        [StringLength(40)]
        public string? UserAcct { get; set; }
        [Column("app_shell")]
        [StringLength(20)]
        public string? AppShell { get; set; }
        [Column("app_name")]
        [StringLength(40)]
        public string? AppName { get; set; }
        [Column("app_acp1")]
        [StringLength(80)]
        public string? AppAcp1 { get; set; }
        [Column("app_acp2")]
        [StringLength(80)]
        public string? AppAcp2 { get; set; }
        [Column("app_acp3")]
        [StringLength(80)]
        public string? AppAcp3 { get; set; }
        [Column("dc_mes")]
        [StringLength(80)]
        public string? DcMes { get; set; }
        [Column("dc_hmi")]
        [StringLength(80)]
        public string? DcHmi { get; set; }
    }
}
