﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.PortalModels
{
    [Keyless]
    [Table("SAPCustomerServiceRepSAPSalesOrganization")]
    public partial class SapcustomerServiceRepSapsalesOrganization
    {
        [Column("SAPCustomerServiceRepID")]
        public long SapcustomerServiceRepId { get; set; }
        [Column("SAPSalesOrganizationID")]
        public long SapsalesOrganizationId { get; set; }

        [ForeignKey(nameof(SapcustomerServiceRepId))]
        public virtual SapcustomerServiceRep SapcustomerServiceRep { get; set; }
        [ForeignKey(nameof(SapsalesOrganizationId))]
        public virtual SapsalesOrganization SapsalesOrganization { get; set; }
    }
}