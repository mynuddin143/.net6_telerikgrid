using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("PhoneNumber")]
    public partial class PhoneNumber
    {
        [Key]
        [Column("PhoneNumberID")]
        public int PhoneNumberID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [StringLength(50)]
        [Unicode(false)]
        public string? Number { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Extension { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? FaxNumber { get; set; }
        [Column("LocationID")]
        public int? LocationID { get; set; }

        [ForeignKey("LocationID")]
        [InverseProperty("PhoneNumbers")]
        public virtual Location? Location { get; set; }
    }
}
