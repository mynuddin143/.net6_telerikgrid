using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.SmartGlanceModel
{
    [Keyless]
    public partial class PlantStat
    {
        [Column("division")]
        [StringLength(40)]
        [Unicode(false)]
        public string? Division { get; set; }
        [Column("plant")]
        [StringLength(40)]
        [Unicode(false)]
        public string? Plant { get; set; }
        [Column("last_production_utc", TypeName = "datetime")]
        public DateTime? LastProductionUtc { get; set; }
        [Column("last_production_time_ago")]
        [StringLength(20)]
        public string? LastProductionTimeAgo { get; set; }
        [Column("num_ents")]
        public int? NumEnts { get; set; }
        [Column("num_running")]
        public int? NumRunning { get; set; }
        [Column("num_stopped")]
        public int? NumStopped { get; set; }
        [Column("num_unscheduled")]
        public int? NumUnscheduled { get; set; }
        [Column("num_unknown")]
        public int? NumUnknown { get; set; }
        [Column("num_shifts")]
        public int? NumShifts { get; set; }
        [Column("num_no_shifts")]
        public int? NumNoShifts { get; set; }
        [Column("num_prod_on_cur_shift")]
        public int? NumProdOnCurShift { get; set; }
        [Column("next_prod_shift_start_utc", TypeName = "datetime")]
        public DateTime? NextProdShiftStartUtc { get; set; }
        [Column("next_prod_shift_start")]
        [StringLength(20)]
        public string? NextProdShiftStart { get; set; }
        [Column("next_prod_utc", TypeName = "datetime")]
        public DateTime? NextProdUtc { get; set; }
        [Column("next_prod")]
        [StringLength(20)]
        public string? NextProd { get; set; }
    }
}
