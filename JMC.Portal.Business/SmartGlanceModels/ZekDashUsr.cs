using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.SmartGlanceModel
{
    [Table("ZEK_DashUsr")]
    public partial class ZekDashUsr
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        [StringLength(50)]
        public string Name { get; set; } = null!;
        [Column("login")]
        [StringLength(50)]
        public string Login { get; set; } = null!;
        [Column("div_id")]
        public int DivId { get; set; }
        [Column("vw_svr")]
        public int VwSvr { get; set; }
        [Column("vw_msg")]
        public int VwMsg { get; set; }
        [Column("edt_cat")]
        public int EdtCat { get; set; }
        [Column("edt_rpt")]
        public int EdtRpt { get; set; }
        [Column("edt_svr")]
        public int EdtSvr { get; set; }
        [Column("edt_usr")]
        public int EdtUsr { get; set; }
        [Column("add_rpt")]
        public int AddRpt { get; set; }
        [Column("all_del")]
        public int AllDel { get; set; }
        [Column("rec_hst")]
        public int RecHst { get; set; }
    }
}
