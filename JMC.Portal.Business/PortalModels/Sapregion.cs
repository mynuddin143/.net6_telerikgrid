﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.PortalModels
{
    [Table("SAPRegion")]
    public partial class Sapregion
    {
        public Sapregion()
        {
            SapshipToSapsalesOrganizations = new HashSet<SapshipToSapsalesOrganization>();
        }

        [Key]
        [Column("SAPRegionID")]
        public long SapregionId { get; set; }
        [Column("DivisionID")]
        public long DivisionId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [Column("SAPCode")]
        [StringLength(50)]
        public string Sapcode { get; set; }

        [ForeignKey(nameof(DivisionId))]
        [InverseProperty("Sapregions")]
        public virtual Division Division { get; set; }
        [InverseProperty(nameof(SapshipToSapsalesOrganization.Sapregion))]
        public virtual ICollection<SapshipToSapsalesOrganization> SapshipToSapsalesOrganizations { get; set; }
    }
}