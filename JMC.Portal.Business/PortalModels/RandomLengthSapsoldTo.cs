﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.PortalModels
{
    [Table("RandomLengthSAPSoldTo")]
    [Microsoft.EntityFrameworkCore.Index(nameof(SapsoldToId), nameof(LocationId), Name = "IX_RandomLengthSAPSoldTo", IsUnique = true)]
    public partial class RandomLengthSapsoldTo
    {
        public RandomLengthSapsoldTo()
        {
            Boxes = new HashSet<Box>();
            ScrapSales = new HashSet<ScrapSale>();
            Trailers = new HashSet<Trailer>();
            Trucks = new HashSet<Truck>();
        }

        [Key]
        [Column("RandomLengthSAPSoldToID")]
        public long RandomLengthSapsoldToId { get; set; }
        [Column("SAPSoldToID")]
        public long SapsoldToId { get; set; }
        [Column("LocationID")]
        public long LocationId { get; set; }
        public bool Active { get; set; }

        [ForeignKey(nameof(LocationId))]
        [InverseProperty("RandomLengthSapsoldTos")]
        public virtual Location Location { get; set; }
        [ForeignKey(nameof(SapsoldToId))]
        [InverseProperty("RandomLengthSapsoldTos")]
        public virtual SapsoldTo SapsoldTo { get; set; }
        [InverseProperty(nameof(Box.RandomLengthSapsoldTo))]
        public virtual ICollection<Box> Boxes { get; set; }
        [InverseProperty(nameof(ScrapSale.RandomLengthSapsoldTo))]
        public virtual ICollection<ScrapSale> ScrapSales { get; set; }
        [InverseProperty(nameof(Trailer.RandomLengthSapsoldTo))]
        public virtual ICollection<Trailer> Trailers { get; set; }
        [InverseProperty(nameof(Truck.RandomLengthSapsoldTo))]
        public virtual ICollection<Truck> Trucks { get; set; }
    }
}