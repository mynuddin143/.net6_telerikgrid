﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JMC.Portal.Business.PortalModels
{
    public partial class GetDeliveryNumbersResult
    {
        public long SAPDeliveryItemID { get; set; }
        public long SAPDeliveryID { get; set; }
        public int Position { get; set; }
        public long? SAPSalesOrderItemID { get; set; }
        public decimal? Weight { get; set; }
        public string WeightUnit { get; set; }
        public string Status { get; set; }
        public decimal? QuantityDelivered { get; set; }
        public string SalesUnit { get; set; }
    }
}
