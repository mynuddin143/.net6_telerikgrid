using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.SmartGlanceModel
{
    [Table("current_shift_oee")]
    public partial class CurrentShiftOee
    {
        [Key]
        [Column("server_name")]
        [StringLength(40)]
        public string ServerName { get; set; } = null!;
        [Column("division")]
        [StringLength(40)]
        [Unicode(false)]
        public string? Division { get; set; }
        [Column("plant")]
        [StringLength(40)]
        [Unicode(false)]
        public string? Plant { get; set; }
        [Column("plant_code")]
        [StringLength(4)]
        public string? PlantCode { get; set; }
        [Column("equipment_type")]
        [StringLength(40)]
        [Unicode(false)]
        public string? EquipmentType { get; set; }
        [Column("equipment")]
        [StringLength(80)]
        public string? Equipment { get; set; }
        [Key]
        [Column("ent_name")]
        [StringLength(80)]
        public string EntName { get; set; } = null!;
        [Column("ent_id")]
        public int? EntId { get; set; }
        [Column("ent_sort_order")]
        public int? EntSortOrder { get; set; }
        [Column("shift_start_local", TypeName = "datetime")]
        public DateTime? ShiftStartLocal { get; set; }
        [Column("shift_end_local", TypeName = "datetime")]
        public DateTime? ShiftEndLocal { get; set; }
        [Column("shift_start_utc", TypeName = "datetime")]
        public DateTime? ShiftStartUtc { get; set; }
        [Column("shift_end_utc", TypeName = "datetime")]
        public DateTime? ShiftEndUtc { get; set; }
        [Column("shift_date", TypeName = "date")]
        public DateTime? ShiftDate { get; set; }
        [Column("shift")]
        [StringLength(80)]
        public string? Shift { get; set; }
        [Column("shift_id")]
        public int? ShiftId { get; set; }
        [Column("run_time_minutes")]
        public double? RunTimeMinutes { get; set; }
        [Column("planned_production_time_minutes")]
        public double? PlannedProductionTimeMinutes { get; set; }
        [Column("stop_time_minutes")]
        public double? StopTimeMinutes { get; set; }
        [Column("total_calendar_time_minutes")]
        public double? TotalCalendarTimeMinutes { get; set; }
        [Column("good_tons")]
        public double? GoodTons { get; set; }
        [Column("scrap_tons")]
        public double? ScrapTons { get; set; }
        [Column("cons_tons")]
        public double? ConsTons { get; set; }
        [Column("run_rate_tons_per_hour")]
        public double? RunRateTonsPerHour { get; set; }
        [Column("ideal_run_rate_tons_per_hour")]
        public double? IdealRunRateTonsPerHour { get; set; }
        [Column("availability")]
        public double? Availability { get; set; }
        [Column("performance")]
        public double? Performance { get; set; }
        [Column("quality")]
        public double? Quality { get; set; }
        [Column("loading")]
        public double? Loading { get; set; }
        [Column("oee")]
        public double? Oee { get; set; }
        [Column("teep")]
        public double? Teep { get; set; }
        [Column("availability_explainer")]
        [StringLength(100)]
        public string? AvailabilityExplainer { get; set; }
        [Column("performance_explainer")]
        [StringLength(100)]
        public string? PerformanceExplainer { get; set; }
        [Column("quality_explainer")]
        [StringLength(100)]
        public string? QualityExplainer { get; set; }
        [Column("loading_explainer")]
        [StringLength(100)]
        public string? LoadingExplainer { get; set; }
        [Column("wo_id")]
        [StringLength(40)]
        public string? WoId { get; set; }
        [Column("item_id")]
        [StringLength(40)]
        public string? ItemId { get; set; }
        [Column("item_desc")]
        [StringLength(80)]
        public string? ItemDesc { get; set; }
        [Column("tube_size")]
        [StringLength(40)]
        public string? TubeSize { get; set; }
        [Column("state_desc")]
        [StringLength(80)]
        public string? StateDesc { get; set; }
        [Column("reas_desc")]
        [StringLength(80)]
        public string? ReasDesc { get; set; }
        [Column("cur_reas_start_local", TypeName = "datetime")]
        public DateTime? CurReasStartLocal { get; set; }
        [Column("cur_reas_start_utc", TypeName = "datetime")]
        public DateTime? CurReasStartUtc { get; set; }
        [Column("last_production_utc", TypeName = "datetime")]
        public DateTime? LastProductionUtc { get; set; }
        [Key]
        [Column("last_updated_utc", TypeName = "datetime")]
        public DateTime LastUpdatedUtc { get; set; }
        [Column("last_running_utc", TypeName = "datetime")]
        public DateTime? LastRunningUtc { get; set; }
        [Column("last_downtime_utc", TypeName = "datetime")]
        public DateTime? LastDowntimeUtc { get; set; }
        [Column("last_changeover_utc", TypeName = "datetime")]
        public DateTime? LastChangeoverUtc { get; set; }
        [Column("last_unscheduled_utc", TypeName = "datetime")]
        public DateTime? LastUnscheduledUtc { get; set; }
        [Column("previous_tube_size")]
        [StringLength(40)]
        public string? PreviousTubeSize { get; set; }
        [Column("previous_tube_size_ended_utc", TypeName = "datetime")]
        public DateTime? PreviousTubeSizeEndedUtc { get; set; }
        [Column("seconds_running")]
        public int? SecondsRunning { get; set; }
        [Column("seconds_downtime")]
        public int? SecondsDowntime { get; set; }
        [Column("seconds_changeover")]
        public int? SecondsChangeover { get; set; }
        [Column("seconds_planned_downtime")]
        public int? SecondsPlannedDowntime { get; set; }
        [Column("seconds_scheduled_maintenance")]
        public int? SecondsScheduledMaintenance { get; set; }
        [Column("seconds_unknown")]
        public int? SecondsUnknown { get; set; }
        [Column("seconds_unscheduled")]
        public int? SecondsUnscheduled { get; set; }
        [Column("next_prod_shift_start_utc", TypeName = "datetime")]
        public DateTime? NextProdShiftStartUtc { get; set; }
        [Column("next_prod_utc", TypeName = "datetime")]
        public DateTime? NextProdUtc { get; set; }
        [Column("next_tube_size")]
        [StringLength(40)]
        public string? NextTubeSize { get; set; }
        [Column("current_tube_size_tons_req")]
        public double? CurrentTubeSizeTonsReq { get; set; }
        [Column("current_tube_size_tons_prod")]
        public double? CurrentTubeSizeTonsProd { get; set; }
        [Column("current_tube_size_seconds_running")]
        public int? CurrentTubeSizeSecondsRunning { get; set; }
        [Column("previous_size_percent_complete")]
        public int? PreviousSizePercentComplete { get; set; }
        [Column("current_size_percent_complete")]
        public int? CurrentSizePercentComplete { get; set; }
    }
}
