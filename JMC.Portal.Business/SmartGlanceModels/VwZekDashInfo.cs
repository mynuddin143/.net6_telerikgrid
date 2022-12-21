using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.SmartGlanceModel
{
    [Keyless]
    public partial class VwZekDashInfo
    {
        [Column("svr")]
        [StringLength(4000)]
        public string? Svr { get; set; }
        [Column("svr_cnt")]
        public int? SvrCnt { get; set; }
        [Column("svrs")]
        public string? Svrs { get; set; }
        [Column("cols")]
        public int Cols { get; set; }
        [Column("rows")]
        public int Rows { get; set; }
        [Column("cat_cnt")]
        public int CatCnt { get; set; }
        [Column("cat_hid")]
        public int CatHid { get; set; }
        [Column("rpt_cnt")]
        public int? RptCnt { get; set; }
        [Column("rpts_ass")]
        public int? RptsAss { get; set; }
        [Column("rpts_hid")]
        public int? RptsHid { get; set; }
        [Column("div_id")]
        public int? DivId { get; set; }
        [Column("div_abb")]
        [StringLength(2)]
        [Unicode(false)]
        public string? DivAbb { get; set; }
        [Column("div_nm")]
        [StringLength(9)]
        [Unicode(false)]
        public string? DivNm { get; set; }
        [Column("div_sh")]
        [StringLength(5)]
        [Unicode(false)]
        public string? DivSh { get; set; }
    }
}
