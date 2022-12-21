using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.SmartGlanceModel
{
    [Keyless]
    public partial class VwZekDashInfoCat
    {
        [Column("svr")]
        [StringLength(4000)]
        public string? Svr { get; set; }
        [Column("cat_cnt")]
        public int CatCnt { get; set; }
        [Column("cat_hid")]
        public int CatHid { get; set; }
        [Column("cols")]
        public int Cols { get; set; }
        [Column("rows")]
        public int Rows { get; set; }
        [Column("cols_all")]
        public string? ColsAll { get; set; }
        [Column("col1")]
        public string? Col1 { get; set; }
        [Column("col2")]
        public string? Col2 { get; set; }
        [Column("col3")]
        public string? Col3 { get; set; }
        [Column("col4")]
        public string? Col4 { get; set; }
        [Column("col5")]
        public string? Col5 { get; set; }
        [Column("hidden")]
        public string? Hidden { get; set; }
    }
}
