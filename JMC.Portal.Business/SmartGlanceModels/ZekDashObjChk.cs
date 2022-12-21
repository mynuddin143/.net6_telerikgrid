using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.SmartGlanceModel
{
    [Table("ZEK_DashObjChks")]
    public partial class ZekDashObjChk
    {
        [Key]
        [Column("rid")]
        public int Rid { get; set; }
        [Column("name")]
        [StringLength(80)]
        public string? Name { get; set; }
        [Column("type")]
        [StringLength(40)]
        public string? Type { get; set; }
        [Column("def_cnt")]
        public int? DefCnt { get; set; }
        [Column("defin")]
        public string? Defin { get; set; }
        public int? NonExist { get; set; }
        [Column("NotOK")]
        public int? NotOk { get; set; }
        [Column("OK")]
        public int? Ok { get; set; }
        [Column("NotExist_Svrs")]
        [StringLength(500)]
        public string? NotExistSvrs { get; set; }
        [Column("NotOK_Svrs")]
        [StringLength(500)]
        public string? NotOkSvrs { get; set; }
        [Column("OK_Svrs")]
        [StringLength(500)]
        public string? OkSvrs { get; set; }
        [Column("systat")]
        public int? Systat { get; set; }
        [Column("crdate", TypeName = "datetime")]
        public DateTime? Crdate { get; set; }
        [Column("refdate", TypeName = "datetime")]
        public DateTime? Refdate { get; set; }
        [Column("base")]
        [StringLength(40)]
        public string? Base { get; set; }
        [Column("chkdate", TypeName = "datetime")]
        public DateTime? Chkdate { get; set; }
    }
}
