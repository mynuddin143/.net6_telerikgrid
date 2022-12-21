using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.JMCQualityManagement
{
    public partial class TrialResponsibility
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("TrialID")]
        public int TrialId { get; set; }
        [Column("TrialDepartmentID")]
        public int TrialDepartmentId { get; set; }
        [Column("TrialUserID")]
        public int TrialUserId { get; set; }
        [Column(TypeName = "text")]
        public string? Intructions { get; set; }
        [Column(TypeName = "text")]
        public string? FeedBack { get; set; }
        public bool? Completed { get; set; }
        [Column(TypeName = "date")]
        public DateTime? CompletedDate { get; set; }
    }
}
