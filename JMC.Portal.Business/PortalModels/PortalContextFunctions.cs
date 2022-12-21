﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Linq;
using JMC.Portal.Business.PortalModels;

namespace JMC.Portal.Business.PortalModels
{
    public partial class PortalContext
    {

        [DbFunction("ConvertSQFRateInCWT", "dbo")]
        public static decimal? ConvertSQFRateInCWT(decimal? sqfRate, decimal? WeightPerFoot, decimal? size, decimal? size2, decimal? diameter)
        {
            throw new NotSupportedException("This method can only be called from Entity Framework Core queries");
        }

        //[DbFunction("fnSplitIds", "dbo")]
        //public IQueryable<fnSplitIdsResult> fnSplitIds(string @string, string delimiter)
        //{
        //    return FromExpression(() => fnSplitIds(string, delimiter));
        //}

        [DbFunction("GETFSCRate", "dbo")]
        public static decimal? GETFSCRate(decimal? sapMaterialId, long? SoldToId, long? ShipToId)
        {
            throw new NotSupportedException("This method can only be called from Entity Framework Core queries");
        }

        protected void OnModelCreatingGeneratedFunctions(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<fnSplitIdsResult>().HasNoKey();
        }
    }
}
