using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace JMC.Portal.Business.SmartGlanceModel
{
    [Table("shift_history")]
    [Index("Plant", "EntName", Name = "IX_plant_ent_name")]
    public partial class ShiftHistory
    {
        [Key]
        [Column("server_name")]
        [StringLength(128)]
        public string ServerName { get; set; } = null!;
        [Column("division")]
        [StringLength(40)]
        public string? Division { get; set; }
        [Column("plant")]
        [StringLength(40)]
        public string? Plant { get; set; }
        [Column("plant_code")]
        [StringLength(40)]
        public string? PlantCode { get; set; }
        [Column("equipment_type")]
        [StringLength(40)]
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
        [Key]
        [Column("shift_start_local", TypeName = "datetime")]
        public DateTime ShiftStartLocal { get; set; }
        [Column("shift_end_local", TypeName = "datetime")]
        public DateTime? ShiftEndLocal { get; set; }
        [Column("shift_id")]
        public int? ShiftId { get; set; }
        [Column("shift")]
        [StringLength(40)]
        public string? Shift { get; set; }
        [Column("availability")]
        public float? Availability { get; set; }
        [Column("performance")]
        public float? Performance { get; set; }
        [Column("quality")]
        public float? Quality { get; set; }
        [Column("loading")]
        public float? Loading { get; set; }
        [Column("oee")]
        public float? Oee { get; set; }
        [Column("teep")]
        public float? Teep { get; set; }
        [Column("run_time_minutes")]
        public float? RunTimeMinutes { get; set; }
        [Column("planned_production_time_minutes")]
        public float? PlannedProductionTimeMinutes { get; set; }
        [Column("total_calendar_time_minutes")]
        public float? TotalCalendarTimeMinutes { get; set; }
        [Column("good_tons")]
        public float? GoodTons { get; set; }
        [Column("scrap_tons")]
        public float? ScrapTons { get; set; }
        [Column("cons_tons")]
        public float? ConsTons { get; set; }
        [Column("run_rate_tons_per_hour")]
        public float? RunRateTonsPerHour { get; set; }
        [Column("ideal_run_rate_tons_per_hour")]
        public float? IdealRunRateTonsPerHour { get; set; }
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
        [Key]
        [Column("last_updated_utc")]
        public DateTime LastUpdatedUtc { get; set; }
    }
}
