using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("LoginHistory")]
    [Index("LoginDate", Name = "ix_LoginDate")]
    public partial class LoginHistory
    {
        [Key]
        [Column("LoginHistoryID")]
        public int LoginHistoryID { get; set; }
        [Column("UserID")]
        public int UserID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime LoginDate { get; set; }
        [StringLength(500)]
        [Unicode(false)]
        public string? UserAgent { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? BrowserName { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? BrowserVersion { get; set; }
        public bool? Cookies { get; set; }
        public bool? Javascript { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Platform { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? ScreenResolution { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? IpAddress { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? HostName { get; set; }

        [ForeignKey("UserID")]
        [InverseProperty("LoginHistories")]
        public virtual User User { get; set; } = null!;
    }
}
