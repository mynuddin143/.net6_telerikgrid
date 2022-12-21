using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("AdditionalPhoneNumber")]
    public partial class AdditionalPhoneNumber
    {
        [Key]
        [Column("AdditionalPhoneNumberID")]
        public int AdditionalPhoneNumberID { get; set; }
        [Column("UserID")]
        public int UserID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [StringLength(25)]
        [Unicode(false)]
        public string? PhoneNumber { get; set; }
        [StringLength(25)]
        [Unicode(false)]
        public string? Extension { get; set; }
        [StringLength(25)]
        [Unicode(false)]
        public string? FaxNumber { get; set; }
        [Column("LocationID")]
        public int? LocationID { get; set; }

        [ForeignKey("LocationID")]
        [InverseProperty("AdditionalPhoneNumbers")]
        public virtual Location? Location { get; set; }
    }
}
