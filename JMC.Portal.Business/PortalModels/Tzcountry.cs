// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.PortalModels
{
    [Table("TZCountry")]
    public partial class Tzcountry
    {
        public Tzcountry()
        {
            Tzzones = new HashSet<Tzzone>();
        }

        [Key]
        [Column("country_code")]
        [StringLength(2)]
        public string CountryCode { get; set; }
        [Column("country_name")]
        [StringLength(45)]
        public string CountryName { get; set; }

        [InverseProperty(nameof(Tzzone.CountryCodeNavigation))]
        public virtual ICollection<Tzzone> Tzzones { get; set; }
    }
}