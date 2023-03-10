// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.PortalModels
{
    [Table("SAPCustomerText")]
    public partial class SapcustomerText
    {
        [Key]
        [Column("SAPCustomerTextID")]
        public long SapcustomerTextId { get; set; }
        [Column("SAPCustomerTextTypeID")]
        public long SapcustomerTextTypeId { get; set; }
        [Column("SAPSalesOrganizationID")]
        public long SapsalesOrganizationId { get; set; }
        [Column("SAPShipToID")]
        public long SapshipToId { get; set; }
        public int LineNumber { get; set; }
        [Required]
        [StringLength(132)]
        public string Text { get; set; }

        [ForeignKey(nameof(SapcustomerTextTypeId))]
        [InverseProperty(nameof(SapcharacteristicOption.SapcustomerTexts))]
        public virtual SapcharacteristicOption SapcustomerTextType { get; set; }
        [ForeignKey(nameof(SapsalesOrganizationId))]
        [InverseProperty("SapcustomerTexts")]
        public virtual SapsalesOrganization SapsalesOrganization { get; set; }
        [ForeignKey(nameof(SapshipToId))]
        [InverseProperty("SapcustomerTexts")]
        public virtual SapshipTo SapshipTo { get; set; }
    }
}