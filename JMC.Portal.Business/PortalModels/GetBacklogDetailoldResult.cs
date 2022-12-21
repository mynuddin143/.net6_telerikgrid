﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JMC.Portal.Business.PortalModels
{
    public partial class GetBacklogDetailoldResult
    {
        public long SAPSalesOrderID { get; set; }
        public long SAPSalesOrderItemID { get; set; }
        public long SAPMaterialID { get; set; }
        public string CustomerMaterialNumber { get; set; }
        public string MaterialShortDescription { get; set; }
        public int? PiecesPerBundle { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? RollDate { get; set; }
        public long? SAPShipToID { get; set; }
        public long? PlantID { get; set; }
        public decimal? DisplayNotReadyWeight { get; set; }
        public decimal? DisplayReadyWeight { get; set; }
        public decimal? DisplayReleasedWeight { get; set; }
        public int Position { get; set; }
        public decimal? OrderedPieces { get; set; }
        public decimal? WeightPerPiece { get; set; }
        public string SalesOrderNumber { get; set; }
        public string PONumber { get; set; }
        public long? SAPSoldToID { get; set; }
        public string CreditStatus { get; set; }
        public string DeliveryBlock { get; set; }
        public string DeliveryBlockText { get; set; }
        public string YourReference { get; set; }
        public string SAPSoldToNumber { get; set; }
        public string SAPSoldToName { get; set; }
        public string SAPShipToNumber { get; set; }
        public string SAPShipToName { get; set; }
        public string SAPShipToIncoTerms2 { get; set; }
        public string SAPSoldToCityAndState { get; set; }
        public string PlantName { get; set; }
        public decimal? ConsumedInShippingCart { get; set; }
        public string SAPDeliveryNumber { get; set; }
        public decimal? DeliveryWeight { get; set; }
        public decimal Price { get; set; }
    }
}
