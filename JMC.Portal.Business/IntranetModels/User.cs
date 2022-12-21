using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("User")]
    public partial class User
    {
        public User()
        {
            LoginHistories = new HashSet<LoginHistory>();
            OutsideSalesReps = new HashSet<OutsideSalesRep>();
            SAPCustomerGroupUsers = new HashSet<SAPCustomerGroupUser>();
            SAPCustomerGroups = new HashSet<SAPCustomerGroup>();
            UserProfiles = new HashSet<UserProfile>();
        }

        [Key]
        [Column("UserID")]
        public int UserID { get; set; }
        [StringLength(25)]
        [Unicode(false)]
        public string LastName { get; set; } = null!;
        [StringLength(25)]
        [Unicode(false)]
        public string FirstName { get; set; } = null!;
        [StringLength(25)]
        [Unicode(false)]
        public string UserName { get; set; } = null!;
        [StringLength(128)]
        [Unicode(false)]
        public string Password { get; set; } = null!;
        [StringLength(128)]
        [Unicode(false)]
        public string PasswordSalt { get; set; } = null!;
        public bool PasswordReset { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Email { get; set; } = null!;
        [Column(TypeName = "datetime")]
        public DateTime? LastLoginDate { get; set; }
        public bool Active { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? PhoneNumber { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? FaxNumber { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Extension { get; set; }

        [InverseProperty("CustomerUserNavigation")]
        public virtual CustomerUser CustomerUser { get; set; } = null!;
        [InverseProperty("EmployeeNavigation")]
        public virtual Employee Employee { get; set; } = null!;
        [InverseProperty("PlantComputerNavigation")]
        public virtual PlantComputer PlantComputer { get; set; } = null!;
        [InverseProperty("User")]
        public virtual ICollection<LoginHistory> LoginHistories { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<OutsideSalesRep> OutsideSalesReps { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<SAPCustomerGroupUser> SAPCustomerGroupUsers { get; set; }
        [InverseProperty("RegionalManagerUser")]
        public virtual ICollection<SAPCustomerGroup> SAPCustomerGroups { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<UserProfile> UserProfiles { get; set; }
    }
}
