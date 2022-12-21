﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.PortalModels
{
    [Table("SAPWheatlandDelivery")]
    [Microsoft.EntityFrameworkCore.Index(nameof(SoldToNumber), Name = "IX_SAPWheatlandDelivery")]
    public partial class SapwheatlandDelivery
    {
        [Key]
        [Column("DeliveryID")]
        public int DeliveryId { get; set; }
        [StringLength(50)]
        public string DeliveryNumber { get; set; }
        [StringLength(50)]
        public string ShipToNumber { get; set; }
        [StringLength(20)]
        public string SoldToNumber { get; set; }
        [Column("CustomerPO")]
        [StringLength(50)]
        public string CustomerPo { get; set; }
        [StringLength(50)]
        public string SalesOrderNumber { get; set; }
        [StringLength(50)]
        public string MaterialNumber { get; set; }
        [StringLength(100)]
        public string ItemDesc { get; set; }
        [StringLength(50)]
        public string BatchNumber { get; set; }
        [StringLength(50)]
        public string HeatNumber { get; set; }
        [StringLength(50)]
        public string RunNumber { get; set; }
        [Column("PGIDate", TypeName = "date")]
        public DateTime? Pgidate { get; set; }
    }
}