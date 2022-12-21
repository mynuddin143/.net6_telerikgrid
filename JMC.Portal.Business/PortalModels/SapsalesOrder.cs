﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.PortalModels
{
    [Table("SAPSalesOrder")]
    [Microsoft.EntityFrameworkCore.Index(nameof(DivisionId), nameof(Number), Name = "IX_SAPSalesOrder", IsUnique = true)]
    [Microsoft.EntityFrameworkCore.Index(nameof(Date), Name = "Prf_SODate")]
    [Microsoft.EntityFrameworkCore.Index(nameof(Number), Name = "missing_index_35_34_SAPSalesOrder")]
    public partial class SapsalesOrder
    {
        public SapsalesOrder()
        {
            SapsalesOrderItems = new HashSet<SapsalesOrderItem>();
        }

        [Key]
        [Column("SAPSalesOrderID")]
        public long SapsalesOrderId { get; set; }
        [Column("DivisionID")]
        public long DivisionId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Date { get; set; }
        [StringLength(10)]
        public string Number { get; set; }
        [Column("BAPIReturnMessage")]
        [StringLength(2000)]
        public string BapireturnMessage { get; set; }
        [Column("QuoteID")]
        public long? QuoteId { get; set; }
        [Column("PONumber")]
        [StringLength(35)]
        public string Ponumber { get; set; }
        [Column("SAPSoldToID")]
        public long? SapsoldToId { get; set; }
        [Column("PlantID")]
        public long? PlantId { get; set; }
        [Column("SAPShipToID")]
        public long? SapshipToId { get; set; }
        [StringLength(4)]
        public string DocumentType { get; set; }
        [StringLength(2)]
        public string DistributionChannel { get; set; }
        [StringLength(1)]
        public string CreditStatus { get; set; }
        [StringLength(2)]
        public string DeliveryBlock { get; set; }
        [StringLength(20)]
        public string DeliveryBlockText { get; set; }
        [Column("PODate", TypeName = "date")]
        public DateTime? Podate { get; set; }
        [Column("UserID")]
        public long? UserId { get; set; }
        [StringLength(50)]
        public string YourReference { get; set; }
        [Column("DealID")]
        public long? DealId { get; set; }

        [ForeignKey(nameof(DivisionId))]
        [InverseProperty("SapsalesOrders")]
        public virtual Division Division { get; set; }
        [ForeignKey(nameof(PlantId))]
        [InverseProperty("SapsalesOrders")]
        public virtual Plant Plant { get; set; }
        [ForeignKey(nameof(QuoteId))]
        [InverseProperty("SapsalesOrders")]
        public virtual Quote Quote { get; set; }
        [ForeignKey(nameof(SapshipToId))]
        [InverseProperty("SapsalesOrders")]
        public virtual SapshipTo SapshipTo { get; set; }
        [ForeignKey(nameof(SapsoldToId))]
        [InverseProperty("SapsalesOrders")]
        public virtual SapsoldTo SapsoldTo { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("SapsalesOrders")]
        public virtual User User { get; set; }
        [InverseProperty("SapsalesOrder")]
        public virtual SapscrapOrder SapscrapOrder { get; set; }
        [InverseProperty(nameof(SapsalesOrderItem.SapsalesOrder))]
        public virtual ICollection<SapsalesOrderItem> SapsalesOrderItems { get; set; }
    }
}