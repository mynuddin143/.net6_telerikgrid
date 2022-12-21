﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.PortalModels
{
    [Table("SAPCondition")]
    public partial class Sapcondition
    {
        public Sapcondition()
        {
            FreightandFscs = new HashSet<FreightandFsc>();
            PriceChangeRequestItems = new HashSet<PriceChangeRequestItem>();
            QuoteMaterialSapconditions = new HashSet<QuoteMaterialSapcondition>();
            QuoteSapconditions = new HashSet<QuoteSapcondition>();
        }

        [Key]
        [Column("SAPConditionID")]
        public long SapconditionId { get; set; }
        [Column("DivisionID")]
        public long DivisionId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [Column("SAPCode")]
        [StringLength(4)]
        public string Sapcode { get; set; }
        [Required]
        [StringLength(5)]
        public string Application { get; set; }
        public bool? Override { get; set; }
        public bool? Extra { get; set; }

        [ForeignKey(nameof(DivisionId))]
        [InverseProperty("Sapconditions")]
        public virtual Division Division { get; set; }
        [InverseProperty(nameof(FreightandFsc.Sapcondition))]
        public virtual ICollection<FreightandFsc> FreightandFscs { get; set; }
        [InverseProperty(nameof(PriceChangeRequestItem.Sapcondition))]
        public virtual ICollection<PriceChangeRequestItem> PriceChangeRequestItems { get; set; }
        [InverseProperty(nameof(QuoteMaterialSapcondition.Sapcondition))]
        public virtual ICollection<QuoteMaterialSapcondition> QuoteMaterialSapconditions { get; set; }
        [InverseProperty(nameof(QuoteSapcondition.Sapcondition))]
        public virtual ICollection<QuoteSapcondition> QuoteSapconditions { get; set; }
    }
}