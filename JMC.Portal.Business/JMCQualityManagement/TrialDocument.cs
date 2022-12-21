using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.JMCQualityManagement
{
    [Table("TrialDocument")]
    public partial class TrialDocument
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("TrialID")]
        public int TrialId { get; set; }
        [StringLength(255)]
        public string? DocumentName { get; set; }
    }
}
