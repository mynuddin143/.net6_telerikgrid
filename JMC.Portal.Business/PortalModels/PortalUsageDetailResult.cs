﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JMC.Portal.Business.PortalModels
{
    public partial class PortalUsageDetailResult
    {
        public string Number { get; set; }
        public int Position { get; set; }
        public DateTime? DATE { get; set; }
        public string SoldToNumber { get; set; }
        public string PONumber { get; set; }
        public string ShipTo { get; set; }
        public string MaterialDescription { get; set; }
        public decimal? OrderedQty { get; set; }
        public decimal? PieceWeight { get; set; }
        public string RecordSource { get; set; }
        public long SAPSalesOrderID { get; set; }
        public long? PlantID { get; set; }
        public long? SAPSoldToID { get; set; }
        public decimal? GrossWeight { get; set; }
        public string PlantCode { get; set; }
    }
}