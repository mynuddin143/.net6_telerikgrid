// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.PortalModels
{
    [Table("QuoteStatus")]
    public partial class QuoteStatus
    {
        public QuoteStatus()
        {
            QuoteStatusChanges = new HashSet<QuoteStatusChange>();
        }

        [Key]
        [Column("QuoteStatusID")]
        public long QuoteStatusId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [InverseProperty(nameof(QuoteStatusChange.QuoteStatus))]
        public virtual ICollection<QuoteStatusChange> QuoteStatusChanges { get; set; }
    }
}