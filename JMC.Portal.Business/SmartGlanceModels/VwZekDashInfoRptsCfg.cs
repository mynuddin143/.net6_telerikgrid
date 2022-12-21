using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.SmartGlanceModel
{
    [Keyless]
    public partial class VwZekDashInfoRptsCfg
    {
        [Column("type")]
        [StringLength(4)]
        [Unicode(false)]
        public string? Type { get; set; }
        [Column("rpt_desc")]
        [StringLength(100)]
        public string? RptDesc { get; set; }
        [Column("config")]
        public int? Config { get; set; }
        [Column("avail")]
        public int? Avail { get; set; }
        [Column("configured_for_dashboards")]
        public string? ConfiguredForDashboards { get; set; }
        [Column("available_on_dashboards")]
        public string? AvailableOnDashboards { get; set; }
        [Column("not_configured_for_dashboards")]
        public string? NotConfiguredForDashboards { get; set; }
        [Column("not_available_on_dashboards")]
        public string? NotAvailableOnDashboards { get; set; }
    }
}
