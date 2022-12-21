﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.PortalModels
{
    [Table("SAPVendor")]
    [Microsoft.EntityFrameworkCore.Index(nameof(DivisionId), nameof(Number), Name = "IX_SAPVendor", IsUnique = true)]
    public partial class Sapvendor
    {
        public Sapvendor()
        {
            Boxes = new HashSet<Box>();
            Sapdeliveries = new HashSet<Sapdelivery>();
            Sapshipments = new HashSet<Sapshipment>();
            Trailers = new HashSet<Trailer>();
            Trucks = new HashSet<Truck>();
        }

        [Key]
        [Column("SAPVendorID")]
        public long SapvendorId { get; set; }
        [Column("DivisionID")]
        public long DivisionId { get; set; }
        [Required]
        [StringLength(10)]
        public string Number { get; set; }
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        [Required]
        [StringLength(30)]
        public string Address { get; set; }
        [Column("CityID")]
        public long CityId { get; set; }
        [Required]
        [StringLength(10)]
        public string PostalCode { get; set; }
        [Required]
        [StringLength(16)]
        public string Phone { get; set; }
        [Required]
        [StringLength(31)]
        public string Fax { get; set; }
        [Required]
        public bool? Active { get; set; }
        public bool Rail { get; set; }
        public bool Express { get; set; }
        public bool Intermodal { get; set; }

        [ForeignKey(nameof(CityId))]
        [InverseProperty("Sapvendors")]
        public virtual City City { get; set; }
        [ForeignKey(nameof(DivisionId))]
        [InverseProperty("Sapvendors")]
        public virtual Division Division { get; set; }
        [InverseProperty(nameof(Box.Sapvendor))]
        public virtual ICollection<Box> Boxes { get; set; }
        [InverseProperty(nameof(Sapdelivery.Sapvendor))]
        public virtual ICollection<Sapdelivery> Sapdeliveries { get; set; }
        [InverseProperty(nameof(Sapshipment.Sapvendor))]
        public virtual ICollection<Sapshipment> Sapshipments { get; set; }
        [InverseProperty(nameof(Trailer.Sapvendor))]
        public virtual ICollection<Trailer> Trailers { get; set; }
        [InverseProperty(nameof(Truck.Sapvendor))]
        public virtual ICollection<Truck> Trucks { get; set; }
    }
}