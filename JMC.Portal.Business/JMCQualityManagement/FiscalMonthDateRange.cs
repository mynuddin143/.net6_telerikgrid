using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.JMCQualityManagement
{
    [Table("FiscalMonthDateRange")]
    public partial class FiscalMonthDateRange
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        public int? Year { get; set; }
        public int? Month { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string? MonthDesc { get; set; }
        [Column(TypeName = "date")]
        public DateTime? StartDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? EndDate { get; set; }
    }
}
