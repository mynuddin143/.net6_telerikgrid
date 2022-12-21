using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace JMC.Portal.Business.SmartGlanceModel
{
    [Table("zekAtlasEquipmentDashboard")]
    [Index("Plant", "Server", "EntId", Name = "UX_plant_server_ent_id", IsUnique = true)]
    public partial class ZekAtlasEquipmentDashboard
    {
        [StringLength(80)]
        public string Server { get; set; } = null!;
        [StringLength(80)]
        public string Plant { get; set; } = null!;
        [StringLength(80)]
        public string? Equipment { get; set; }
        [Column("Equipment_type")]
        [StringLength(80)]
        public string? EquipmentType { get; set; }
        [Column("Ent_Id")]
        public int EntId { get; set; }
        [Column("Sort_Order")]
        [StringLength(80)]
        public string? SortOrder { get; set; }
        public double? WelderSpeed { get; set; }
        [StringLength(40)]
        public string? Order { get; set; }
        [StringLength(80)]
        public string? OrderDesc { get; set; }
        [StringLength(40)]
        public string? Material { get; set; }
        public int? TubesMade { get; set; }
        public int? TubesRequired { get; set; }
        public int? BundlesMade { get; set; }
        public int? BundlesRequired { get; set; }
        public double? OrderTons { get; set; }
        public double? OrderTonsDone { get; set; }
        [StringLength(40)]
        public string? State { get; set; }
        [StringLength(80)]
        public string? Reason { get; set; }
        [StringLength(40)]
        public string? Duration { get; set; }
        public int? ChangeOvers { get; set; }
        public int? CoilsThisShift { get; set; }
        public int? CoilsThisWeek { get; set; }
        public double? AvgCoilsPerHour { get; set; }
        public double? SlitWeightRequired { get; set; }
        public double? SlitWeightCompleted { get; set; }
        [Column("cs_start")]
        [StringLength(40)]
        public string? CsStart { get; set; }
        [Column("cs_end")]
        [StringLength(40)]
        public string? CsEnd { get; set; }
        [Column("cs_id")]
        public int? CsId { get; set; }
        [Column("tw_prd")]
        [StringLength(40)]
        public string? TwPrd { get; set; }
        [Column("tw_cns")]
        [StringLength(40)]
        public string? TwCns { get; set; }
        [Column("tw_yld")]
        [StringLength(40)]
        public string? TwYld { get; set; }
        [Column("tw_oee")]
        [StringLength(40)]
        public string? TwOee { get; set; }
        [Column("tw_up")]
        [StringLength(40)]
        public string? TwUp { get; set; }
        [StringLength(40)]
        public string? Shift1MonthDay { get; set; }
        [Column("Shift1Shift_Desc")]
        [StringLength(40)]
        public string? Shift1ShiftDesc { get; set; }
        [Column("Shift1DOWNTIME")]
        [StringLength(40)]
        public string? Shift1Downtime { get; set; }
        [Column("Shift1RUNNING")]
        [StringLength(40)]
        public string? Shift1Running { get; set; }
        [Column("Shift1UNKNOWN")]
        [StringLength(40)]
        public string? Shift1Unknown { get; set; }
        [Column("Shift1UNSCHEDULED")]
        [StringLength(40)]
        public string? Shift1Unscheduled { get; set; }
        [Column("Shift1SCHEDULEDMAINTENANCE")]
        [StringLength(40)]
        public string? Shift1Scheduledmaintenance { get; set; }
        [Column("Shift1CHANGEOVER")]
        [StringLength(40)]
        public string? Shift1Changeover { get; set; }
        [Column("Shift1PLANNEDDOWNTIME")]
        [StringLength(40)]
        public string? Shift1Planneddowntime { get; set; }
        [Column("Shift1TONSPROD")]
        [StringLength(40)]
        public string? Shift1Tonsprod { get; set; }
        [Column("Shift1TONSCON")]
        [StringLength(40)]
        public string? Shift1Tonscon { get; set; }
        [Column("Shift1UPTIME")]
        [StringLength(40)]
        public string? Shift1Uptime { get; set; }
        [Column("Shift1OEE")]
        [StringLength(40)]
        public string? Shift1Oee { get; set; }
        [StringLength(40)]
        public string? Shift2MonthDay { get; set; }
        [Column("Shift2Shift_Desc")]
        [StringLength(40)]
        public string? Shift2ShiftDesc { get; set; }
        [Column("Shift2TONSPROD")]
        [StringLength(40)]
        public string? Shift2Tonsprod { get; set; }
        [Column("Shift2TONSCON")]
        [StringLength(40)]
        public string? Shift2Tonscon { get; set; }
        [Column("Shift2UPTIME")]
        [StringLength(40)]
        public string? Shift2Uptime { get; set; }
        [Column("Shift2OEE")]
        [StringLength(40)]
        public string? Shift2Oee { get; set; }
        [StringLength(40)]
        public string? Shift3MonthDay { get; set; }
        [Column("Shift3Shift_Desc")]
        [StringLength(40)]
        public string? Shift3ShiftDesc { get; set; }
        [Column("Shift3TONSPROD")]
        [StringLength(40)]
        public string? Shift3Tonsprod { get; set; }
        [Column("Shift3TONSCON")]
        [StringLength(40)]
        public string? Shift3Tonscon { get; set; }
        [Column("Shift3UPTIME")]
        [StringLength(40)]
        public string? Shift3Uptime { get; set; }
        [Column("Shift3OEE")]
        [StringLength(40)]
        public string? Shift3Oee { get; set; }
        [StringLength(40)]
        public string? Shift4MonthDay { get; set; }
        [Column("Shift4Shift_Desc")]
        [StringLength(40)]
        public string? Shift4ShiftDesc { get; set; }
        [Column("Shift4TONSPROD")]
        [StringLength(40)]
        public string? Shift4Tonsprod { get; set; }
        [Column("Shift4TONSCON")]
        [StringLength(40)]
        public string? Shift4Tonscon { get; set; }
        [Column("Shift4UPTIME")]
        [StringLength(40)]
        public string? Shift4Uptime { get; set; }
        [Column("Shift4OEE")]
        [StringLength(40)]
        public string? Shift4Oee { get; set; }
        [StringLength(40)]
        public string? CoilStack00 { get; set; }
        [StringLength(40)]
        public string? CoilStack01 { get; set; }
        [StringLength(40)]
        public string? CoilStack02 { get; set; }
        [StringLength(40)]
        public string? CoilStack03 { get; set; }
        [StringLength(40)]
        public string? CoilStack04 { get; set; }
        [StringLength(40)]
        public string? CoilStack05 { get; set; }
        [StringLength(40)]
        public string? CoilStack06 { get; set; }
        [StringLength(40)]
        public string? CoilStack07 { get; set; }
        [StringLength(40)]
        public string? CoilStack08 { get; set; }
        [StringLength(40)]
        public string? CoilStack09 { get; set; }
        [StringLength(12)]
        public string? HeatStack00 { get; set; }
        [StringLength(12)]
        public string? HeatStack01 { get; set; }
        [StringLength(12)]
        public string? HeatStack02 { get; set; }
        [StringLength(12)]
        public string? HeatStack03 { get; set; }
        [StringLength(12)]
        public string? HeatStack04 { get; set; }
        [StringLength(12)]
        public string? HeatStack05 { get; set; }
        [StringLength(12)]
        public string? HeatStack06 { get; set; }
        [StringLength(12)]
        public string? HeatStack07 { get; set; }
        [StringLength(12)]
        public string? HeatStack08 { get; set; }
        [StringLength(12)]
        public string? HeatStack09 { get; set; }
        [StringLength(40)]
        public string? MaterialStack00 { get; set; }
        [StringLength(40)]
        public string? MaterialStack01 { get; set; }
        [StringLength(40)]
        public string? MaterialStack02 { get; set; }
        [StringLength(40)]
        public string? MaterialStack03 { get; set; }
        [StringLength(40)]
        public string? MaterialStack04 { get; set; }
        [StringLength(40)]
        public string? MaterialStack05 { get; set; }
        [StringLength(40)]
        public string? MaterialStack06 { get; set; }
        [StringLength(40)]
        public string? MaterialStack07 { get; set; }
        [StringLength(40)]
        public string? MaterialStack08 { get; set; }
        [StringLength(40)]
        public string? MaterialStack09 { get; set; }
        public int? ReceivedCount { get; set; }
        public long? ReceivedPounds { get; set; }
        public DateTime? LastUpdated { get; set; }
        [Key]
        [Column("row_id")]
        public int RowId { get; set; }
        public double? FeetMade { get; set; }
        public double? FeetRequired { get; set; }
    }
}
